namespace TesteBaseDocker.TesteConceito.SegundaCamada.Interfaces;

public interface IArquivosDados
{
    public string id_vinculo { get; set; }
    public string id_processo { get; set; }
    public DateTime data_vinculo { get; set; }
    public IConteudo Conteudo { get; set; }

}



public interface IConteudo
{
    List<Link> links { get; set; }
    string id { get; set; }
    string tipo { get; set; }
    //List<IArquivo> arquivos { get; set; }

}



public interface IArquivo
{
    List<Link> links { get; set; }
    string id_arquivo { get; set; }
    string nome { get; set; }
}




public class Conteudo : IConteudo
{
    public List<Link> links { get; set; }
    public string id { get; set; }
    public string tipo { get; set; }
    //public List<IArquivo> arquivos { get; set; }
}

public class Link
{
    public string href { get; set; }
    public string rel { get; set; }
    public string type { get; set; }
}

public class Arquivo : IArquivo
{
    public List<Link> links { get; set; }
    public string id_arquivo { get; set; }
    public string nome { get; set; }
}

public class ArquivosDados : IArquivosDados
{
    public string id_vinculo { get; set; }
    public string id_processo { get; set; }
    public DateTime data_vinculo { get; set; }
    public IConteudo Conteudo { get; set; }
}