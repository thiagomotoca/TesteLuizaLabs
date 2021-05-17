using System;
using System.Runtime.Serialization;

namespace TesteLuizaLabs.Lib.Excecao
{
    public class DadoNaoEncontratoException : Exception
    {
        public DadoNaoEncontratoException() : base() { }

        public DadoNaoEncontratoException(string message) : base(message) { }

        public DadoNaoEncontratoException(string message, Exception innerException) : base(message, innerException) { }

        protected DadoNaoEncontratoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
