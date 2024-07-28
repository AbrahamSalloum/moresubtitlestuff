
class MainEntry {
    public static async Task Main()
    {
           
           
            await ApiRequests.GetRecentList();
            await ApiRequests.SearchSubtitle();
            await ApiRequests.DownloadSubtitle();

            byte[] moviehash = Utils.ComputeMovieHash(@"Z:\test.mp4");
            Console.WriteLine("The hash of the movie-file is: {0}", Utils.ToHexadecimal(moviehash));
    }

}
