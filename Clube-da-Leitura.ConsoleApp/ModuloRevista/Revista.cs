using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloCaixa;
using Clube_da_Leitura.ConsoleApp.ModuloReserva;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloRevista;
public class Revista : EntidadeBase<Revista>
{
    public string Titulo { get; set; }
    public int NumeroDaEdicao { get; set; }
    public int AnoDaPublicacao { get; set; }
    public string StatusDeEmprestimo { get; set; }//Disponível / Emprestada / Reservada
    public Reserva Reserva { get; set; }
    public Caixa Caixa { get; set; }

    public Revista(string titulo, int numeroDaEdicao, int anoDaPublicacao, Caixa caixa)
    {
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

    public override void AtualizarRegistro(Revista registroEditado)
    {
        Titulo = registroEditado.Titulo;
        NumeroDaEdicao = registroEditado.NumeroDaEdicao;
        AnoDaPublicacao = registroEditado.AnoDaPublicacao;
        Caixa = registroEditado.Caixa;
    }

    public override string Validar()
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

    public bool EstaEmprestada()
    {
        if (StatusDeEmprestimo == "Emprestada")
           return true;

        return false;
    }

 
}
