using CodeStash.Core.Entities;
using CodeStash.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Dtos;
public class SnippetDto
{
    [MaxLength(100)]
    public required string Title { get; set; }

    [MaxLength(4000)]
    public required string Content { get; set; }
    public required string Language { get; set; } = SnippetLanguage.None;
    public List<Tag> Tags { get; set; } = [];
}
