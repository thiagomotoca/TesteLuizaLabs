using System;
using System.Runtime.Serialization;

namespace TesteLuizaLabs.Lib.Excecao
{
    public class DadoInvalidoException : ArgumentException
    {
        public DadoInvalidoException() : base() { }

        public DadoInvalidoException(string message) : base(message) { }

        public DadoInvalidoException(string message, Exception innerException) : base(message, innerException) { }

        public DadoInvalidoException(string message, string paramName) : base(message, paramName) { }

        public DadoInvalidoException(string message, string paramName, Exception innerException) : base(message, paramName, innerException) { }

        protected DadoInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
