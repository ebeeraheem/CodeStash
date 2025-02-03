using CodeStash.Core.Dtos;
using CodeStash.Core.Models;

namespace CodeStash.UI.Services;

public class SnippetsHttpService(HttpClient httpClient) : ISnippetsHttpService
{
    public async Task<Result> CreateSnippet(AddSnippetDto snippet)
    {
        var response = await httpClient.PostAsJsonAsync<AddSnippetDto>("snippets", snippet);

        return await response.Content.ReadFromJsonAsync<Result>();
    }

    public async Task<Result<List<string?>>> GetAllSnippetLanguages()
    {
        return await httpClient.GetFromJsonAsync<Result<List<string?>>>("snippets/languages");
    }

    //public async Task<Result> GetSnippets(int pageNumber, int pageSize, SnippetsFilter filter)
    //{
    //    var response = await httpClient.GetFromJsonAsync<Result>($"snippets?pageNumber={pageNumber}&pageSize={pageSize}&filter={filter}");
    //    return response;
    //}
}
