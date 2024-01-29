using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities.Enums
{
    public enum PermissoesEnum
    {
        Administrador = 1,
        Usuario = 2
    }

    public static class Permissoes
    {
        public const string Administrador = "Administrador";
        public const string Usuario = "Usuario";
    }
}
