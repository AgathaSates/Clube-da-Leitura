using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloReserva
{
    public class TelaReserva : TelaBase<Reserva>, ITela
    {
        public RepositorioAmigo repositorioAmigo;
        public RepositorioReserva repositorioReserva;
        public RepositorioRevista repositorioRevista;
        public RepositorioCaixa repositorioCaixa;

        public TelaAmigo telaAmigo;
        public TelaRevista telaRevista;
        string opcaoMenu;

        public TelaReserva(RepositorioAmigo repositorioAmigo, RepositorioReserva repositorioReserva, RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, TelaAmigo telaAmigo, TelaRevista telaRevista) : base("Reserva", repositorioReserva)
        {
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioReserva = repositorioReserva;
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;

            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
        }

        public string ApresentarMenu()
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
                return opcaoMenu = Console.ReadLine().Trim();
            }
        }

        public void ExecutarOpcao(string opcao)
        {
            switch (opcao)
            {
                case "1": CadastrarRegistro(1); break;

                case "2": ExcluirRegistro(2); break;

                case "3": VisualizarRegistros(true, true); break;

                case "4": return;

                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
        public override void CadastrarRegistro(int numeroDoTitulo)
        {
            ApresentarTitulo(numeroDoTitulo);

            if (!telaAmigo.ExisteRegistros())
                return;

            if (!telaRevista.ExisteRegistros())
                return;

            Reserva novaReserva = ObterDadosDoRegistro(true);

            if (NaoConseguiuValidar(novaReserva))
            {
                CadastrarRegistro(numeroDoTitulo);
                return;
            }

            string mensagemResultado = repositorioReserva.CadastrarRegistro(novaReserva);
            ConseguiuCadastrar(mensagemResultado);

            Notificador.ApresentarMensagemParaSair();
        }

        public override void ExcluirRegistro(int numeroDoTitulo)
        {
            ApresentarTitulo(numeroDoTitulo);

            if (!ExisteReservas(true))
                return;

            VisualizarRegistros(false, false);
            ColorirTexto.ExibirMensagemSemLinha("> Digite o Id da reserva que deseja cancelar: ", ConsoleColor.Yellow);
            int idReserva = Validador.DigitouUmNumero();

            if (NaoEncontrouRegistro(idReserva))
                return;

            Reserva reserva = repositorioReserva.SelecionarRegistroPorId(idReserva);
            reserva.ConcluirReserva();
            reserva.Revista.Devolver();

            ColorirTexto.ExibirMensagem(">> Reserva Cancelada!", ConsoleColor.Green);

            Notificador.ApresentarMensagemParaSair();
        }

        public override void VisualizarRegistros(bool exibirTitulo, bool exibirSair)
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
            List<Reserva> reservas = repositorioReserva.SelecionarTodosRegistros();

            foreach (Reserva reserva in reservas)
            {
                if (reserva != null)

                    if (reserva != null)
                    {
                        Console.WriteLine("║{0, -4} ║ {1, -21} ║ {2, -15} ║ {3, -20} ║ {4, -20} ║",
                        reserva.Id, reserva.Amigo.Nome, reserva.Revista.Titulo, reserva.DataReserva, reserva.Status);

                        if (contador < reservas.Count - 1)
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

        public override Reserva ObterDadosDoRegistro(bool criarIdNovo, int idExistente = 0)
        {
            telaAmigo.VisualizarRegistros(true, false);
            ColorirTexto.ExibirMensagemSemLinha("> Selecione o amigo: ", ConsoleColor.Yellow);
            int idAmigo = Validador.DigitouUmNumero();

            Amigo amigoSelecionado = repositorioAmigo.SelecionarRegistroPorId(idAmigo);

            telaRevista.VisualizarRegistros(true, false);
            ColorirTexto.ExibirMensagemSemLinha("Selecione a revista: ", ConsoleColor.Yellow);
            int idRevista = Validador.DigitouUmNumero();

            Revista revistaSelecionada = repositorioRevista.SelecionarRegistroPorId(idRevista);

            return new Reserva(amigoSelecionado, revistaSelecionada);
        }


        public override bool NaoConseguiuValidar(Reserva novaReserva)
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
            if (repositorioReserva.SelecionarTodosRegistros().Count == 0)
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

        public override void ApresentarTitulo(int numeroDoTitulo)
        {

            switch (numeroDoTitulo)
            {
                case 1:
                    Console.Clear();
                    ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
                    ColorirTexto.ExibirMensagem("║         Cadastrar Reserva            ║", ConsoleColor.Blue);
                    ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
                    Console.WriteLine();
                    break;
                case 2:
                    Console.Clear();
                    ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
                    ColorirTexto.ExibirMensagem("║         Cancelar Reserva             ║", ConsoleColor.Blue);
                    ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
                    Console.WriteLine();
                    break;
                default: ColorirTexto.ExibirMensagem(">> Nenhum título encontrado!", ConsoleColor.Red); break;
            }
        }
    }
}
