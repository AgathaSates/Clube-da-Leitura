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
            Console.Write("> Digite uma opção: ");
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
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Cadastrar Amigo             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();

        Amigo novoAmigo = ObterDadosAmigo(true);

        if (novoAmigo.Validar() != "")
        {
            ApresentarDadosInvalidos(novoAmigo);

            InserirAmigo();

            return;
        }

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
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Editar Amigo                ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        VisualizarTodosOsAmigos(false, false);
        Console.WriteLine();
        Console.Write("> Digite o ID do amigo: ");
        int id = Convert.ToInt32(Console.ReadLine().Trim());

        Amigo amigoExiste = repositorioAmigo.SelecionarPorId(id);

        if (amigoExiste == null)
        {
            ColorirTexto.ExibirMensagem("(X) Amigo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();

            EditarAmigo();

            return;
        }

        Amigo amigoEditado = ObterDadosAmigo(false, id);

        if (amigoEditado.Validar() != "")
        {
            ApresentarDadosInvalidos(amigoEditado);

            EditarAmigo();

            return;
        }

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
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Excluir Amigo               ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        VisualizarTodosOsAmigos(false, false);

        Console.Write("> Digite o ID do amigo: ");
        int id = Convert.ToInt32(Console.ReadLine().Trim());

        Amigo amigo = repositorioAmigo.SelecionarPorId(id);
        if (amigo == null)
        {
            ColorirTexto.ExibirMensagem("(X) Amigo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();
            ExcluirAmigo();
            return;
        }

        repositorioAmigo.Excluir(amigo.Id);
        ColorirTexto.ExibirMensagem("(V) Amigo excluído com sucesso!", ConsoleColor.Green);

        Notificador.ApresentarMensagemParaSair();
    }

    public void VisualizarTodosOsAmigos(bool exibirTitulo, bool exibirSair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║     Visualizando Amigos Cadastrados     ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
            Console.WriteLine();
        }

        Amigo[] amigos = repositorioAmigo.SelecionarTodos();

        if (amigos.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum amigo cadastrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("╔═════╦════════════╦═════════════════╦═══════════════╗");
        Console.WriteLine("║{0, -4} ║ {1, -10} ║ {2, -15} ║ {3, -13} ║",
                             "Id", "Nome", "Responsável", "Telefone");
        Console.WriteLine("║═════╬════════════╬═════════════════╬═══════════════║");

        int contador = 0;

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
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║ Visualizando Empréstimos dos Amigos     ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
        Console.WriteLine();
        VisualizarTodosOsAmigos(false, false);
        Console.WriteLine();
        Console.Write("> Digite o ID do amigo que deseja ver os empréstimos: ");
        int id = Validador.DigitouUmNumero();

        Amigo amigo = repositorioAmigo.SelecionarPorId(id);
        if (amigo == null)
        {
            ColorirTexto.ExibirMensagem("(X) Amigo não encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemTenteNovamente();
            VisualizarEmprestimos();
            return;
        }

        Emprestimo[] emprestimos = repositorioAmigo.VisualizarEmprestimos(amigo);

        if (emprestimos.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum empréstimo encontrado!", ConsoleColor.Red);
            Notificador.ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("=========================================");
        Console.WriteLine($" Emprestimos de {amigo.Nome}");
        Console.WriteLine("=========================================");


        foreach (Emprestimo emprestimo in emprestimos)
            if (emprestimo != null)
            {
                if (emprestimo.EmprestimoEstaAtrasado(emprestimo))
                    emprestimo.RegistrarAtraso();

                if (emprestimo.StatusDeEmprestimo == "Atrasado")
                {
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    Console.WriteLine($"Id: {emprestimo.Id}");
                    Console.WriteLine($"Revista: {emprestimo.revista.Titulo}");
                    Console.WriteLine($"Data do Empréstimo: {emprestimo.DataIniciodoEmprestimo}");
                    Console.WriteLine($"Data de Devolução: {emprestimo.DataDevolucao}");
                    ColorirTexto.ExibirMensagem($"Status do Empréstimo: {emprestimo.StatusDeEmprestimo}",ConsoleColor.Red);
                    Console.WriteLine("----------------------------------------------------------------------------------");

                }

                else
                {
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    Console.WriteLine($"Id: {emprestimo.Id}");
                    Console.WriteLine($"Revista: {emprestimo.revista.Titulo}");
                    Console.WriteLine($"Data do Empréstimo: {emprestimo.DataIniciodoEmprestimo}");
                    Console.WriteLine($"Data de Devolução: {emprestimo.ObterDataDeDevolucao()}");
                    ColorirTexto.ExibirMensagem($"Status do Empréstimo: {emprestimo.StatusDeEmprestimo}", ConsoleColor.Green);
                    Console.WriteLine("----------------------------------------------------------------------------------");

                }

            }

        Notificador.ApresentarMensagemParaSair();
    }

    public Amigo ObterDadosAmigo(bool criarIdNovo, int idExistente = 0)
    {
        Console.Write("> Digite o Nome do Amigo: ");
        string nome = Console.ReadLine().Trim();
        nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome);

        Console.Write("> Digite o Nome do Responsável: ");
        string nomeResponsavel = Console.ReadLine().Trim();
        nomeResponsavel = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomeResponsavel);

        Console.Write("> Digite o Telefone do Responsável ( 11 digitos (XX)XXXXX-XXXX): ");
        string telefone = Console.ReadLine().Trim();

        if (criarIdNovo)
            return new Amigo(nome, nomeResponsavel, telefone);

        return new Amigo(idExistente, nome, nomeResponsavel, telefone);
    }

    public void ApresentarDadosInvalidos(Amigo novoAmigo)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar Amigo!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagemSemLinha(novoAmigo.Validar(), ConsoleColor.Red);
        Notificador.ApresentarMensagemTenteNovamente();
    }
}
