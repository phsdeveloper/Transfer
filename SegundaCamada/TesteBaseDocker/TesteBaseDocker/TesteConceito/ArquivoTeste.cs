using System.Text.Json.Serialization;

namespace TesteBaseDocker.TesteConceito;


public class ArquivoTeste : IArquivoTeste
{

    public string Nome { get; set; }
    public int Idade { get; set; }
    public IConta Conta { get; set; }

    public List<IDocumentos> Documentos { get; set; }
}

public class Conta : IConta
{
    public string Agencia { get; set; }
    public string conta { get; set; }
}

public class Documento : IDocumentos
{
    public string Tipo { get; set; }
    public string Conteudo { get; set; }
}



public interface IArquivoTeste
{
    string Nome { get; set; }
    int Idade { get; set; }
    IConta Conta { get; set; }
    List<IDocumentos> Documentos { get; set; }
}

public interface IDocumentos
{
    string Tipo { get; set; }
    string Conteudo { get; set; }
}

public interface IConta
{
    string Agencia { get; set; }
    string conta { get; set; }

}