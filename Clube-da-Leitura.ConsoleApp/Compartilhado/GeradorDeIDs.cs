namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

class GeradorDeIDs
{
    public static int IdAmigo = 0;
    public static int IdRevista = 0;
    public static int IdCaixa = 0;
    public static int IdEmprestimo = 0;
    public static int IdMulta = 0;

    public static int GerarIdAmigo()
    {
        return ++IdAmigo;
    }

    public static int GerarIdRevista()
    {
        return ++IdRevista;
    }

    public static int GerarIdCaixa()
    {
        return ++IdCaixa;
    }

    public static int GerarIdEmprestimo()
    {
        return ++IdEmprestimo;
    }
    public static int GerarIdMulta()
    {
        return ++IdMulta;
    }
}
