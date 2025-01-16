using System.ComponentModel.DataAnnotations;

namespace CodeStash.Application.Models;
public class TagModel
{
    [MaxLength(50)]
    public required string Name { get; set; }
}
