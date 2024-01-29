﻿using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Data.Repositories;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services
{
    public class InvestimentoService : IInvestimentoService
    {
        private readonly IInvestimentoRepository _investimentoRepository;
        private readonly ISegmentoRepository _segmentoRepository;

        public InvestimentoService(IInvestimentoRepository investimentoRepository, ISegmentoRepository segmentoRepository)
        {
            _investimentoRepository = investimentoRepository;
            _segmentoRepository = segmentoRepository;
        }

        public async Task<List<UsuarioModel>> ListaInvestimentoPorSegmentoPeloCPFUsuario(string CPF)
        {
            return await _investimentoRepository.ListaInvestimentoPorSegmentoPeloCPFUsuario(CPF);
        }

        public async Task<InvestimentoModel> RetornaInvestimentoUsuarioSegmento(int idUsuario, int idSegmento)
        {
            return await _investimentoRepository.RetornaInvestimentoUsuarioSegmento(idUsuario, idSegmento);
        }

        public async Task<InvestimentoModel> Aplicar(AplicacaoModel aplicacao, int idUsuario, int idSegmento)
        {
            if (aplicacao != null)
            {
                var investimentoModel = new InvestimentoModel();

                investimentoModel.IdUsuario = idUsuario;
                investimentoModel.IdSegmento = idSegmento;

                try
                {
                    var cadastrarInvestimentoInicial = await CadastroInvestimento(investimentoModel);

                    if (cadastrarInvestimentoInicial != 0)
                    {
                        aplicacao.IdInvestimento = cadastrarInvestimentoInicial;

                        var aplicar = await _investimentoRepository.Aplicar(aplicacao);

                        if (aplicar != 0)
                        {
                            var valoresAnteriores = RetornaInvestimentoUsuarioSegmento(idUsuario, idSegmento);

                            var saldoAnterior = valoresAnteriores.Result.Saldo;

                            var segmento = _segmentoRepository.RetornaSegmentoIdSegmento(idSegmento);

                            var percentRendimento = segmento.Result.PercentualRendimento;

                            var saldoAtual = saldoAnterior + aplicacao.Valor;
                            var valorRendimentoAtual = saldoAtual * percentRendimento;
                            var valorFinalAtual = valorRendimentoAtual;

                            investimentoModel.IdUsuario = idUsuario;
                            investimentoModel.IdSegmento = idSegmento;
                            investimentoModel.Saldo = saldoAtual;
                            investimentoModel.ValorRendimento = valorRendimentoAtual;
                            investimentoModel.ValorFinal = valorFinalAtual;

                            var atualizar = await AtualizarInvestimento(investimentoModel);

                            if (atualizar)
                                return investimentoModel;
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null!;
        }

        public async Task<InvestimentoModel> Resgatar(ResgateModel resgate, int idUsuario, int idSegmento)
        {
            if (resgate != null)
            {
                try
                {
                    var valoresAnteriores = RetornaInvestimentoUsuarioSegmento(idUsuario, idSegmento);

                    resgate.IdInvestimento = valoresAnteriores.Id;

                    var resgatar = await _investimentoRepository.Resgatar(resgate);

                    if (resgatar != 0)
                    {
                        var saldoAnterior = valoresAnteriores.Result.Saldo;

                        var segmento = _segmentoRepository.RetornaSegmentoIdSegmento(idSegmento);

                        var percentRendimento = segmento.Result.PercentualRendimento;
                        var taxaAdm = segmento.Result.TaxaAdm;

                        var saldoAtual = saldoAnterior - resgate.Valor;
                        var valorRendimentoAtual = saldoAtual * percentRendimento;
                        var valorFinalAtual = valorRendimentoAtual - (valorRendimentoAtual * taxaAdm);

                        var investimentoModel = new InvestimentoModel()
                        {
                            IdUsuario = idUsuario,
                            IdSegmento = idSegmento,
                            Saldo = saldoAtual,
                            ValorRendimento = valorRendimentoAtual,
                            ValorFinal = valorFinalAtual
                        };

                        var atualizar = await AtualizarInvestimento(investimentoModel);

                        if (atualizar)
                            return investimentoModel;
                    }
                }
                catch (Exception)
                {
                    return null!;
                }
            }

            return null!;
        }

        public async Task<int> CadastroInvestimento(InvestimentoModel investimento)
        {
            if (investimento == null)
                return 0;

            return await _investimentoRepository.CadastroInvestimento(investimento);
        }

        public async Task<bool> AtualizarInvestimento(InvestimentoModel investimento)
        {
            if (investimento == null)
                return false;

            return await _investimentoRepository.AtualizarInvestimento(investimento);
        }
    }
}
