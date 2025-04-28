namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

public interface ITelaCrud : ITela
{
    void CadastrarRegistro(int numeroDoTitulo);
    void EditarRegistro(int numeroDoTitulo);
    void ExcluirRegistro(int numeroDoTitulo);
    void VisualizarRegistros(bool exibirTitulo, bool exibirSair);
}