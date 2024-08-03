
class SelectOptions
{

    ApiRequests subfetch;

    public SelectOptions(ApiRequests Subfetch) 
    {
            subfetch = Subfetch;
    }

    public async Task ShowSelection() {
        string controls = $"1) Search By Movie Name. \n2) Search By MovieHash.\n3) Custom Search.\n";
        Console.WriteLine(controls);
        await ParseSelection();
    }

    public int TakeSelection() {
           
        string? input = Console.ReadLine(); 
        if(input is null) return 0;   
        return Int32.Parse(input); 
    }

    public async Task ParseSelection() {

        switch(TakeSelection())
        {
            case 1:
                SubtitleResults? subtitleResults = await TextSearch();
                Console.WriteLine($"Subtitle ID is: {subtitleResults?.data?[0]?.attributes?.files?[0].file_id}");
                break;
            case 2: 
                SubtitleResults? subtitleResultsx = await MovieHashSearch();
                break;
            case 3:
                SubtitleResults? customSearch = await MovieHashSearch();
                break; 
            default: 
                Console.WriteLine("1 2 or 3. No other Choice.");
                break;     
        }
    }

    public async Task<SubtitleResults?> TextSearch() 
    {

        Console.WriteLine("Type a Movie Name and Year:");
        string? SearchString = Console.ReadLine();
        if(SearchString is null) {
            return null; 
        }
        
        SubtitleResults subtitleResults  = await subfetch.SearchSubtitle(SearchString);
        return subtitleResults; 
    }

    public async Task<SubtitleResults?> MovieHashSearch() 
    {

        Console.WriteLine("File path to Movie:");
        string? moviepath = Console.ReadLine();
        if(moviepath is null) {
            return null; 
        }

        byte[]? moviehash = Utils.ComputeMovieHash(moviepath);
        if(moviehash is null) {
            return null; 
        }
        string stringhash = Utils.ToHexadecimal(moviehash);
         
        var subtitleResults  = await subfetch.SearchMovieHash(stringhash);
        Console.WriteLine("===Results==="); 
        Console.WriteLine($"Subtitle ID is: {subtitleResults?.data?[0]?.attributes?.files?[0].file_id}");
        Console.WriteLine($"Subtitle ID is: {subtitleResults?.data?[0]?.attributes?.files?[0].file_name}");
        return subtitleResults; 
    }    
}