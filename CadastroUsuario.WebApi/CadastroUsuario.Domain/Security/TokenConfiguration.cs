using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroUsuario.Domain.Security
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
