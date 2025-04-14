namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

class Validador
{
    public static bool VerificaSeEhNumero(string texto)
    {
        foreach (char c in texto)
            if (!char.IsDigit(c))
                return false;

        return true;
    }

    public static int DigitouUmNumero()
    {
        int ehNumero;

        while (!int.TryParse(Console.ReadLine(), out ehNumero))
           ColorirTexto.ExibirMensagemSemLinha("O Valor precisa ser número! Digite novamente:", ConsoleColor.Red);  

        return ehNumero;
    }
}
