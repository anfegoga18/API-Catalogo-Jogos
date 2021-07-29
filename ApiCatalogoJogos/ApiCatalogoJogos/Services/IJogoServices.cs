using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public interface IJogoServices : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        
        Task<JogoViewModel> Obter(Guid id);

        Task<JogoViewModel> InserirJogo(JogoInputModel jogo);

        Task AtualizarJogo(Guid id, JogoInputModel jogo);

        Task AtualizarJogo(Guid id, double preco);

        Task ApagarJogo(Guid id);
        
    }
}
