using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Domain.Model.Base;

namespace Route.Domain.Model
{
    public class Routes : Entity
    {
       // public readonly static string INSERT = "INSERT INTO tb_rotas(OS, Cidade, Base, Servico, Endereco, Numero, Complemento, Cep, Bairro) values(@OS, @Cidade, @Base, @Servico, @Endereco, @Numero, @Complemento, @Cep, @Bairro)";

        public DateTime? DataRota { get; set; }
        public string? StatusRota { get; set; }
        public string? Auditado { get; set; }
        public string? CopRevertue { get; set; }
        public string? LogRota { get; set; }
        public string? Pdf { get; set; }
        public string? Foto { get; set; }
        public string? Contrato { get; set; }
        public string? OS { get; set; }
        public string? Assinante { get; set; }
        public string? Tecnicos { get; set; }
        public string? LoginUser { get; set; }
        public string? Cop { get; set; }
        public string? UltimoAlterar { get; set; }
        public string? LocalAtuacao { get; set; }
        public string? Ponto { get; set; }
        public string? Cidade { get; set; }
        public string? Base { get; set; }
        public DateTime? Horario { get; set; }
        public string? Segmento { get; set; }
        public string? Servico { get; set; }
        public string? TipoServico { get; set; }
        public string? TipoOS { get; set; }
        public string? Endereco { get; set; }
        public string? Numero { get; set; }
        public string Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cep { get; set; }
        public string? NodeRota { get; set; }
        public string? Pacote { get; set; }
        public string? Cod { get; set; }
        public string? Telefone1 { get; set; }
        public string? Telefone2 { get; set; }
        public string? Obs { get; set; }
        public string? ObsTecnico { get; set; }
        public string? Equipamento { get; set; }

    }
}
