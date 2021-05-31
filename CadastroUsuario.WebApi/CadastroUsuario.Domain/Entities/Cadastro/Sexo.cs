using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CadastroUsuario.Domain.Entities.Cadastro
{
    [Table("Sexo")]
    public class Sexo
    {
        [Key]
        [Column("SexoId")]
        public int SexoId { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }
    }
}
