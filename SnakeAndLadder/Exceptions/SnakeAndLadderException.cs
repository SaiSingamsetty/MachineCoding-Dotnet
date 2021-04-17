using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadder.Exceptions
{
    public class SnakeAndLadderException : Exception
    {
        private int StatusCode { get; set; }

        private new string Message { get; set; }

        public SnakeAndLadderException(int code, string message) : base(message)
        {
            StatusCode = code;
            Message = message;
        }
    }
}
