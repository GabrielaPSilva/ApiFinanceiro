using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities
{
    public class InvestimentoModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdSegmento { get; set; }
        public Decimal Saldo { get; set; }
        public Decimal ValorRendimento { get; set; }
        public Decimal ValorFinal { get; set; }

        public SegmentoModel? Segmento { get; set; }
    }
}
