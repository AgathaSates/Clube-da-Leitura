﻿namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

class Notificador
{
    public static void ApresentarOpcaoInvalida()
    {
        Console.WriteLine();
        ColorirTexto.ExibirMensagemSemLinha("(X) Opção inválida! Pressione Enter para tentar novamente.", ConsoleColor.DarkYellow);
        Console.ReadKey();
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
