using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloAmigo;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;

namespace Clube_da_Leitura.ConsoleApp.ModuloEmprestimo;
class Emprestimo
{
    public int Id;
    public Amigo amigo;
    public Revista revista;
    public DateTime DataIniciodoEmprestimo;
    public DateTime DataDevolucao;
    public string StatusDeEmprestimo; //Aberto / Concluído / Atrasado

    public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo)
    {
        Id = GeradorDeIDs.GerarIdEmprestimo();
        this.amigo = amigo;
        this.revista = revista;
        DataIniciodoEmprestimo = dataEmprestimo;
        DataDevolucao = ObterDataDeDevolucao();

    }

    public string Validar()
    {
        string erros = "";

        if (amigo == null)
            erros += "> O Amigo é obrigatório!\n";

        if (revista == null)
            erros += "> A Revista é obrigatória!\n";

        if (revista.StatusDeEmprestimo != "Disponível")
            erros += "> A Revista não está disponível para empréstimo!\n";

        return erros;
    }

    public DateTime ObterDataDeDevolucao()
    {
        return DataIniciodoEmprestimo.AddDays(revista.Caixa.DiasDeEmprestimoMaximo);
    }

    public bool EmprestimoEstaAtrasado(Emprestimo emprestimo)
    {
        if (emprestimo.DataDevolucao < DateTime.Now)
            return true;
        return false;
    }

    public void RegistrarDevolucao()
    {
        StatusDeEmprestimo = "Concluído";
        revista.Devolver();
    }

    public void RegistrarAtraso()
    {

        StatusDeEmprestimo = "Atrasado";
    }

    public void RegistrarEmprestimo()
    {
        StatusDeEmprestimo = "Aberto";
        revista.Emprestar();
    }
}
