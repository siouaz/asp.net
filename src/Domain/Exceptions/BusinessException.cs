using System;
using System.Runtime.Serialization;

namespace siwar.Domain.Exceptions
{
    /// <summary>
    /// Base business exception.
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException() { }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
