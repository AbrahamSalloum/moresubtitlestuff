
class SelectOptions
{

    ApiRequests subfetch;

    public SelectOptions(ApiRequests Subfetch) 
    {
            subfetch = Subfetch;
    }

    public void ShowSelection() {
        string controls = $"1) Search By Movie Name. \n2) Search By MovieHash. \n";
        Console.WriteLine(controls);
    }

    public int TakeSelection() {
          
        string? input = Console.ReadLine(); 
        if(input is null) return 0;   
        return Int32.Parse(input); 
    }

    public async void ParseSelection() {

        switch(TakeSelection())
        {
            case 1:
                await TextSearch();
                break;
            case 2: 
            //
                break;
            default: 
                Console.WriteLine("1 or 2. No other Choice.");
                break;     
        }
    }

    public async Task TextSearch() 
    {
        
         SubtitleResults? subtitleResults  = await subfetch.SearchSubtitle("Harry Potter");
        Console.WriteLine(subtitleResults?.data?[0]?.attributes?.files?[0].file_id);
    }
}