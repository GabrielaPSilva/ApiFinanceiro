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

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            sbMensagemErro.Clear();

            string mensagemErroNome = string.Empty;
            isValid = isValid && ValidarDescricao(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarDescricao(out string mensagemErro)
        {
            bool isValid = true;
            isValid = isValid && !string.IsNullOrEmpty(Descricao);
            isValid = isValid && Descricao!.Length >= 1;
            isValid = isValid && Descricao!.Length <= 100;

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "A descricao deve ter entre 1 e 100 caracteres.\n";

            return isValid;
        }
    }
}
