
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloCaixa;

class RepositorioCaixa
{
    Caixa[] caixas = new Caixa[100];
    int indiceCaixa = 0;

    public string Inserir(Caixa caixa)
    {
        if (VerificarLimiteCaixas())
            return "Limite de caixas atingido.";

        if (VerificaCaixaJaExiste(caixa))
            return "Caixa já cadastrada, não pode registrar etiquetas duplicadas.";

        caixas[indiceCaixa++] = caixa;
        return "(V) Caixa cadastrada com sucesso!";

    }

    public bool Editar(int id, Caixa caixaEditada)
    {
        foreach (Caixa caixa in caixas)
            if (caixa != null)
                if (caixa.Id == id)
                {
                    caixa.Etiqueta = caixaEditada.Etiqueta;
                    caixa.Cor = caixaEditada.Cor;
                    caixa.DiasDeEmprestimoMaximo = caixaEditada.DiasDeEmprestimoMaximo;
                    return true;
                }
        return false;
    }

    public bool Excluir(int id)
    {
        for (int i = 0; i < caixas.Length; i++)
            if (caixas[i] != null)
                if (caixas[i].Id == id)
                {
                    if (caixas[i].revistas[0] != null)
                        return true;

                    caixas[i] = null;
                    indiceCaixa--;
                }
        return false;
    }

    public Caixa[] SelecionarTodos()
    {
        int contadorCaixasPreenchidas = 0;

        foreach (Caixa caixa in caixas)
            if (caixa != null)
                contadorCaixasPreenchidas++;

        Caixa[] caixasSelecionadas = new Caixa[contadorCaixasPreenchidas];
        int contador = 0;

        foreach (Caixa caixa in caixas)
            if (caixa != null)
                caixasSelecionadas[contador++] = caixa;

        return caixasSelecionadas;
    }

    public Caixa SelecionarPorId(int id)
    {
        foreach (Caixa caixa in caixas)
            if (caixa != null)
                if (caixa.Id == id)
                    return caixa;
        return null;
    }
    
    public int ContarRevistasNaCaixa(Caixa caixa)
    {
        int contador = 0;
        foreach (Revista revista in caixa.revistas)
            if (revista != null)
                contador++;
        return contador;
    }

    public bool VerificaCaixaJaExiste(Caixa caixa)
    {
        bool jaExiste = false;
        foreach (Caixa caixaExistente in caixas)
            if (caixaExistente != null)
                if (caixa.Etiqueta == caixaExistente.Etiqueta)
                    jaExiste = true;

        return jaExiste;
    }

    public bool VerificarLimiteCaixas()
    {
        if (indiceCaixa == caixas.Length)
            return true;
        return false;
    }

}
