using System;
using System.Runtime.Serialization;

namespace Observations
{
    [Serializable]
    internal class DBHandlerException : Exception
    {
        public DBHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}