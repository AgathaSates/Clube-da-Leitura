namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

class ColorirTexto
{
    public static void ExibirMensagemSemLinha(string mensagem, ConsoleColor cor) 
    {
        Console.ForegroundColor = cor;
        Console.Write(mensagem);
        Console.ResetColor();
    }

    public static void ExibirMensagem(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }
}
