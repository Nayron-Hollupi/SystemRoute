using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Domain.Model.Base;

namespace Route.Domain.Model
{
    public class City : Entity
    {
        #region Properties
        [DisplayName("Nome da Cidade")]
        public string Name { get; set; }
        [DisplayName("Nome do Estado UF")]
        public string State { get; set; }

        #endregion
    }
}
