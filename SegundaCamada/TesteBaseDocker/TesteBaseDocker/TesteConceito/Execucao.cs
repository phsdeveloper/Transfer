using Newtonsoft.Json;
using TesteBaseDocker.TesteConceito.SegundaCamada.Interfaces;
namespace TesteBaseDocker.TesteConceito;

public enum versao
{
  Versao1 = 1,
  Versao2 = 2,
}

public class Execucao
{
  public static void MTD_Executar()
  {
    string json = @"{
    ""Nome"": ""Paulo"",
    ""Idade"": 30,
    ""Conta"": { ""Agencia"": ""1234"", ""conta"": ""56789"" },
    ""Documentos"": [
        { ""Tipo"": ""CPF"", ""Conteudo"": ""123.456.789-00"" },
        { ""Tipo"": ""RG"", ""Conteudo"": ""MG-12.345.678"" }
    ]
}";

    JsonSerializerSettings settings = new JsonSerializerSettings
    {
      Converters = new List<JsonConverter> {
        //new DocumentosConverter(),
        new DocumentosConverterGenerico<Documento>(),
      new ContaConverterGenerico<Conta>(),
       },
    };

    IArquivoTeste arquivo = JsonConvert.DeserializeObject<ArquivoTeste>(json, settings);

    Console.WriteLine(arquivo.Nome);
    Console.WriteLine(arquivo.Idade);
    Console.WriteLine($"Conta: {arquivo.Conta.Agencia}, {arquivo.Conta.conta}");
    foreach (var doc in arquivo.Documentos)
    {
      Console.WriteLine($"Tipo: {doc.Tipo}, Conteudo: {doc.Conteudo}");
    }

    bool bl_pausar = true;
  }


  public static void ExecutarSegundaCamda(versao versao = versao.Versao1)
  {
    try
    {
      // Obtém o caminho da pasta "Arquivos" na raiz do projeto
      string st_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin/Debug/net6.0", @"TesteConceito/Arquivos"), $"{(versao == versao.Versao1 ? "Versao01" : "Versao02")}.json");
      if (File.Exists(st_path) == false)
      {
        Console.WriteLine($"Arquivo não encontrado: {st_path}");
        return;
      }

      string st_json = File.ReadAllText(st_path);
      System.Console.WriteLine($" - Lendo arquivo JSON: {st_path}");
      System.Console.WriteLine($" - Data Leitura: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

      // JsonSerializerSettings settings = new JsonSerializerSettings
      // {
      //   Converters = new List<JsonConverter>
      //   {
      //     //new SegundaCamada_ArquivosDadosConverterGenerico<ArquivosDados>(),
      //     new SegundaCamada_ConteudoConverterGenerico<Conteudo>(),
      //     //new SegundaCamada_ArquivoConverterGenerico<Arquivo>(),
      //     //new SegundaCamada_ArquivosDados<ArquivosDados>(),
      //     new InterfaceConverterList<IArquivo, Arquivo>(),
      //   }
      // };
      JsonSerializerSettings settings = new JsonSerializerSettings();

      //settings.Converters.Add(new SegundaCamada_ConteudoConverterGenerico<Conteudo>());
      if (versao == versao.Versao2)
      {
        settings.Converters.Add(new SegundaCamada_ConteudoConverterGenerico<Conteudo2>());
      }
      else
      {
        settings.Converters.Add(new SegundaCamada_ConteudoConverterGenerico<Conteudo>());
      }
      settings.Converters.Add(new InterfaceConverterList<IArquivo, Arquivo>());

      IArquivosDados arquivosDados = JsonConvert.DeserializeObject<ArquivosDados>(st_json, settings);
      System.Console.WriteLine($" - Listando dados: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
      Console.WriteLine($"    ID Vinculo: {arquivosDados.id_vinculo}");
      Console.WriteLine($"    ID Processo: {arquivosDados.id_processo}");
      Console.WriteLine($"    Data Vinculo: {arquivosDados.data_vinculo}");
      Console.WriteLine($"    Conteudo ID: {arquivosDados.Conteudo.id}");
      Console.WriteLine($"    Conteudo Tipo: {arquivosDados.Conteudo.tipo}");
      Console.WriteLine($"    - Conteudo da propriedade Conteudo:");
      arquivosDados.Conteudo.links.ForEach(link => System.Console.WriteLine($"       {arquivosDados.Conteudo.links.IndexOf(link) + 1}) Link HREF: {link.href}, Rel: {link.rel}, Type: {link.type}"));

      Console.WriteLine($"    - Conteudo da propriedade arquivos:");
      foreach (var arquivo in arquivosDados.Conteudo.arquivos)
      {
        Console.WriteLine($"        {arquivosDados.Conteudo.arquivos.IndexOf(arquivo) + 1}) Arquivo ID: {arquivo.id_arquivo}, Nome: {arquivo.nome}");
        Console.WriteLine($"        - Conteudo da propriedade links:");
        foreach (var link in arquivo.links)
        {
          Console.WriteLine($"           {arquivo.links.IndexOf(link) + 1}) Link HREF: {link.href}, Rel: {link.rel}, Type: {link.type}");
        }
      }

      bool bl_pausar = true;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);

    }

  }

  public static void MTD_ExecutarNovoModelo()
  {
    string json = @"{
    ""Nome"": ""Paulo"",
    ""Idade"": 30,
    ""Conta"": { ""Agencia"": ""1234"", ""conta"": ""56789"" },
    ""Documentos"": [
        { ""Tipo"": ""CPF"", ""Conteudo"": ""123456789"" },
        { ""Tipo"": ""RG"", ""Conteudo"": ""987654321"" }
    ]
}";

    var arquivo = JsonConvert.DeserializeObject<ArquivoTeste>(json);
    Console.WriteLine($"Nome: {arquivo.Nome}, Idade: {arquivo.Idade}");
    Console.WriteLine($"Conta: Agência {arquivo.Conta.Agencia}, Conta {arquivo.Conta.conta}");
    // foreach (var doc in arquivo.Documentos)
    // {
    //   Console.WriteLine($"Documento: {doc.Tipo}, Conteúdo: {doc.Conteudo}");
    // }

  }

}

