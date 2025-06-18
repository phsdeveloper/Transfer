using Newtonsoft.Json;
using TesteBaseDocker.TesteConceito.SegundaCamada.Interfaces;
namespace TesteBaseDocker.TesteConceito;

public class DocumentosConverter : JsonConverter<List<IDocumentos>>
{
    public override List<IDocumentos> ReadJson(JsonReader reader, Type objectType, List<IDocumentos> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var list = serializer.Deserialize<List<Documento>>(reader);
        return list?.Cast<IDocumentos>().ToList();
    }

    public override void WriteJson(JsonWriter writer, List<IDocumentos> value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}

public class DocumentosConverterGenerico<T> : JsonConverter<List<IDocumentos>>
{
    public override List<IDocumentos> ReadJson(JsonReader reader, Type objectType, List<IDocumentos> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var list = serializer.Deserialize<List<T>>(reader);
        return list?.Cast<IDocumentos>().ToList();
    }

    public override void WriteJson(JsonWriter writer, List<IDocumentos> value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}


public class ContaConverter : JsonConverter<IConta>
{
    public override IConta ReadJson(JsonReader reader, Type objectType, IConta existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        System.Console.WriteLine("LEndo json");
        try
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
            return jsonObject.ToObject<Conta>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao desserializar Conta: {e.Message}");
            System.Threading.Thread.Sleep(1000);
        }
        return new Conta();
    }

    public override void WriteJson(JsonWriter writer, IConta value, JsonSerializer serializer)
    {
        System.Console.WriteLine("Serializando Conta...");
        try
        {
            serializer.Serialize(writer, (Conta)value);
            System.Console.WriteLine("Conta serializada com sucesso.");

        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao serializar Conta: {e.Message}");
        }
    }
}

public class ContaConverterGenerico<T> : JsonConverter<IConta> where T : class, IConta, new()
{
    public override IConta ReadJson(JsonReader reader, Type objectType, IConta existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        System.Console.WriteLine("LEndo json");
        try
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
            return jsonObject.ToObject<T>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao desserializar Conta: {e.Message}");
            System.Threading.Thread.Sleep(1000);
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, IConta value, JsonSerializer serializer)
    {
        System.Console.WriteLine("Serializando Conta...");
        try
        {
            serializer.Serialize(writer, (Conta)value);
            System.Console.WriteLine("Conta serializada com sucesso.");

        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao serializar Conta: {e.Message}");
        }
    }
}


public class SegundaCamada_ArquivosDadosConverterGenerico<T> : JsonConverter<IArquivosDados> where T : class, IArquivosDados, new()
{
    public override IArquivosDados ReadJson(JsonReader reader, Type objectType, IArquivosDados existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
        return jsonObject.ToObject<T>();
    }

    public override void WriteJson(JsonWriter writer, IArquivosDados value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, (ArquivosDados)value);
    }
}

public class SegundaCamada_ConteudoConverterGenerico<T> : JsonConverter<IConteudo> where T : class, IConteudo, new()
{
    public override IConteudo ReadJson(JsonReader reader, Type objectType, IConteudo existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = Newtonsoft.Json.Linq.JObject.Load(reader);
        return jsonObject.ToObject<T>();
    }

    public override void WriteJson(JsonWriter writer, IConteudo value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, (ArquivosDados)value);
    }
}


public class SegundaCamada_ArquivoConverterGenerico<T> : JsonConverter<List<IArquivo>>
{
    public override List<IArquivo> ReadJson(JsonReader reader, Type objectType, List<IArquivo> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var list = serializer.Deserialize<List<T>>(reader);
        return list?.Cast<IArquivo>().ToList();
    }

    public override void WriteJson(JsonWriter writer, List<IArquivo> value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
/// <summary>
/// Teste mais generico
/// </summary>
/// <typeparam name="TInterface"></typeparam>
/// <typeparam name="TConcrete"></typeparam>
//public class InterfaceConverterList<TInterface, TConcrete> : JsonConverter
//  where TConcrete : TInterface, new()
//{
//    public override bool CanConvert(Type objectType) =>
//      objectType == typeof(TInterface)
//      || objectType == typeof(List<TInterface>);
//
//    public override object ReadJson(JsonReader reader, Type objectType,
//      object existingValue, JsonSerializer serializer)
//    {
//        if (typeof(IEnumerable<TInterface>).IsAssignableFrom(objectType))
//        {
//            var lista = serializer.Deserialize<List<TConcrete>>(reader);
//            return lista.Cast<TInterface>().ToList();
//        }
//        return serializer.Deserialize<TConcrete>(reader);
//    }
//
//    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
//      serializer.Serialize(writer, value);
//}

public class InterfaceConverterList<TInterface, TConcrete> : JsonConverter
  where TConcrete : TInterface, new()
{
    public override bool CanConvert(Type objectType) =>
      objectType == typeof(TInterface)
      || objectType == typeof(List<TInterface>);

    public override object ReadJson(JsonReader reader, Type objectType,
      object existingValue, JsonSerializer serializer)
    {
        // se for uma lista, trata como antes…
        if (typeof(IEnumerable<TInterface>).IsAssignableFrom(objectType))
        {
            var list = serializer.Deserialize<List<TConcrete>>(reader);
            return list.Cast<TInterface>().ToList();
        }
        // caso contrário, desserializa um único TConcrete
        return serializer.Deserialize<TConcrete>(reader);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
      serializer.Serialize(writer, value);
}
