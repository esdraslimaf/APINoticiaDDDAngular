using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
   A classe Notifica é uma classe base que fornece funcionalidades para validação de propriedades e gerenciamento de mensagens de erro ou advertências. 
   Ela contém métodos para validar propriedades de diferentes tipos e uma lista para armazenar notificações geradas durante o processo de validação.
*/


namespace Entidades.Entidades
{
    public class Noticia:Notifica
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