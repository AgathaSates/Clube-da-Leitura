using System.Globalization;
using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;

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
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║    Gerenciamento de Amigos     ║");
            Console.WriteLine("║════════════════════════════════║");
            Console.WriteLine("║ 1- Cadastrar Amigo.            ║");
            Console.WriteLine("║ 2- Editar Amigo.               ║");
            Console.WriteLine("║ 3- Excluir Amigo.              ║");
            Console.WriteLine("║ 4- Visualizar todos os Amigos. ║");
            Console.WriteLine("║ 5- Visualizar Empréstimos.     ║");
            Console.WriteLine("║ 6- Voltar ao Menu Principal.   ║");
            Console.WriteLine("╚════════════════════════════════╝");
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
        if (ValidarAmigo(novoAmigo, false))
            return;

        string mensagemResultado = repositorioAmigo.Inserir(novoAmigo);

        if (mensagemResultado == "(V) Amigo cadastrado com sucesso!")
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

        if (ExisteAmigos())
            return;

        VisualizarTodosOsAmigos(false, false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ID do amigo: ", ConsoleColor.Yellow);
        int id = Convert.ToInt32(Console.ReadLine().Trim());

        if (EncontrouAmigo(id))
            return;

        Amigo amigoEditado = ObterDadosAmigo(false, id);

        if (ValidarAmigo(amigoEditado, true))
            return;

        bool editou = repositorioAmigo.Editar(id, amigoEditado);

        if (!editou)
        {
            ColorirTexto.ExibirMensagem("(X)Não foi possível editar o amigo!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        ColorirTexto.ExibirMensagem("(V) Amigo editado com sucesso!", ConsoleColor.Green);
        Notificador.ApresentarMensagemParaSair();
    }

    public void ExcluirAmigo()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║             Excluir Amigo               ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        if (ExisteAmigos())
            return;

        VisualizarTodosOsAmigos(false, false);

        ColorirTexto.ExibirMensagemSemLinha("> Digite o ID do amigo: ", ConsoleColor.Yellow);
        int id = Convert.ToInt32(Console.ReadLine().Trim());

        if (EncontrouAmigo(id))
            return;

        bool excluiu = repositorioAmigo.Excluir(id);

        if (!excluiu)
        {
            ColorirTexto.ExibirMensagem("(X) Não foi possível excluir o amigo!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        ColorirTexto.ExibirMensagem("(V) Amigo excluído com sucesso!", ConsoleColor.Green);
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

        if (ExisteAmigos())
            return;

        Console.WriteLine("╔═════╦════════════╦═════════════════╦═══════════════╗");
        Console.WriteLine("║{0, -4} ║ {1, -10} ║ {2, -15} ║ {3, -13} ║",
                             "Id", "Nome", "Responsável", "Telefone");
        Console.WriteLine("║═════╬════════════╬═════════════════╬═══════════════║");

        int contador = 0;
        Amigo[] amigos = repositorioAmigo.SelecionarTodos();

        foreach (Amigo amigo in amigos)
            if (amigo != null)
            {
                Console.WriteLine(
               "║{0, -4} ║ {1, -10} ║ {2, -15} ║ {3, -13} ║",
               amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone);

                if (contador < amigos.Length - 1)
                    Console.WriteLine("║═════╬════════════╬═════════════════╬═══════════════║");

                contador++;

            }

        Console.WriteLine("╚═════╩════════════╩═════════════════╩═══════════════╝");

        if (exibirSair)
            Notificador.ApresentarMensagemParaSair();
    }

    public void VisualizarEmprestimos()
    {
        Console.Clear();
        ColorirTexto.ExibirMensagem("╔═════════════════════════════════════════╗", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("║ Visualizando Empréstimos dos Amigos     ║", ConsoleColor.Blue);
        ColorirTexto.ExibirMensagem("╚═════════════════════════════════════════╝", ConsoleColor.Blue);
        Console.WriteLine();

        ////if (ExisteAmigos())
        ////    return;

        ////VisualizarTodosOsAmigos(false, false);

        ////ColorirTexto.ExibirMensagemSemLinha("> Digite o ID do amigo que deseja ver os empréstimos: ", ConsoleColor.Yellow);
        ////int id = Validador.DigitouUmNumero();

        ////if (EncontrouAmigo(id))
        ////    return;

        
        ////if (AmigoTemEmprestimos(id))
        ////    return;

        //Console.WriteLine("╔═════════════════════════════════════════╗");
        //Console.WriteLine($"║ Emprestimos de {repositorioAmigo.SelecionarPorId(id).Nome}║");
        //Console.WriteLine("╚═════════════════════════════════════════╝");

        //Emprestimo[] emprestimos = repositorioAmigo.VisualizarEmprestimos(id);

        //foreach (Emprestimo emprestimo in emprestimos)
        //    if (emprestimo != null)
        //    {
        //        if (emprestimo.EmprestimoEstaAtrasado(emprestimo))
        //            emprestimo.RegistrarAtraso();

        //        Console.WriteLine("╔═════╦════════════╦═════════════════╦═══════════════╗");

        //    }

        Notificador.ApresentarMensagemParaSair();
    } // terminar após o empréstimo

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

    public bool AmigoTemEmprestimos(int id)
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

    public bool EncontrouAmigo(int id)
    {

        Amigo amigo = repositorioAmigo.SelecionarPorId(id);
        if (amigo == null)
        {
            ColorirTexto.ExibirMensagem("(X) Amigo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();

            EditarAmigo();

            return true;
        }
        return false;
    }

    public bool ExisteAmigos()
    {
        Amigo[] amigos = repositorioAmigo.SelecionarTodos();
        if (amigos.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum amigo cadastrado!", ConsoleColor.Red);
            Console.WriteLine();
            Notificador.ApresentarMensagemParaSair();
            return true;

        }
        return false;
    }

    public bool ValidarAmigo(Amigo novoAmigo, bool ehEditar)
    {
        if (novoAmigo.Validar() != "")
        {
            ApresentarDadosInvalidos(novoAmigo);

            if (ehEditar)
                EditarAmigo();

            InserirAmigo();

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
