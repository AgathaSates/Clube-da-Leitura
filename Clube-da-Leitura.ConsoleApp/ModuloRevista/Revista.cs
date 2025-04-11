using Clube_da_Leitura.ConsoleApp.ModuloCaixa;

namespace Clube_da_Leitura.ConsoleApp.ModuloRevista;
class Revista
{
    public int id;
    public string titulo;
    public int numeroDaEdicao;
    public int anoDaPublicacao;
    public string statusDeEmprestimo;
    public Caixa caixa;

    //● Não pode haver revistas com mesmo título e edição

    public Revista(string titulo, int numeroDaEdicao, int anoDaPublicacao, Caixa caixa)
    {
        this.titulo = titulo;
        this.numeroDaEdicao = numeroDaEdicao;
        this.anoDaPublicacao = anoDaPublicacao;
        statusDeEmprestimo = "Disponível";
        this.caixa = caixa;
    }
    
    public string Validar()
    {
        int tamanhoMaximoTitulo = 100;
        int tamanhoMinimoTitulo = 2;
        string erros = "";

        if (string.IsNullOrWhiteSpace(titulo))
            erros += "> O Título é obrigatório!\n";

        else if (titulo.Length < tamanhoMinimoTitulo || titulo.Length > tamanhoMaximoTitulo)
            erros += "> O Título deve ter entre 2 e 100 caracteres\n";

        if (numeroDaEdicao <= 0)
            erros += "> O Número da Edição deve ser um número positivo\n";

        if (anoDaPublicacao < 1800 || anoDaPublicacao > DateTime.Now.Year)
            erros += "> O Ano da Publicação deve ser um ano válido\n";

        if (caixa == null)
            erros += "> A Caixa é obrigatória!\n";


        return erros;
    }

    public void Emprestar()
    {
        statusDeEmprestimo = "Emprestada";
    }

    public void Devolver()
    {
        statusDeEmprestimo = "Disponível";
    }
}
