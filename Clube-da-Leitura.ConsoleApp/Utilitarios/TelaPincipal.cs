namespace Clube_da_Leitura.ConsoleApp.Utilitarios;

class TelaPincipal
{
    public static void ApresentarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║         Clube da Leitura         ║");
        Console.WriteLine("║══════════════════════════════════║");
        Console.WriteLine("║  Bem-vindo ao Clube da Leitura!  ║");
        Console.WriteLine("║══════════════════════════════════║");
        Console.WriteLine("║ 1. Gerenciar Amigos.             ║");
        Console.WriteLine("║ 2. Gerenciar Revistas.           ║");
        Console.WriteLine("║ 3. Gerenciar Caixas.             ║");
        Console.WriteLine("║ 4. Gerenciar Empréstimos.        ║");
        Console.WriteLine("║ 5. Sair do Clube.                ║");
        Console.WriteLine("╚══════════════════════════════════╝");
        Console.Write("> Digite uma opção: ");
        Console.ReadLine();
    }
    
}
