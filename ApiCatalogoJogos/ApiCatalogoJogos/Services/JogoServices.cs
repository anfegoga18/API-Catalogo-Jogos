using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class JogoServices : IJogoServices
    {
        public readonly IJogoRepository jogoRepository;

        public JogoServices(IJogoRepository jogoRepository)
        {
            this.jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await jogoRepository.Obter(id);

            if (jogo == null)
            {
                return null;
            }

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> InserirJogo(JogoInputModel jogo)
        {
            var entidadeJogo = await jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await jogoRepository.InserirJogo(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task AtualizarJogo(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await jogoRepository.AtualizarJogo(entidadeJogo);
        }

        public async Task AtualizarJogo(Guid id, double preco)
        {
            var entidadeJogo = await jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await jogoRepository.AtualizarJogo(entidadeJogo);
        }

        public async Task ApagarJogo(Guid id)
        {
            var jogo = await jogoRepository.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            await jogoRepository.ApagarJogo(id);
        }

        public void Dispose()
        {
            jogoRepository?.Dispose();
        }
    }
}
