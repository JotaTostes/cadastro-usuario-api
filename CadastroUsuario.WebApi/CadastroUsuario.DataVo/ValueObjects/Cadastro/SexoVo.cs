using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CadastroUsuario.DataVo.ValueObjects.Cadastro
{
    [DataContract]
    public class SexoVo
    {
        [DataMember(Order = 1, Name = "SexoId", IsRequired = false)]
        public int UsuarioId { get; set; }

        [DataMember(Order = 2, Name = "Descricao", IsRequired = false)]
        public string Nome { get; set; }

    }
}
