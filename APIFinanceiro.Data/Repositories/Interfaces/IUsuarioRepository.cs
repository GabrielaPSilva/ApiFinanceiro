using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> ListarUsuario();
        Task<UsuarioModel> RetornarUsuarioCPF(string CPF);
        Task<int> CadastrarUsuario(UsuarioModel usuario);
        Task<bool> AlterarUsuario(UsuarioModel usuario);
        Task<bool> DesativarUsuario(int idUsuario);
        Task<bool> ReativarUsuario(string CPF);
    }
}
