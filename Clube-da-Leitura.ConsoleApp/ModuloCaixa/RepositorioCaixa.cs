
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloCaixa;

class RepositorioCaixa
{
    Caixa[] Caixas = new Caixa[100];
    int indiceCaixa = 0;

    public string Inserir(Caixa caixa)
    {
        if (VerificarLimiteCaixas())
            return ">> (X) Limite de caixas atingido.";

        if (VerificaCaixaJaExiste(caixa))
            return ">> (X) Caixa já cadastrada, não pode registrar etiquetas duplicadas.";

        Caixas[indiceCaixa++] = caixa;
        return ">> (V) Caixa cadastrada com sucesso!";

    }

    public bool Editar(int id, Caixa caixaEditada)
    {
        foreach (Caixa caixa in Caixas)
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
        for (int i = 0; i < Caixas.Length; i++)
            if (Caixas[i] != null)
                if (Caixas[i].Id == id)
                {
                    if (Caixas[i].revistas[0] != null)
                        return true;

                    Caixas[i] = null;
                    indiceCaixa--;
                    return true;
                }
        return false;
    }

    public Caixa[] SelecionarTodos()
    {
        int contadorCaixasPreenchidas = 0;

        foreach (Caixa caixa in Caixas)
            if (caixa != null)
                contadorCaixasPreenchidas++;

        Caixa[] caixasSelecionadas = new Caixa[contadorCaixasPreenchidas];
        int contador = 0;

        foreach (Caixa caixa in Caixas)
            if (caixa != null)
                caixasSelecionadas[contador++] = caixa;

        return caixasSelecionadas;
    }

    public Caixa SelecionarPorId(int id)
    {
        foreach (Caixa caixa in Caixas)
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
        foreach (Caixa caixaExistente in Caixas)
            if (caixaExistente != null)
                if (caixa.Etiqueta == caixaExistente.Etiqueta)
                    jaExiste = true;

        return jaExiste;
    }

    public bool VerificarLimiteCaixas()
    {
        if (indiceCaixa == Caixas.Length)
            return true;
        return false;
    }
}
