
class MainEntry {
    public static async Task Main()
    {

            ApiRequests subfetch = new ApiRequests("testfakekey", "abraham", "supermanFAKEpw");
            await subfetch.Login(); 

            SelectOptions begin = new SelectOptions(subfetch); 
            await begin.TextSearch(); 
    }

}
