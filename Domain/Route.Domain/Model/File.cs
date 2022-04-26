using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Route.Domain.Model.Base;

namespace Route.Domain.Model
{
    public class File : Entity
    { 

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Nome do Arquivo")]
        public string FileDescription { get; set; }

        [NotMapped]
        [DisplayName("Arquivo")]
        public IFormFile Excel { get; set; }
    }
}
