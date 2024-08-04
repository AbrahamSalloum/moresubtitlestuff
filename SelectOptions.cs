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

    public async Task<SubtitleResults?> ParseSelection() {
        SubtitleResults? subtitleResults;
        switch(TakeSelection())
        {
            case 1:
                subtitleResults = await TextSearch();
                
                break;
            case 2: 
                subtitleResults = await MovieHashSearch();
                break;
            case 3:
                subtitleResults = await CustomSearch();
                break; 
            default: 
                Console.WriteLine("1 2 or 3. No other Choice.");
                subtitleResults = null;
                break;     
        }
        if(subtitleResults is null) {
            Console.WriteLine("search results are null. Something might have gone wrong.");
            return subtitleResults; 
        }

        PrintSample(subtitleResults);
        return subtitleResults; 
    }

    public async Task<SubtitleResults?> TextSearch() 
    {

        Console.WriteLine("Type a Movie Name and Year:");
        string? SearchString = Console.ReadLine();
        if(SearchString is null) {
            return null; 
        }
        object TextSearchObject = new 
        {
            query = SearchString
        };
        SubtitleResults? subtitleResults  = await subfetch.SubtitleSearch(TextSearchObject);
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
         
        object movieHashObject = new 
        {
            moviehash = stringhash
        };
        SubtitleResults? subtitleResults  = await subfetch.SubtitleSearch(movieHashObject);

        return subtitleResults; 
    }    

    public async Task<SubtitleResults?> CustomSearch() 
    {
        Console.WriteLine("Enter JSON search terms:");
        string? j = Console.ReadLine();
        if(j is null) 
        {
            return null; 
        }
        SubtitleResults? subtitleResults  = await subfetch.SubtitleSearch(j);
        return subtitleResults;
    }

    public static void PrintSample(SubtitleResults subtitleResults)
    {
        Console.WriteLine("===Sample Results==="); 
        Console.WriteLine($"Subtitle ID is: {subtitleResults?.data?[0]?.attributes?.files?[0]?.file_id}");
        Console.WriteLine($"Subtitle ID is: {subtitleResults?.data?[0]?.attributes?.files?[0]?.file_name}");
    }
}