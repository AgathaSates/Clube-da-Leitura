using System.Drawing;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
class TelaEmprestimo
{
    public RepositorioEmprestimo repositorioEmprestimo;
    public RepositorioAmigo repositorioAmigo;
    public RepositorioRevista repositorioRevista;
    public TelaAmigo telaAmigo;
    public TelaRevista telaRevista;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, TelaAmigo telaAmigo, TelaRevista telaRevista)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.telaAmigo = telaAmigo;
        this.telaRevista = telaRevista;
    }

    internal void ApresentarMenu()
    {
        while (true)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║     Gerenciamento de Empréstimos     ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║══════════════════════════════════════║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 1- Cadastrar Empréstimo.             ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 2- Editar Empréstimo.                ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 3- Excluir Empréstimo.               ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 4- Registrar Devolução.              ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 5- Visualizar todos os Empréstimos.  ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 6- Voltar ao Menu Principal.         ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
            string opcaoMenu = Console.ReadLine().Trim();

            switch (opcaoMenu)
            {
                case "1": InserirEmprestimo(); break;

                case "2": EditarEmprestimo(); break;

                case "3": ExcluirEmprestimo(); break;

                case "4": RegistrarDevolucao(); break;

                case "5": VisualizarTodosOsEmprestimos(true, true); break;

                case "6": return;

                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
    }

    public void InserirEmprestimo()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║         Cadastrar Empréstimo         ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

        if (!telaAmigo.ExisteAmigos())
            return;

        if (!telaRevista.ExisteRevistas())
            return;

        Emprestimo novoEmprestimo = ObterDadosDoEmprestimo(true);

        if (NãoConseguiuValidarEmprestimo(novoEmprestimo))
            return;

        string mensagemrResultado = repositorioEmprestimo.Inserir(novoEmprestimo);

        if (mensagemrResultado == "(V) Empréstimo cadastrado com sucesso!")
        {
            ColorirTexto.ExibirMensagem(mensagemrResultado, ConsoleColor.Green);
            Notificador.ApresentarMensagemParaSair();
        }

        else
        {
            ColorirTexto.ExibirMensagem(mensagemrResultado, ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
        }
    }

    public void EditarEmprestimo()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║         Editar Empréstimo            ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

        if (!ExisteEmprestimos())
            return;

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ID do Empréstimo que deseja editar: ", ConsoleColor.Yellow);
        VisualizarTodosOsEmprestimos(false, false);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouEmprestimo(id, false, false))
            return;

        Emprestimo emprestimoEditado = ObterDadosDoEmprestimo(false, id);

        if (NãoConseguiuValidarEmprestimo(emprestimoEditado))
            return;

        bool editou = repositorioEmprestimo.Editar(id, emprestimoEditado);

        if (!editou)
        {
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível editar o Empréstimo, pois o amigo possui um empréstimo ativo ", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
        }

        else
        {
            ColorirTexto.ExibirMensagem("(V) Empréstimo editado com sucesso!", ConsoleColor.Green);
            Notificador.ApresentarMensagemParaSair();
        }
    }

    public void ExcluirEmprestimo()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║         Excluir Empréstimo           ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

        if (!ExisteEmprestimos())
            return;

        VisualizarTodosOsEmprestimos(false, false);
        ColorirTexto.ExibirMensagemSemLinha("> Digite o Id do empréstimo que deseja excluir: ", ConsoleColor.Yellow);
        int idEmprestimo = Validador.DigitouUmNumero();

        if (NaoEncontrouEmprestimo(idEmprestimo, false, true))
            return;

        bool excluiu = repositorioEmprestimo.Excluir(idEmprestimo);

        if (!excluiu)
        {
            ColorirTexto.ExibirMensagem(">> (X) Não é possível excluir o Empréstimo, pois ele não está concluído.", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
        }
        else
        {
            ColorirTexto.ExibirMensagem("(V) Empréstimo excluído com sucesso!", ConsoleColor.Green);
            Notificador.ApresentarMensagemParaSair();
        }
    }

    public void VisualizarTodosOsEmprestimos(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║   Visualizando todos os Empréstimos  ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);
        }

        if (!ExisteEmprestimos())
            return;

        Console.WriteLine("╔═════╦═══════════════════════╦═════════════════╦══════════════════════╦══════════════════════╦═══════════════════╗");
        Console.WriteLine("║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4, -20} ║ {5, -15} ║",
                          "Id", "Amigo", "Revista", "Data do Empréstimo", "Status de Emprestimo", "Data de Devolução");
        Console.WriteLine("╠═════╬═══════════════════════╬═════════════════╬══════════════════════╬══════════════════════╬═══════════════════╣");

        int contador = 0;
        Emprestimo[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        foreach (Emprestimo emprestimo in emprestimos)
        {
            if (emprestimo != null)
            {
                if (emprestimo.EmprestimoEstaAtrasado(emprestimo))
                { }

                string linhaCompleta = string.Format(
                    "║{0, -4} ║ {1,-21} ║ {2,-15} ║ {3,-20} ║ {4,-20} ║ {5,-15}   ║",
                    emprestimo.Id,
                    emprestimo.amigo.Nome,
                    emprestimo.revista.Titulo,
                    emprestimo.DataIniciodoEmprestimo.ToString("dd/MM/yyyy"),
                    emprestimo.StatusDeEmprestimo,
                    emprestimo.DataDevolucao.ToString("dd/MM/yyyy")
                );

                if (emprestimo.StatusDeEmprestimo == "Atrasado")
                {
                    ColorirTexto.ExibirMensagem(linhaCompleta, ConsoleColor.Red);
                }
                else
                {
                    Console.WriteLine(linhaCompleta);
                }

                contador++;
            }
        }

        Console.WriteLine("╚═════╩═══════════════════════╩═════════════════╩══════════════════════╩══════════════════════╩═══════════════════╝");

        if (exibirSair)
        {
            Notificador.ApresentarMensagemParaSair();
        }

    }

    public void RegistrarDevolucao()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔══════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║         Registrar Devolução          ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚══════════════════════════════════════╝", ConsoleColor.Blue);

        if (!ExisteEmprestimos())
            return;

        VisualizarTodosOsEmprestimos(false, false);

        ColorirTexto.ExibirMensagem("> Digite o Id do Empréstimo que deseja registar a devolução: ", ConsoleColor.Yellow);
        int idEmprestimo = Validador.DigitouUmNumero();

        if (NaoEncontrouEmprestimo(idEmprestimo, false, false))
            return;

        Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idEmprestimo);
        emprestimoSelecionado.RegistrarDevolucao(emprestimoSelecionado.revista);

        ColorirTexto.ExibirMensagem("(V) Devolução registrada com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public Emprestimo ObterDadosDoEmprestimo(bool CriarIdNovo, int idExistente = 0)
    {
        telaAmigo.VisualizarTodosOsAmigos(true, false);
        ColorirTexto.ExibirMensagemSemLinha("> Selecione o amigo: ", ConsoleColor.Yellow);
        int idAmigo = Validador.DigitouUmNumero();

        Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

        telaRevista.VisualizarTodasAsRevistas(true, false);
        ColorirTexto.ExibirMensagemSemLinha("Selecione a revista: ", ConsoleColor.Yellow);
        int idRevista = Validador.DigitouUmNumero();

        Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

        if (CriarIdNovo)
            return new Emprestimo(amigoSelecionado, revistaSelecionada, DateTime.Now);

        return new Emprestimo(idExistente, amigoSelecionado, revistaSelecionada, DateTime.Now);
    }

    public bool ExisteEmprestimos()
    {
        if (repositorioEmprestimo.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum Empréstimo cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return false;
        }
        return true;
    }

    public bool NaoEncontrouEmprestimo(int id, bool ehEditar, bool ehExcluir)
    {
        if (repositorioEmprestimo.SelecionarPorId(id) == null)
        {
            ColorirTexto.ExibirMensagem("Empréstimo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();
            if (ehEditar)
                EditarEmprestimo();

            if (ehExcluir)
                ExcluirEmprestimo();
        }
        return false;
    }

    public bool NãoConseguiuValidarEmprestimo(Emprestimo novoEmprestimo)
    {
        if (novoEmprestimo.Validar() != "")
        {
            ApresentarDadosInvalidos(novoEmprestimo);
            return true;
        }
        return false;
    }

    public void ApresentarDadosInvalidos(Emprestimo novoEmprestimo)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Empréstimo!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagem(novoEmprestimo.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }
}
