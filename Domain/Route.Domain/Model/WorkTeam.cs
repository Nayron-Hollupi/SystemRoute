using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Domain.Model.Base;

namespace Route.Domain.Model
{
    public class WorkTeam : Entity
    {
        #region Properties
        [DisplayName("Nome da Equipe")]
        public string Name { get; set; }
        [DisplayName("Nomes do  integrantes da equipe")]
        public string Person { get; set; }
        [DisplayName("Cidade de atuação")]
        public string City { get; set; }


        #endregion
    }
}
