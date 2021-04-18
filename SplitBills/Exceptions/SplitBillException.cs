using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitBills.Exceptions
{
    public class SplitBillException : Exception
    {
        private string _errorMessage;

        private int _statusCode;

        public SplitBillException(int code, string message) : base(message)
        {
            _errorMessage = message;
            _statusCode = code;
        }
    }
}
