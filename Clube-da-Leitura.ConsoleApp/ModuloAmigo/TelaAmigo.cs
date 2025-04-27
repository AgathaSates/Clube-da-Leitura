using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

class TelaAmigo
{
    public RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    public void ApresentarMenu()
    {
        while (true)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔════════════════════════════════╗", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║    Gerenciamento de Amigos     ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║════════════════════════════════║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 1- Cadastrar Amigo.            ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 2- Editar Amigo.               ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 3- Excluir Amigo.              ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 4- Visualizar todos os Amigos. ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 5- Visualizar Empréstimos.     ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("║ 6- Voltar ao Menu Principal.   ║", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagem("╚════════════════════════════════╝", ConsoleColor.DarkCyan);
            ColorirTexto.ExibirMensagemSemLinha("> Digite uma opção: ", ConsoleColor.Yellow);
            string opcaoMenu = Console.ReadLine().Trim();

            switch (opcaoMenu)
            {
                case "1": InserirAmigo(); break;

                case "2": EditarAmigo(); break;

                case "3": ExcluirAmigo(); break;

                case "4": VisualizarTodosOsAmigos(true, true); break;

                case "5": VisualizarEmprestimos(); break;

                case "6": return;

                default: Notificador.ApresentarOpcaoInvalida(); break;
            }
        }
    }

    public void InserirAmigo()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Cadastrar Amigo             ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        Amigo novoAmigo = ObterDadosAmigo(true);

        if (NaoConseguiuValidarAmigo(novoAmigo))
        {
            InserirAmigo();
            return;
        }

        string mensagemResultado = repositorioAmigo.Inserir(novoAmigo);

        if (mensagemResultado == ">> (V) Amigo cadastrado com sucesso!")
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Green);
        else
            ColorirTexto.ExibirMensagem(mensagemResultado, ConsoleColor.Red);

        Notificador.ApresentarMensagemParaSair();
    }

    public void EditarAmigo()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Editar Amigo                ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!ExisteAmigos())
            return;

        VisualizarTodosOsAmigos(false, false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ID do amigo que deseja editar: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouAmigo(id))
            return;

        Amigo amigoEditado = ObterDadosAmigo(false, id);

        if (NaoConseguiuValidarAmigo(amigoEditado))
        {
            EditarAmigo();
            return;
        }

        bool editou = repositorioAmigo.Editar(id, amigoEditado);

        if (!editou)
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível editar o amigo!", ConsoleColor.Red);

        else
            ColorirTexto.ExibirMensagem(">> (V) Amigo editado com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public void ExcluirAmigo()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Excluir Amigo               ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!ExisteAmigos())
            return;

        VisualizarTodosOsAmigos(false, false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o Id do amigo que deseja excluir: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouAmigo(id))
            return;

        bool excluiu = repositorioAmigo.Excluir(id);

        if (!excluiu)
            ColorirTexto.ExibirMensagem(">> (X) Não foi possível excluir o amigo, pois ele possui um empréstimo ativo", ConsoleColor.Red);

        else
            ColorirTexto.ExibirMensagem(">> (V) Amigo excluído com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public void VisualizarTodosOsAmigos(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║     Visualizando Amigos Cadastrados     ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
            Console.WriteLine();
        }

        if (!ExisteAmigos())
            return;

        Console.WriteLine();
        Console.WriteLine("╔═════╦═════════════════╦═════════════════╦═══════════════╗");
        Console.WriteLine("║{0, -4} ║ {1, -15} ║ {2, -15} ║ {3, -13} ║",
                             "Id", "Nome", "Responsável", "Telefone");
        Console.WriteLine("║═════╬═════════════════╬═════════════════╬═══════════════║");

        int contador = 0;
        Amigo[] amigos = repositorioAmigo.SelecionarTodos();

        foreach (Amigo amigo in amigos)
            if (amigo != null)
            {
                Console.WriteLine(
               "║{0, -4} ║ {1, -15} ║ {2, -15} ║ {3, -13} ║",
               amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone);

                if (contador < amigos.Length - 1)
                    Console.WriteLine("║═════╬═════════════════╬═════════════════╬═══════════════║");

                contador++;
            }

        Console.WriteLine("╚═════╩═════════════════╩═════════════════╩═══════════════╝");
        Console.WriteLine();

        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    } 

    public void VisualizarEmprestimos()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║   Visualizando Empréstimos dos Amigos   ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (!ExisteAmigos())
            return;

        VisualizarTodosOsAmigos(false, false);
        ColorirTexto.ExibirMensagemSemLinha("> Digite o Id do amigo que deseja visualizar os empréstimos: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NaoEncontrouAmigo(id))
            return;

        if (AmigoNaoTemEmprestimos(id))
            return;

        Amigo amigo = repositorioAmigo.SelecionarPorId(id);
        Emprestimo[] emprestimos = amigo.ObterEmprestimos();

        Console.WriteLine();
        Console.WriteLine("╔═════╦═════════════════╦═════════════════╦═══════════════╗");
        Console.WriteLine("║{0, -4} ║ {1, -15} ║ {2, -15} ║ {3, -13} ║",
                             "Id", "Nome", "Revista", "Data de Devolução");
        Console.WriteLine("║═════╬═════════════════╬═════════════════╬═══════════════║");
        int contador = 0;
        foreach (Emprestimo emprestimo in emprestimos)
            if (emprestimo != null)
            {
                Console.WriteLine(
               "║{0, -4} ║ {1, -15} ║ {2, -15} ║ {3, -13} ║",
               emprestimo.Id, emprestimo.amigo.Nome, emprestimo.revista.Titulo, emprestimo.DataDevolucao.ToShortDateString());
                if (contador < emprestimos.Length - 1)
                    Console.WriteLine("║═════╬═════════════════╬═════════════════╬═══════════════║");
                contador++;
            }
        Console.WriteLine("╚═════╩═════════════════╩═════════════════╩═══════════════╝");
        Notificador.ApresentarMensagemParaSair();
    }

    public Amigo ObterDadosAmigo(bool criarIdNovo, int idExistente = 0)
    {
        ColorirTexto.ExibirMensagemSemLinha("> Digite o Nome do Amigo: ", ConsoleColor.Yellow);
        string nome = Console.ReadLine().Trim();
        nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o Nome do Responsável: ", ConsoleColor.Yellow);
        string nomeResponsavel = Console.ReadLine().Trim();
        nomeResponsavel = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomeResponsavel);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o Telefone do Responsável ( 11 digitos (XX)XXXXX-XXXX): ", ConsoleColor.Yellow);
        string telefone = Console.ReadLine().Trim();

        if (criarIdNovo)
            return new Amigo(nome, nomeResponsavel, telefone);

        return new Amigo(idExistente, nome, nomeResponsavel, telefone);
    }

    public bool AmigoNaoTemEmprestimos(int id)
    {
        Emprestimo[] emprestimos;
        emprestimos = repositorioAmigo.VisualizarEmprestimos(id);
        if (emprestimos.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum empréstimo encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return true;
        }
        return false;
    }

    public bool ExisteAmigos()
    {
        if (repositorioAmigo.SelecionarTodos().Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum amigo cadastrado!", ConsoleColor.Red);
            Console.WriteLine();
            Notificador.ApresentarMensagemParaSair();
            return false;
        }
        return true;
    }

    public bool  NaoEncontrouAmigo(int id)
    {
        if (repositorioAmigo.SelecionarPorId(id) == null)
        {
            ColorirTexto.ExibirMensagem("(X) Amigo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return true;
        }
        return false;
    }

    public bool NaoConseguiuValidarAmigo(Amigo novoAmigo)
    {
        if (novoAmigo.Validar() != "")
        {
            ApresentarDadosInvalidos(novoAmigo);
            return true;
        }
        return false;
    }

    public void ApresentarDadosInvalidos(Amigo novoAmigo)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Amigo!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagemSemLinha(novoAmigo.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }
}
