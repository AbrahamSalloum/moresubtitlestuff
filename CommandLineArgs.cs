class CommandLineArgs
{

    ApiRequests subfetch;

    public CommandLineArgs(ApiRequests Subfetch)
    {
        subfetch = Subfetch;
    }
    public async Task<SubtitleResults?> Command(string[] args)
    {

        if (args is null)
        {
            Console.WriteLine("format is --custom {\"key\": \"value\",...}");
            return null;
        }
        if (args[0] == "--custom")
        {
            if (args.Length >= 2)
            {
                string customSearch = args[1];
                SubtitleResults? subtitleResults = await subfetch.SubtitleSearch(customSearch);
                if (subtitleResults is null)
                {
                    return null;
                }

                SelectOptions.PrintSample(subtitleResults);
                return subtitleResults;
            }


        }
        Console.WriteLine("format is --custom {\"key\": \"value\",...}");
        return null;
    }
}