using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFiliado.Models
{
    public class Filiado
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
