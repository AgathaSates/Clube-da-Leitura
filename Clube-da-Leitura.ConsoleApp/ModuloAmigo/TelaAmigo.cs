using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloAmigo;

public class TelaAmigo : TelaBase<Amigo>, ITelaCrud
{
    public RepositorioAmigo repositorioAmigo;
    string opcaoMenu;
    public TelaAmigo(RepositorioAmigo repositorioAmigo) : base("Amigo", repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    public string ApresentarMenu()
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
            opcaoMenu = Console.ReadLine()!.Trim();
        }       
    }

    public void ExecutarOpcao(string opcao)
    {
        switch (opcaoMenu)
        {
            case "1": CadastrarRegistro(1); break;

            case "2": EditarRegistro(2); break;

            case "3": ExcluirRegistro(3); break;

            case "4": VisualizarRegistros(true, true); break;

            case "5": VisualizarEmprestimos(); break;

            case "6": return;

            default: Notificador.ApresentarOpcaoInvalida(); break;
        }
    }

    public override void VisualizarRegistros(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("║     Visualizando Amigos Cadastrados     ║", ConsoleColor.Blue);
            ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
            Console.WriteLine();
        }

        if (!ExisteRegistros())
            return;

        Console.WriteLine();
        Console.WriteLine("╔═════╦═════════════════╦═════════════════╦═══════════════╗");
        Console.WriteLine("║{0, -4} ║ {1, -15} ║ {2, -15} ║ {3, -13} ║",
                             "Id", "Nome", "Responsável", "Telefone");
        Console.WriteLine("║═════╬═════════════════╬═════════════════╬═══════════════║");

        int contador = 0;
        List<Amigo> amigos = repositorioAmigo.SelecionarTodosRegistros();

        foreach (Amigo amigo in amigos)
            if (amigo != null)
            {
                Console.WriteLine(
               "║{0, -4} ║ {1, -15} ║ {2, -15} ║ {3, -13} ║",
               amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone);

                if (contador < amigos.Count - 1)
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

        if (!ExisteRegistros())
            return;

        VisualizarRegistros(false, false);
        ColorirTexto.ExibirMensagemSemLinha("> Digite o Id do amigo que deseja visualizar os empréstimos: ", ConsoleColor.Yellow);
        int id = Validador.DigitouUmNumero();

        if (NãoEncontrouRegistro(id))
            return;

        if (AmigoNaoTemEmprestimos(id))
            return;

        Amigo amigo = repositorioAmigo.SelecionarRegistroPorId(id);
        List <Emprestimo> emprestimos = amigo.ObterEmprestimos();

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
                if (contador < emprestimos.Count - 1)
                    Console.WriteLine("║═════╬═════════════════╬═════════════════╬═══════════════║");
                contador++;
            }
        Console.WriteLine("╚═════╩═════════════════╩═════════════════╩═══════════════╝");
        Notificador.ApresentarMensagemParaSair();
    }

    public override Amigo ObterDadosDoRegistro(bool criarIdNovo, int idExistente = 0)
    {
        ColorirTexto.ExibirMensagemSemLinha("> Digite o Nome do Amigo: ", ConsoleColor.Yellow);
        string nome = Console.ReadLine()!.Trim();
        nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o Nome do Responsável: ", ConsoleColor.Yellow);
        string nomeResponsavel = Console.ReadLine()!.Trim();
        nomeResponsavel = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomeResponsavel);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o Telefone do Responsável ( 11 digitos (XX)XXXXX-XXXX): ", ConsoleColor.Yellow);
        string telefone = Console.ReadLine()!.Trim();

        if (criarIdNovo)
            return new Amigo(nome, nomeResponsavel, telefone);

        return new Amigo(idExistente, nome, nomeResponsavel, telefone);
    }

    public bool AmigoNaoTemEmprestimos(int id)
    {
        List <Emprestimo> emprestimos;
        emprestimos = repositorioAmigo.VisualizarEmprestimos(id);
        if (emprestimos.Count == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum empréstimo encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
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

    public override bool NãoConseguiuValidar(Amigo amigo)
    {
        if (amigo.Validar() != "")
        {
            ApresentarDadosInvalidos(amigo);
            return true;
        }
        return false;
    }

    public override void ApresentarTitulo(int numeroDoTitulo)
    {
       switch(numeroDoTitulo)
        {
            case 1:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Cadastrar Amigo             ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            case 2:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Editar Amigo                ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            case 3:
                Console.Clear();
                ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("║             Excluir Amigo               ║", ConsoleColor.Blue);
                ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
                Console.WriteLine();
                break;
            default: ColorirTexto.ExibirMensagem(">> Nenhum título encontrado!", ConsoleColor.Red); break;
        }
    }
}
