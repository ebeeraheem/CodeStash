using CodeStash.Application.Filters;
using CodeStash.Core.Models;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public interface ISnippetService
{
    Task<Result> AddSnippetAsync(AddSnippetDto request);
    Task<Result> DeleteSnippetAsync(string snippetId);
    Result GetAllSnippetLanguages();
    Task<Result> GetMySnippets();
    Task<Result> GetSnippetById(string snippetId);
    Task<Result> GetSnippets(int pageNumber, int pageSize, SnippetsFilter filter);
    Task<Result> GetSnippetsByAuthorId(string authorId);
    Task<Result> GetSnippetsByTag(string tagId);
    Task<Result> UpdateSnippetAsync(string snippetId, UpdateSnippetDto request);
}