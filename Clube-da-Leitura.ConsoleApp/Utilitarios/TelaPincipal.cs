namespace Clube_da_Leitura.ConsoleApp.Utilitarios;

class telaPincipal
{
    public void ApresentarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║        Clube da Leitura        ║");
        Console.WriteLine("║════════════════════════════════║");
        Console.WriteLine("║ Bem-vindo ao Clube da Leitura! ║");
        Console.WriteLine("║════════════════════════════════║");
        Console.WriteLine("║ 1. Gerenciar Amigos.           ║");
        Console.WriteLine("║ 2. Gerenciar Revistas.         ║");
        Console.WriteLine("║ 3. Gerenciar Caixas.           ║");
        Console.WriteLine("║ 4. Gerenciar Empréstimos.      ║");
        Console.WriteLine("║ 5. Sair do Clube.              ║");
        Console.WriteLine("╚════════════════════════════════╝");
        Console.Write("> Digite uma opção: ");
    }

    public void ApresentarSairDoClube()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════╗");
        Console.WriteLine("║  Obrigado por usar o Clube da Leitura!  ║");
        Console.WriteLine("║              Até a próxima!             ║");
        Console.WriteLine("╚═════════════════════════════════════════╝");
    }

    public void ApresentarOpçaoInvalida()
    {
        Console.WriteLine("Opção inválida! Digite novamente: ");
    }
}
