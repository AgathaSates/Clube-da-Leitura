using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.Utilitarios;
namespace Clube_da_Leitura.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {      
        TelaPincipal telaPincipal = new TelaPincipal();
        while (true) 
        {
            telaPincipal.ApresentarMenuPrincipal();

            ITela telaSelecionada = telaPincipal.ObterTela();
            if (telaSelecionada == null)
                return;
            string opcaoEscolhida = telaSelecionada.ApresentarMenu();
            telaSelecionada.ExecutarOpcao(opcaoEscolhida);
        }
    }
}
