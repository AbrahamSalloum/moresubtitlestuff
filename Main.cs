class MainEntry {
    public static async Task Main()
    {
            ApiRequests subfetch = new ApiRequests("testfakekey", "abraham", "supermanFAKEpw");
            await subfetch.Login(); 
            SubtitleResults subtitleResults = await subfetch.SearchSubtitle("Harry Potter");
            int id = subtitleResults.data[0].attributes.files[0].file_id;
            DownLoadLinkData info = await subfetch.RequestDownloadURL(id);
            await subfetch.DownloadSubFile(info);
    }

}
