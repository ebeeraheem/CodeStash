﻿using CodeStash.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Dtos;

public class UpdateSnippetDto
{
    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(4000)]
    public string? Content { get; set; }
    public string? Language { get; set; }
    public List<Tag>? Tags { get; set; }
    public bool UpdateTags { get; set; } = false;
}