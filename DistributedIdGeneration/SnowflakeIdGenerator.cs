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
     *  https://github.com/gopalbala/distributed-idgen/blob/master/src/main/java/com/gb/didgen/service/SnowflakeSequenceIdGenerator.java
     */
    public class SnowflakeIdGenerator
    {
        private readonly int _generatingNodeId;
        private const int SequenceBitLength = 12;
        private const int NodeIdBitLength = 10;

        private readonly int _maxSequence = (int) (Math.Pow(2, SequenceBitLength) - 1);
        private readonly int _maxNodeValue = (int) Math.Pow(2, NodeIdBitLength - 1);

        private readonly long _epochStart = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        private long _lastTimeStamp = -1L;
        private long _currentSequence = -1L;
        private readonly object _lockObj = new object();

        private void CheckNodeIdBounds()
        {
            if (_generatingNodeId < 0 || _generatingNodeId > _maxNodeValue)
            {
                throw new Exception("Invalid Node Id");
            }
        }

        public SnowflakeIdGenerator()
        {
            _generatingNodeId = CreateNodeId();
        }

        private static int CreateNodeId()
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
                nodeId = new Random().Next(1, NodeIdBitLength);
            }

            nodeId &= NodeIdBitLength;
            return nodeId;
        }

        public long GenerateId(int counter)
        {
            CheckNodeIdBounds();
            Console.WriteLine("Started generating ID : " + counter);
            lock (_lockObj)
            {
                var currentTimeStamp = GetTimeStamp();
                if (currentTimeStamp < _lastTimeStamp)
                {
                    throw new Exception("Clock moved back");
                }
                
                if (currentTimeStamp == _lastTimeStamp)
                {
                    Console.WriteLine($"CTS and LTS are equal, Current timestamp: {currentTimeStamp} Last time stamp : {_lastTimeStamp}");
                    Console.WriteLine($"Sequence before: {_currentSequence}");
                    _currentSequence++;
                    _currentSequence &= _maxSequence;

                    if (_currentSequence == 0)
                    {
                        currentTimeStamp = WaitNextMillis(currentTimeStamp);
                    }
                    Console.WriteLine($"Sequence after: {_currentSequence}");
                }
                else
                {
                    Console.WriteLine($"Current and Last are not equal now, Current timestamp: {currentTimeStamp} Last time stamp : {_lastTimeStamp}");
                    _currentSequence = 0;
                }

                _lastTimeStamp = currentTimeStamp;

                
                var id = currentTimeStamp << (NodeIdBitLength + SequenceBitLength);
                id |= (long) (_generatingNodeId << SequenceBitLength);
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
