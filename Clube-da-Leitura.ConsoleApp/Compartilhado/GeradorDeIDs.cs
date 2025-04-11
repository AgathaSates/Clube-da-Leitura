namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

class GeradorDeIDs
{
    public static int idAmigo = 0;
    public static int idRevista = 0;
    public static int idCaixa = 0;
    public static int idEmprestimo = 0;

    public static int GerarIdAmigo()
    {
        return ++idAmigo;
    }

    public static int GerarIdRevista()
    {
        return ++idRevista;
    }

    public static int GerarIdCaixa()
    {
        return ++idCaixa;
    }

    public static int GerarIdEmprestimo()
    {
        return ++idEmprestimo;
    }

}
