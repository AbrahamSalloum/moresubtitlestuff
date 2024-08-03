using System.Text.Json;
class MainEntry 

{
    public static async Task Main()
    {

            Credentials? credentials = Credentails.ReadCredentials(); 
            if(credentials is null) {
                return;
            }
            ApiRequests subfetch = new ApiRequests(credentials.key, credentials.username, credentials.password);
            await subfetch.Login(); 

            SelectOptions begin = new SelectOptions(subfetch); 
            await begin.ShowSelection(); 
    }
}
 