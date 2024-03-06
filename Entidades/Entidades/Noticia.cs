using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }


        public string Informacao { get; set; }


        public bool Ativo { get; set; }


        public DateTime DataCadastro { get; set; }


        public DateTime DataAlteracao { get; set; }


        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}