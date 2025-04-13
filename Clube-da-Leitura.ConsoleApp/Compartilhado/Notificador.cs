﻿namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

class Notificador
{
    public static void ApresentarOpçaoInvalida()
    {
        ColorirTexto.ExibirMensagemSemLinha("(X) Opção inválida! Pressione enter para tentar novamente.", ConsoleColor.Red);
        Console.ReadKey();
    }

    public static void ApresentarMensagemTenteNovamente()
    {
        ColorirTexto.ExibirMensagemSemLinha(">Pessione Enter para tentar novamente.", ConsoleColor.Yellow);
        Console.ReadKey();
    }

    public static void ApresentarMensagemParaSair()
    {
        ColorirTexto.ExibirMensagemSemLinha(">Pessione Enter para Sair.", ConsoleColor.Yellow);
        Console.ReadKey();
    }
}
