using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloReserva
{
    class TelaReserva
    {
        public RepositorioAmigo repositorioAmigo;
        public RepositorioReserva repositorioReserva;
        public RepositorioRevista repositorioRevista;
        public RepositorioCaixa repositorioCaixa;

        public TelaAmigo telaAmigo;
        public TelaRevista telaRevista;

        public TelaReserva(RepositorioAmigo repositorioAmigo, RepositorioReserva repositorioReserva, RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, TelaAmigo telaAmigo, TelaRevista telaRevista)
        {
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioReserva = repositorioReserva;
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;

            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
        }

        internal void ApresentarMenu()
        {
            while (true)
            {
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagem("║     Gerenciamento de Reservas        ║", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagem("║══════════════════════════════════════║", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagem("║ 1- Cadastrar Reserva.                ║", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagem("║ 2- Excluir Reserva.                  ║", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagem("║ 3- Visualizar Reservas.              ║", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagem("║ 4- Voltar ao Menu Principal.         ║", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.DarkCyan);
                ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
                string opcaoMenu = Console.ReadLine().Trim();

                switch (opcaoMenu)
                {
                    case "1": InserirReserva(); break;

                    case "2": ExcluirReserva(); break;

                    case "3": VisualizarTodasAsReservas(true, true); break;

                    case "4": return;

                    default: Notificador.ApresentarOpcaoInvalida(); break;
                }
            }
        }

        public void InserirReserva()
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║         Cadastrar Reserva            ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

            if (!telaAmigo.ExisteAmigos())
                return;

            if (!telaRevista.ExisteRevistas())
                return;

            Reserva novaReserva = ObterDadosDaReserva(true);

            if (NaoConseguiuValidarReserva(novaReserva))
            {
                InserirReserva();
                return;
            }

            string mensagemResultado = repositorioReserva.Inserir(novaReserva);

            if (mensagemResultado == ">> (V) Reserva cadastrada com sucesso!")
                ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Green);

            else
                ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Red);

            Notificador.ApresentarMensagemParaSair();
        }

        public void ExcluirReserva()
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║         Excluir Reserva              ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

            if (!ExisteReservas(true))
                return;

            VisualizarTodasAsReservas(false, false);
            ColorirTexto.ExibirMensagemSemLinha("> Digite o Id da reserva que deseja excluir: ", ConsoleColor.Yellow);
            int idReserva = Validador.DigitouUmNumero();

            if (NaoEncontrouReserva(idReserva))
                return;

            bool excluiu = repositorioReserva.Excluir(idReserva);

            if (!excluiu)
                ColorirTexto.ExibirMensagem(">> (X) Não é possível excluir a Reserva.", ConsoleColor.Red);

            else
                ColorirTexto.ExibirMensagem(">> (V) Reserva excluído com sucesso!", ConsoleColor.Green);

            Notificador.ApresentarMensagemParaSair();
        }

        private bool NaoEncontrouReserva(int idReserva)
        {
            if (repositorioReserva.SelecionarPorId(idReserva) == null)
            {
                ColorirTexto.ExibirMensagem("Reserva não encontrada!", ConsoleColor.Red);
                Notificador.ApresentarMensagemParaSair();
                return true;
            }
            return false;
        }

        public Reserva ObterDadosDaReserva(bool criarIdNovo, int idExistente = 0)
        {
            telaAmigo.VisualizarTodosOsAmigos(true, false);
            ColorirTexto.ExibirMensagemSemLinha("> Selecione o amigo: ", ConsoleColor.Yellow);
            int idAmigo = Validador.DigitouUmNumero();

            Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

            telaRevista.VisualizarTodasAsRevistas(true, false);
            ColorirTexto.ExibirMensagemSemLinha("Selecione a revista: ", ConsoleColor.Yellow);
            int idRevista = Validador.DigitouUmNumero();

            Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

            return new Reserva(amigoSelecionado, revistaSelecionada);
        }

        public void VisualizarTodasAsReservas(bool exibirTitulo, bool exibirSair)
        {
            if (exibirTitulo)
            {
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║   Visualizando todas as Reservas     ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
            }

            if (!ExisteReservas(true))
                return;

            Console.WriteLine("╔═════╦═══════════════════════╦═════════════════╦══════════════════════╦══════════════════════╗");
            Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║",
                              "Id", "Amigo", "Revista", "Data da Reserva", "Status da Reserva");
            Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════════════╣");
            ;


            int contador = 0;
            Reserva[] reservas = repositorioReserva.SelecionarTodos();

            foreach (Reserva reserva in reservas)
            {
                if (reserva != null)

                    if (reserva != null)
                    {
                        Console.WriteLine("║{0, -4} ║ {1, -21} ║ {2, -15} ║ {3, -20} ║ {4, -20} ║",
                        reserva.Id, reserva.Amigo.Nome, reserva.Revista.Titulo, reserva.DataReserva, reserva.Status);

                        if (contador < reservas.Length - 1)
                        {
                            Console.WriteLine("║═════╬══════════════════════╬════════════╬════════════════════╬═════════════════║");
                            contador++;
                        }
                    }

            }

            Console.WriteLine("╚═════╩═══════════════════════╩═════════════════╩══════════════════════╩══════════════════════╝");

            if (exibirSair)
                Notificador.ApresentarMensagemParaSair();
        }

        public bool NaoConseguiuValidarReserva(Reserva novaReserva)
        {
            if (novaReserva.Validar() != "")
            {
                ApresentarDadosInvalidos(novaReserva);
                return true;
            }
            return false;
        }

        public bool ExisteReservas(bool exibirMensagem)
        {
            if (repositorioReserva.SelecionarTodos().Length == 0)
            {
                if (exibirMensagem)
                {
                    ColorirTexto.ExibirMensagem("(X) Nenhuma Reserva cadastrada !", ConsoleColor.Red);
                    Notificador.ApresentarMensagemParaSair();
                }
                return false;
            }
            return true;
        }

        public void ApresentarDadosInvalidos(Reserva novaReserva)
        {
            ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Reserva!", ConsoleColor.Red);
            ColorirTexto.ExibirMensagem(novaReserva.Validar(), ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();
        }
    }
}
