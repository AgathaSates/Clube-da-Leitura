
namespace Clube_da_Leitura.ConsoleApp.ModuloRevista;
class RepositorioRevista
{
    public Revista[] Revistas = new Revista[100];
    public int contadorRevista = 0;

    public string Inserir(Revista novaRevista)
    {
        if (VerificarLimiteRevistas())
            return "Limite de revistas atingido.";

        if (VerificaRevistaJaExiste(novaRevista))
            return "Revista já cadastrada.";

        Revistas[contadorRevista++] = novaRevista;
        return "(V) Revista cadastrada com sucesso!";
    }

    public bool Editar(int id, Revista revistaEditada)
    {
        foreach (Revista revista in Revistas)
            if (revista != null)
                if (revista.Id == id)
                {
                    revista.Titulo = revistaEditada.Titulo;
                    revista.NumeroDaEdicao = revistaEditada.NumeroDaEdicao;
                    revista.AnoDaPublicacao = revistaEditada.AnoDaPublicacao;
                    revista.Caixa = revistaEditada.Caixa;

                    return true;
                }

        return false;
    }

    public void Excluir(int id)
    {
        for (int i = 0; i < Revistas.Length; i++)
            if (Revistas[i] != null)
                if (Revistas[i].Id == id)
                {
                    Revistas[i] = null;
                    contadorRevista--;
                    break;
                }

    }

    public Revista[] SelecionarTodos()
    {
        int contadorRevistasPreenhidos = 0;

        foreach (Revista revista in Revistas)
            if (revista != null)
                contadorRevistasPreenhidos++;

        Revista[] revistasSelecionadas = new Revista[contadorRevistasPreenhidos];
        int contador = 0;

        foreach (Revista revista in Revistas)
            if (revista != null)
                revistasSelecionadas[contador++] = revista;

        return revistasSelecionadas;
    }

    public Revista SelecionarPorId(int id)
    {
        foreach (Revista revista in Revistas)
            if (revista != null)
                if (revista.Id == id)
                    return revista;

        return null;
    }

    public bool VerificarLimiteRevistas()
    {
        if (contadorRevista == Revistas.Length)
            return true;
        return false;
    }

    public bool VerificaRevistaJaExiste(Revista novaRevista)
    {
        bool jaExiste = false;

        foreach (Revista revista in Revistas)
            if (revista != null)
                if (revista.Titulo == novaRevista.Titulo && revista.NumeroDaEdicao == novaRevista.NumeroDaEdicao)
                    jaExiste = true;
        return jaExiste;
    }

}
