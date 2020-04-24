using System;
using System.Collections.Generic;
using System.Text;

namespace poc_cryptography
{
    public class Response
    {
        public short Numero_casas { get; set; }
        public string Token { get; set; }
        public string Cifrado { get; set; }
        public string Decifrado { get; set; }
        public string Resumo_criptografico { get; set; }
    }
}
