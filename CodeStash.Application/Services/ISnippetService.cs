using CodeStash.Application.Models;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public interface ISnippetService
{
    Task<Result> AddSnippetAsync(SnippetDto request);
    Task<Result> DeleteSnippetAsync(Guid snippetId);
    Result GetAllSnippetLanguages();
    Task<Result> UpdateSnippetAsync(Guid snippetId, UpdateSnippetDto request);
}