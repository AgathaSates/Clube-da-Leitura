namespace Clube_da_Leitura.ConsoleApp.Compartilhado;

public interface ITelaCrud
{
    void ApresentarMenu();
    void CadastrarRegistro();
    void EditarRegistro();
    void ExcluirRegistro();
    void VisualizarRegistros(bool exibirTitulo, bool exibirSair);
}