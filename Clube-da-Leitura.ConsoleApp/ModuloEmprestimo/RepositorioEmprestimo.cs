using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
class RepositorioEmprestimo
{
    public Emprestimo[] emprestimos = new Emprestimo[100];
    public int contadorEmprestimos = 0;



    public string Inserir(Emprestimo novoEmprestimo)
    {
        if (VerificarLimiteEmprestimos())
            return ">> Limite de empréstimos atingido.";

        if (VerificaAmigoTemEmprestimoAtivo(novoEmprestimo.amigo))
            return ">> Amigo já possui um empréstimo ativo.";

        if (VerificaRevistaJaEmprestada(novoEmprestimo.revista))
            return ">> Revista já emprestada.";
        if (novoEmprestimo.amigo.VerificaMultaAtiva())
            return ">> O Amigo possuí uma multa ativa.";

        emprestimos[++contadorEmprestimos] = novoEmprestimo;
        novoEmprestimo.RegistrarEmprestimo();
        novoEmprestimo.amigo.AdicionarEmprestimo(novoEmprestimo);
        return ">> (V) Empréstimo cadastrado com sucesso!";
    }

    public bool Editar(int id, Emprestimo novoEmprestimo)
    {
        foreach (Emprestimo emprestimo in emprestimos)
            if (emprestimo != null)
                if (emprestimo.Id == id)
                {
                    if (emprestimo.amigo.VerificaMultaAtiva())
                        return false;
                    if (emprestimo.amigo.VerificaEmprestimoAtivo())
                        return false;
                    emprestimo.amigo = novoEmprestimo.amigo;
                    emprestimo.revista = novoEmprestimo.revista;
                    emprestimo.DataIniciodoEmprestimo = novoEmprestimo.DataIniciodoEmprestimo;
                    return true;
                }
        return false;
    }

    public bool Excluir(int id)
    {
        foreach (Emprestimo emprestimo in emprestimos)
            if (emprestimo != null)
                if (emprestimo.Id == id)
                {
                    if (emprestimo.StatusDeEmprestimo != "Concluído")
                        return false;
                    emprestimos[contadorEmprestimos] = null;
                    contadorEmprestimos--;
                    return true;
                }
        return false;
    }

    public Emprestimo[] SelecionarTodos()
    {
        int contadorEmprestimosPreenchidos = 0;
        foreach (Emprestimo emprestimo in emprestimos)
            if (emprestimo != null)
                contadorEmprestimosPreenchidos++;

        Emprestimo[] emprestimosSelecionados = new Emprestimo[contadorEmprestimosPreenchidos];
        int contador = 0;

        foreach (Emprestimo emprestimo in emprestimos)
            if (emprestimo != null)
                emprestimosSelecionados[contador++] = emprestimo;

        return emprestimosSelecionados;
    }
    public Multa[] SelecionarTodasAsMultas()
    {
        Emprestimo[] emprestimos = SelecionarTodos();
        Multa[] multas = new Multa[emprestimos.Length];
        int contadorDeMultas = 0;
        for (int i = 0; i < emprestimos.Length; i++)
        {
            if (emprestimos[i].Multa != null)
            {
                if (emprestimos[i].Multa.EstaPendente())
                    multas[contadorDeMultas++] = emprestimos[i].Multa;
            }
        }
        return multas;
    }

    public Emprestimo SelecionarPorId(int id)
    {
        foreach (Emprestimo emprestimo in emprestimos)
            if (emprestimo != null)
                if (emprestimo.Id == id)
                    return emprestimo;
        return null;
    }

    public Multa SelecionarMultaPorId(int id)
    {
        Multa[] multas = SelecionarTodasAsMultas();
        foreach (Multa multa in multas)
            if (multas != null)
                if (multa.Id == id)
                    return multa;
        return null;
    }

    public bool VerificarLimiteEmprestimos()
    {
        if (contadorEmprestimos == emprestimos.Length)
            return true;
        return false;
    }

    public bool VerificaAmigoTemEmprestimoAtivo(Amigo amigo)
    {
        return amigo.VerificaEmprestimoAtivo();
    }

    public bool VerificaRevistaJaEmprestada(Revista revista)
    {
        if (revista.StatusDeEmprestimo != "Disponível")
            return true;
        return false;
    }

   
}
