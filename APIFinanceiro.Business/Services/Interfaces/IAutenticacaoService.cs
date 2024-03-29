﻿using APIFinanceiro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<UsuarioPermissaoModel> ObterUsuarioNomeSenha(string nome, string senha);
        string GerarToken(UsuarioPermissaoModel user);
    }
}
