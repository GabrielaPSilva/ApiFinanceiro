using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities
{
    public class RiscoModel
    {
        public int Id { get; set; }
        public int GrauRisco { get; set; }
        public string? Descricao { get; set; }
    }
}
