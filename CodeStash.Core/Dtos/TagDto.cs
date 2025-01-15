using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Dtos;
public class TagDto
{
    [MaxLength(50)]
    public required string Name { get; set; }
}
