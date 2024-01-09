using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities
{
    public class ResgateModel
    {
        public int Id { get; set; }
        public int IdInvestimento { get; set; }
        public Decimal Valor { get; set; }
        public DateTime DataResgate { get; set; }
    }
}
