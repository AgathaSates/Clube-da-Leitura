using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloReserva
{
    class TelaReserva
    {
        public RepositorioReserva repositorioReserva;
        public RepositorioRevista repositorioRevista;
        public RepositorioCaixa repositorioCaixa;

        public TelaReserva(RepositorioReserva repositorioReserva, RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa)
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
        }

        internal void ApresentarMenu()
        {
            throw new NotImplementedException();
        }
    }
}
