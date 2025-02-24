﻿using CodeStash.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CodeStash.Core.Entities;
public class ApplicationRole : IdentityRole, IAuditableEntity
{
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
