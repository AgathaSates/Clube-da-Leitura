


using Clube_da_Leitura.ConsoleApp.Compartilhado;

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
            string opcaomenu = Console.ReadLine().Trim();

            switch (opcaomenu)
            {
                case "1": InserirAmigo(); break;
                case "2": EditarAmigo(); break;
                case "3": ExcluirAmigo(); break;
                case "4": VisualizarTodosOsAmigos(true,true); break;
                case "5": VisualizarEmprestimos(); break;
                case "6": return;
                default: ApresentarOpçaoInvalida(); break;
            }
        }
    }

    public void VisualizarEmprestimos()
    {
        throw new NotImplementedException();
    }

    public void VisualizarTodosOsAmigos(bool exibirTitulo, bool exibirsair)
    {
        if (exibirTitulo)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║     Visualizando Amigos Cadastrados     ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");
        }
        
        Amigo[] amigos = repositorioAmigo.SelecionarTodos();

        if (amigos.Length == 0)
        {
            ColorirTexto.ExibirMensagem("(X) Nenhum amigo cadastrado!", ConsoleColor.Red);
            ApresentarMensagemParaSair();
            return;
        }

        Console.WriteLine("╔═════╦════════════╦═════════════════╦═══════════════╗");
        Console.WriteLine(
           "║{0, -4} ║ {1, -10} ║ {2, -15} ║ {3, -13} ║",
           "Id", "Nome", "Responsável", "Telefone");
        Console.WriteLine("║═════╬════════════╬═════════════════╬═══════════════║");
        int contador = 0;
        foreach (Amigo amigo in amigos)
        {
            if (amigo != null)
            {
                Console.WriteLine(
               "║{0, -4} ║ {1, -10} ║ {2, -15} ║ {3, -13} ║",
               amigo.Id, amigo.Nome, amigo.NomeResponsavel, amigo.Telefone);

                if (contador < amigos.Length - 1)
                {
                    Console.WriteLine("║═════╬════════════╬═════════════════╬═══════════════║");
                }
                contador++;

            }              
        }
        Console.WriteLine("╚═════╩════════════╩═════════════════╩═══════════════╝");
        if (exibirsair)
        {
            ApresentarMensagemParaSair();
        }
    }

    public void ExcluirAmigo()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Excluir Amigo               ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");

        VisualizarTodosOsAmigos(false,false);

        Console.Write("> Digite o ID do amigo: ");
        int id = Convert.ToInt32(Console.ReadLine().Trim());

        Amigo amigo = repositorioAmigo.SelecionarPorId(id);
        if (amigo == null)
        {
            ColorirTexto.ExibirMensagem("(X) Amigo não encontrado!", ConsoleColor.Red);
            ApresentarMensagemTenteNovamente();
            ExcluirAmigo();
            return;
        }

        repositorioAmigo.Excluir(amigo.Id);
        ColorirTexto.ExibirMensagem("(V) Amigo excluído com sucesso!", ConsoleColor.Green);
        ApresentarMensagemParaSair();
    }

    public void EditarAmigo()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Editar Amigo                ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");

        VisualizarTodosOsAmigos(false,false);

        Console.Write("> Digite o ID do amigo: ");
        int id = Convert.ToInt32(Console.ReadLine().Trim());

        Amigo amigoExiste = repositorioAmigo.SelecionarPorId(id);

        if (amigoExiste == null)
        {
            ColorirTexto.ExibirMensagem("(X) Amigo não encontrado!", ConsoleColor.Red);
            ApresentarMensagemTenteNovamente();

            EditarAmigo();

            return;
        }

        Amigo amigoEditado = ObterDadosAmigo(false, id);

        if (amigoEditado.Validar() != "")
        {
            ApresentarDadosInválidos(amigoEditado);

            EditarAmigo();

            return;
        }

        bool editou = repositorioAmigo.Editar(id, amigoEditado);

        if (!editou)
        {
            ColorirTexto.ExibirMensagem("Houve um erro durante a edição", ConsoleColor.Red);
            ApresentarMensagemParaSair();
            return;
        }

        ColorirTexto.ExibirMensagem("(V) Amigo editado com sucesso!", ConsoleColor.Green);
        ApresentarMensagemParaSair();
    }

    public void InserirAmigo()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║             Cadastrar Amigo             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");

        Amigo novoAmigo = ObterDadosAmigo(true);

        if (novoAmigo.Validar() != "")
        {
            ApresentarDadosInválidos(novoAmigo);

            InserirAmigo();

            return;
        }

        string mensagem = repositorioAmigo.Inserir(novoAmigo);

        if (mensagem == "(V) Amigo cadastrado com sucesso!")
            ColorirTexto.ExibirMensagem(mensagem, ConsoleColor.Green);

        else
            ColorirTexto.ExibirMensagem(mensagem, ConsoleColor.Red);

        ApresentarMensagemParaSair();
    }

    public void ApresentarDadosInválidos(Amigo novoAmigo)
    {
        ColorirTexto.ExibirMensagem("(X) Erro ao cadastrar amigo!", ConsoleColor.Red);
        ColorirTexto.ExibirMensagemSemLinha(novoAmigo.Validar(), ConsoleColor.Red);
        ApresentarMensagemTenteNovamente();
    }

    public Amigo ObterDadosAmigo(bool criarIdNovo, int idExistente = 0)
    {
        Console.Write("> Digite o Nome do Amigo: ");
        string nome = Console.ReadLine().Trim();

        Console.Write("> Digite o Nome do Responsável: ");
        string nomeResponsavel = Console.ReadLine().Trim();

        Console.Write("> Digite o Telefone do Responsável((XX)XXXXX-XXXX): ");
        string telefone = Console.ReadLine().Trim();

        if (criarIdNovo)            
            return new Amigo(nome, nomeResponsavel, telefone);
        
        return new Amigo(idExistente, nome, nomeResponsavel, telefone);
    }

    public void ApresentarOpçaoInvalida()
    {
        ColorirTexto.ExibirMensagemSemLinha("(X) Opção inválida! Pressione enter para tentar novamente.", ConsoleColor.Red);
        Console.ReadKey();
    }

    public void ApresentarMensagemParaSair()
    {
        ColorirTexto.ExibirMensagemSemLinha(">Pessione Enter para Sair.", ConsoleColor.Yellow);
        Console.ReadKey();
    }

    public void ApresentarMensagemTenteNovamente()
    {
        ColorirTexto.ExibirMensagemSemLinha(">Pessione Enter para tentar novamente.", ConsoleColor.Yellow);
        Console.ReadKey();
    }
}
