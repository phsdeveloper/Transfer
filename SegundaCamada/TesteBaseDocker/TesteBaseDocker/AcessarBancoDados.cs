using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace TesteBaseDocker;

public class AcessarBancoDados
{
 
    public static void AcessarDados()
    {
        string connectionString = "Server=localhost;Database=TestDB;User Id=sa;Password=M0niqu3!#)#11@@;TrustServerCertificate=True;";

        var db = new System.Data.SqlClient.SqlConnection(connectionString);

        db.Open();

        var query = "SELECT * FROM USU_Usuario";
        var resultados = db.Query<dynamic>(query);
        if (resultados != null)
        {
            Console.WriteLine("Dados obtidos com sucesso:");
        }


    }
}