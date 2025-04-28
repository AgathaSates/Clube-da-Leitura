using Clube_da_Leitura.ConsoleApp.Compartilhado;
using Clube_da_Leitura.ConsoleApp.ModuloRevista;
using Clube_da_Leitura.ConsoleApp.Utilitarios;

namespace Clube_da_Leitura.ConsoleApp.ModuloCaixa;

public class Caixa : EntidadeBase<Caixa>
{

    public string Etiqueta { get; set; }
    public string Cor { get; set; }
    public int DiasDeEmprestimoMaximo { get; set; }

    public List<Revista> revistas = new List<Revista>();
    public readonly string[] CoresPermitidas = { "Amarelo", "Azul", "Verde", "Vermelho", "Rosa" };

    public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
    {
        Etiqueta = etiqueta;
        Cor = cor;
        DiasDeEmprestimoMaximo = diasDeEmprestimo;
    }

    public Caixa(int id, string etiqueta, string cor, int diasDeEmprestimo)
    {
        Id = id;
        Etiqueta = etiqueta;
        Cor = cor;
        DiasDeEmprestimoMaximo = diasDeEmprestimo;
    }

    public override void AtualizarRegistro(Caixa registroEditado)
    {
        Etiqueta = registroEditado.Etiqueta;
        Cor = registroEditado.Cor;
        DiasDeEmprestimoMaximo = registroEditado.DiasDeEmprestimoMaximo;
    }

    public override string Validar()
    {
        int tamanhoMaximoEtiqueta = 50;
        int tamanhoMinimoEtiqueta = 3;

        string erros = "";

        if (string.IsNullOrWhiteSpace(Etiqueta))
            erros += "> A Etiqueta é obrigatório!\n";

        else if (Etiqueta.Length < tamanhoMinimoEtiqueta || Etiqueta.Length > tamanhoMaximoEtiqueta)
            erros += "> A Etiqueta deve ter entre 3 e 50 caracteres\n";

        if (string.IsNullOrWhiteSpace(Cor))
            erros += "> A Cor é obrigatório!\n";

        else if(!EhCorValida(Cor))
            erros += "> A Cor deve ser uma das cores disponíveis: " + MostrarCoresPermitidas() + "\n";

        if (DiasDeEmprestimoMaximo < 1)
            erros += "> Os Dias de Empréstimo deve ser maior que 0\n";

        return erros;
    }

    public bool EhCorValida(string cor)
    {
        return CoresPermitidas.Any(c => c.Equals(cor));
    }

    public string MostrarCoresPermitidas()
    {
        string cores = "";
        foreach (var cor in CoresPermitidas)
            cores += $" - {cor}"; 
        return cores;
    }

    public bool TemRevistas()
    {
        return revistas.Count > 0;
    }
    public void AdicionarRevista(Revista revista)
    {
        revistas.Add(revista);
    }

    public void RemoverRevista(Revista revista)
    {
        revistas.Remove(revista);
    } 
}
