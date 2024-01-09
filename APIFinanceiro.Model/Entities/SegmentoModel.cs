using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities
{
    public class SegmentoModel
    {
        public int Id { get; set; }
        public int IdRisco { get; set; }
        public string? TipoSegmento { get; set; }
        public Decimal PercentualRendimento { get; set; }
        public Decimal TaxaAdm { get; set; }
        public int MesesVigencia { get; set; }
    }
}
