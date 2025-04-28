namespace Clube_da_Leitura.ConsoleApp.Utilitarios;

class Notificador
{
    public static void ApresentarOpcaoInvalida()
    {
        Console.WriteLine();
        ColorirTexto.ExibirMensagemSemLinha("(X) Opção inválida!", ConsoleColor.DarkYellow);
    }

    public static void ApresentarMensagemTenteNovamente()
    {
        Console.WriteLine();
        ColorirTexto.ExibirMensagemSemLinha("> Pessione Enter para tentar novamente.", ConsoleColor.DarkYellow);
        Console.ReadKey();
    }

    public static void ApresentarMensagemParaSair()
    {
        Console.WriteLine();
        ColorirTexto.ExibirMensagemSemLinha("> Pessione Enter para Sair.", ConsoleColor.DarkYellow);
        Console.ReadKey();
    }
}
