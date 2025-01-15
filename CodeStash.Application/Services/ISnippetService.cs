﻿using CodeStash.Application.Filters;
using CodeStash.Application.Models;
using CodeStash.Core.Dtos;

namespace CodeStash.Application.Services;
public interface ISnippetService
{
    Task<Result> AddSnippetAsync(AddSnippetDto request);
    Task<Result> DeleteSnippetAsync(Guid snippetId);
    Result GetAllSnippetLanguages();
    Task<Result> GetSnippetById(Guid snippetId);
    Task<Result> GetSnippets(int pageNumber, int pageSize, SnippetsFilter filter);
    Task<Result> UpdateSnippetAsync(Guid snippetId, UpdateSnippetDto request);
}