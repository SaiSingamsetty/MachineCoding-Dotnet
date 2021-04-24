using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DistributedIdGeneration
{
    /*
     * References:
     *  https://github.com/gopalbala/distributed-idgen/blob/master/src/main/java/com/gb/didgen/service/SnowflakeSequenceIdGenerator.java
     *  https://www.c-sharpcorner.com/article/generate-a-snowflake-id-whose-length-is-16/
     *  https://www.callicoder.com/distributed-unique-id-sequence-number-generator/
     */
    public class SnowflakeIdGenerator
    {
        private readonly int _generatedNodeId;
        private const int SequenceBitLength = 12;
        private const int NodeIdBitLength = 10;

        private readonly int _maxSequence = (int) (Math.Pow(2, SequenceBitLength) - 1);
        private readonly int _maxNodeValue = (int) Math.Pow(2, NodeIdBitLength - 1);

        private readonly long _epochStart = 1609495800; // Epoch NOW: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();  Custom Epoch : 1609495800  Jan1 2021 10:10:00 AM

        private long _lastTimeStamp = -1L;
        private long _currentSequence = -1L;
        private readonly object _lockObj = new object();

        private void CheckNodeIdBounds()
        {
            if (_generatedNodeId < 0 || _generatedNodeId > _maxNodeValue)
            {
                throw new Exception("Invalid Node Id");
            }
        }

        public SnowflakeIdGenerator()
        {
            _generatedNodeId = CreateNodeId();
        }

        private int CreateNodeId()
        {
            int nodeId;
            try
            {
                var sb = new StringBuilder();
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (var networkInterface in networkInterfaces)
                {
                    var mac = networkInterface.GetPhysicalAddress().GetAddressBytes();
                    if (!mac.Any()) 
                        continue;

                    foreach (var eachByte in mac)
                    {
                        sb.Append(eachByte);
                    }
                }

                nodeId = sb.ToString().GetHashCode();
            }
            catch (Exception)
            {
                nodeId = new Random().Next(1, _maxNodeValue);
            }

            nodeId &= _maxNodeValue;
            return nodeId;
        }

        public long GenerateId(int counter)
        {
            CheckNodeIdBounds();
            //Console.WriteLine("Started generating ID : " + counter);
            lock (_lockObj)
            {
                var currentTimeStamp = GetTimeStamp();
                if (currentTimeStamp < _lastTimeStamp)
                {
                    throw new Exception("Clock moved back");
                }
                
                if (currentTimeStamp == _lastTimeStamp)
                {
                    //Console.WriteLine($"CTS and LTS are equal, {currentTimeStamp}");
                    //Console.WriteLine($"Sequence before: {_currentSequence}");
                    _currentSequence++;
                    _currentSequence &= _maxSequence;

                    if (_currentSequence == 0)
                    {
                        // Sequence Exhausted, wait till next millisecond.
                        currentTimeStamp = WaitNextMillis(currentTimeStamp);
                    }
                    //Console.WriteLine($"Sequence after: {_currentSequence}");
                }
                else
                {
                    _currentSequence = 0;
                }

                _lastTimeStamp = currentTimeStamp;

                
                var id = currentTimeStamp << (NodeIdBitLength + SequenceBitLength);
                id |= (long) (_generatedNodeId << SequenceBitLength);
                id |= _currentSequence;

                return id;
            }

        }

        private long GetTimeStamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _epochStart;
        }

        private long WaitNextMillis(long currentTimeStamp)
        {
            //As long as Current time stamp is less than Last time stamp, generate current time stamp again.
            while (currentTimeStamp <= _lastTimeStamp)
            {
                currentTimeStamp = GetTimeStamp();
            }
            return currentTimeStamp;
        }

    }
}
