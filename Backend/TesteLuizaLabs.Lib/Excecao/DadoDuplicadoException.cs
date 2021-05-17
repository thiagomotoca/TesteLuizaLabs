using System;
using System.Runtime.Serialization;

namespace TesteLuizaLabs.Lib.Excecao
{
    public class DadoDuplicadoException : Exception
    {
        public DadoDuplicadoException() : base() { }

        public DadoDuplicadoException(string message) : base(message) { }

        public DadoDuplicadoException(string message, Exception innerException) : base(message, innerException) { }

        protected DadoDuplicadoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}