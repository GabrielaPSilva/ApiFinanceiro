using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioModel>> ListarUsuario()
        {
            return await _usuarioRepository.ListarUsuario();
        }

        public async Task<UsuarioModel> RetornarUsuarioCPF(string CPF)
        {
            return await _usuarioRepository.RetornarUsuarioCPF(CPF);
        }

        public async Task<int> CadastrarUsuario(UsuarioModel usuario)
        {
            if (usuario == null)
                return 0;

            return await _usuarioRepository.CadastrarUsuario(usuario);
        }

        public async Task<bool> AlterarUsuario(UsuarioModel usuario)
        {
            if (usuario == null)
                return false;

            return await _usuarioRepository.AlterarUsuario(usuario);
        }

        public async Task<bool> DesativarUsuario(int idUsuario)
        {
            return await _usuarioRepository.DesativarUsuario(idUsuario);
        }

        public async Task<bool> DesativarUsuarioCPF(string CPF)
        {
            return await _usuarioRepository.DesativarUsuarioCPF(CPF);
        }

        public async Task<bool> ReativarUsuario(string CPF)
        {
            return await _usuarioRepository.ReativarUsuario(CPF);
        }
    }
}
