using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloReserva;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloRevista;
class Revista
{
    public int Id;
    public string Titulo;
    public int NumeroDaEdicao;
    public int AnoDaPublicacao;
    public string StatusDeEmprestimo; //Disponível / Emprestada / Reservada
    public Reserva Reserva;
    public Caixa Caixa;

    public Revista(string titulo, int numeroDaEdicao, int anoDaPublicacao, Caixa caixa)
    {
        Id = GeradorDeIDs.GerarIdRevista();
        Titulo = titulo;
        NumeroDaEdicao = numeroDaEdicao;
        AnoDaPublicacao = anoDaPublicacao;
        StatusDeEmprestimo = "Disponível";
        Caixa = caixa;
    }

    public Revista(int id, string titulo, int numeroDaEdicao, int anoDaPublicacao, Caixa caixa)
    {
        Id = id;
        Titulo = titulo;
        NumeroDaEdicao = numeroDaEdicao;
        AnoDaPublicacao = anoDaPublicacao;
        StatusDeEmprestimo = "Disponível";
        Caixa = caixa;
    }

    public string Validar()
    {
        int tamanhoMaximoTitulo = 100;
        int tamanhoMinimoTitulo = 2;
        string erros = "";

        if (string.IsNullOrWhiteSpace(Titulo))
            erros += "> O Título é obrigatório!\n";

        else if (Titulo.Length < tamanhoMinimoTitulo || Titulo.Length > tamanhoMaximoTitulo)
            erros += "> O Título deve ter entre 2 e 100 caracteres\n";

        if (NumeroDaEdicao <= 0)
            erros += "> O Número da Edição deve ser um número positivo\n";

        if (AnoDaPublicacao < 1800 || AnoDaPublicacao > DateTime.Now.Year)
            erros += "> O Ano da Publicação deve ser um ano válido\n";

        if (Caixa == null)
            erros += "> A Caixa é obrigatória!\n";

        return erros;
    }

    public void Reservar(Reserva novaReserva)
    {
        Reserva = novaReserva;
        StatusDeEmprestimo = "Reservada";
    }

    public void Emprestar()
    {
        StatusDeEmprestimo = "Emprestada";
    }

    public void Devolver()
    {
        StatusDeEmprestimo = "Disponível";
    }
}
