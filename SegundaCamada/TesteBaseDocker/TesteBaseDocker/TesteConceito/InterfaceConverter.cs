using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// namespace TesteBaseDocker.TesteConceito
// {
//     public class InterfaceConverter<TInterface, TConcrete> : JsonConverter where TConcrete : TInterface, new()
//     {
//         public override bool CanConvert(Type objectType) => typeof(TInterface).IsAssignableFrom(objectType);

//         public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//         {
//             var jsonObject = JObject.Load(reader);
//             var target = new TConcrete();
//             serializer.Populate(jsonObject.CreateReader(), target);
//             return target;
//         }

//         public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//         {
//             serializer.Serialize(writer, value);
//         }
//     }
// }