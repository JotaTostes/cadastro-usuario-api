using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CadastroUsuario.DataVo.ValueObjects.Cadastro
{
    [DataContract]
    public class UsuarioVo
    {
        [DataMember(Order = 1, Name = "UsuarioId", IsRequired = false)]
        [JsonIgnore]
        public int UsuarioId { get; set; }

        [DataMember(Order = 2, Name = "Nome", IsRequired = true)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve conter no maximo 50 caracteres e no minimo 3.")]
        public string Nome { get; set; }

        [DataMember(Order = 3, Name = "Email", IsRequired = true)]
        public string Email { get; set; }

        [DataMember(Order = 4, Name = "Sexo", IsRequired = true)]
        public int Sexo { get; set; }

        [DataMember(Order = 5, Name = "Senha", IsRequired = true)]
        public string Senha { get; set; }

        [DataMember(Order = 6, Name = "DataNascimento", IsRequired = false)]
        public DateTime? DataNascimento { get; set; }

        [DataMember(Order = 7, Name = "Ativo", IsRequired = false)]
        public bool Ativo { get; set; }
    }
}
