// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
//TesteBaseDocker.AcessarBancoDados.AcessarDados();
//TesteBaseDocker.TesteConceito.Execucao.MTD_Executar();
try
{
    TesteBaseDocker.TesteConceito.Execucao.ExecutarSegundaCamda(TesteBaseDocker.TesteConceito.versao.Versao2);
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao executar a primeira camada: {ex.Message}");
    Console.WriteLine($"Erro ao executar a primeira camada: {ex.StackTrace}");
}
