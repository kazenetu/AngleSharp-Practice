using AngleSharp;

internal class Program
{
    /// <summary>
    /// エントリメソッド
    /// </summary>    
    private static void Main(string[] args)
    {
        // パラメータが不足の場合はヘルプ表示
        if (args.Length < 2)
        {
            Console.WriteLine("dotnet run \"url\" \"query\"");
            Console.WriteLine("  url:target URL");
            Console.WriteLine("  query:CSS like Query");
            return;
        }

        var url = args[0];
        var query = args[1];

        var queryResults = GetQueryResults(url, query).Result;

        foreach (var queryResult in queryResults)
        {
            Console.WriteLine(queryResult);
        }
    }

    /// <summary>
    /// URLからqueryの値を取得
    /// </summary>
    /// <param name="url">url</param>
    /// <returns>IDocumentインスタンス</returns>
    private static async Task<List<string>> GetQueryResults(string url, string query)
    {
        var config = Configuration.Default
            .WithDefaultLoader();

        using var context = BrowsingContext.New(config);

        // URLを開く
        var doc = await context.OpenAsync(url);

        // 対象を取得
        var elements = doc.QuerySelectorAll(query)
            .Select(x => x.TextContent)
            .ToList();

        return elements;
    }
}

