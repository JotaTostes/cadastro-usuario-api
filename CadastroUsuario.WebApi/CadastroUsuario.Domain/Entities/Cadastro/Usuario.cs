using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CadastroUsuario.Domain.Entities.Cadastro
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("USR_GUID")]
        public Guid? Guid { get; set; }

        [Column("USR_NOME")]
        public string Nome { get; set; }

        [Column("USR_EMAIL")]
        public string Email { get; set; }

        [Column("USR_SEXO")]
        public string Sexo { get; set; }

        [Column("USR_SENHA")]
        public string Senha { get; set; }

        [Column("USR_DATA_NASC")]
        public DateTime? DataNascimento { get; set; }

        [Column("USR_STATUS")]
        public char Status { get; set; }

    }
}
