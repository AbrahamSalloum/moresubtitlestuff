
using System.Text.Json;
class Credentails
{
    public static Credentials? ReadCredentials()
    {
        string file = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "credentials.json");
        string? text = File.ReadAllText(file);
        if(text is null) 
        {
            Console.WriteLine("Can not open the credentails.json file.");
            return null; 
        }
        Credentials? person = JsonSerializer.Deserialize<Credentials>(text);
        if (person is null)
        {
            Console.WriteLine("Something is wrong with your credentials.json file.");
            return null;
        }

        return person;
    }
}