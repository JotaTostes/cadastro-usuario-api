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
        [Column("UsuarioId")]
        public int UsuarioId { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("SexoId")]
        public int Sexo { get; set; }

        [Column("Senha")]
        public string Senha { get; set; }

        [Column("DataNascimento")]
        public DateTime? DataNascimento { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

    }
}
