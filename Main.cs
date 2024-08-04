


class MainEntry 


{
    public static async Task Main(string[] args)
    {

            Credentials? credentials = Credentails.ReadCredentials(); 
            if(credentials is null) {
                return;
            }
            ApiRequests subfetch = new ApiRequests(credentials.key, credentials.username, credentials.password);
            await subfetch.Login(); 
            if(args is not null) 
            {
                CommandLineArgs cm = new CommandLineArgs(subfetch);
                await cm.Command(args); 
                return;
            }



            SelectOptions begin = new SelectOptions(subfetch); 
            await begin.ShowSelection(); 
    }
}
 