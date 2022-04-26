using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Domain.Model.Base;

namespace Route.Domain.Model
{
    public class Person : Entity
    {
        #region Properties
        [DisplayName("Nome do funcionario")]
        public string Name { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
  

        #endregion
    }
}
