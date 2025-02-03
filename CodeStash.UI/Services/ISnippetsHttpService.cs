using CodeStash.Core.Dtos;
using CodeStash.Core.Models;

namespace CodeStash.UI.Services;
public interface ISnippetsHttpService
{
    Task<Result> CreateSnippet(AddSnippetDto snippet);
    Task<Result<List<string?>>> GetAllSnippetLanguages();
}