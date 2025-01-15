using CodeStash.Core.Entities;

namespace CodeStash.Infrastructure.Seeder;
public static class SeedTags
{
    public static List<Tag> GetInitialTags()
    {
        return new List<Tag>
        {
            // Patterns & Architecture
            new Tag { Id = Guid.NewGuid(), Name = "design-patterns" },
            new Tag { Id = Guid.NewGuid(), Name = "solid-principles" },
            new Tag { Id = Guid.NewGuid(), Name = "clean-code" },
            new Tag { Id = Guid.NewGuid(), Name = "clean-architecture" },
            new Tag { Id = Guid.NewGuid(), Name = "dry" },
            new Tag { Id = Guid.NewGuid(), Name = "mvc" },
            new Tag { Id = Guid.NewGuid(), Name = "mvvm" },
            
            // Security
            new Tag { Id = Guid.NewGuid(), Name = "authentication" },
            new Tag { Id = Guid.NewGuid(), Name = "authorization" },
            new Tag { Id = Guid.NewGuid(), Name = "encryption" },
            new Tag { Id = Guid.NewGuid(), Name = "hashing" },
            new Tag { Id = Guid.NewGuid(), Name = "jwt" },
            new Tag { Id = Guid.NewGuid(), Name = "oauth" },
            
            // Performance
            new Tag { Id = Guid.NewGuid(), Name = "caching" },
            new Tag { Id = Guid.NewGuid(), Name = "optimization" },
            new Tag { Id = Guid.NewGuid(), Name = "memory-management" },
            new Tag { Id = Guid.NewGuid(), Name = "performance" },
            new Tag { Id = Guid.NewGuid(), Name = "lazy-loading" },
            
            // Data & Storage
            new Tag { Id = Guid.NewGuid(), Name = "database" },
            new Tag { Id = Guid.NewGuid(), Name = "orm" },
            new Tag { Id = Guid.NewGuid(), Name = "linq" },
            new Tag { Id = Guid.NewGuid(), Name = "entity-framework" },
            new Tag { Id = Guid.NewGuid(), Name = "migration" },
            new Tag { Id = Guid.NewGuid(), Name = "serialization" },
            
            // Testing
            new Tag { Id = Guid.NewGuid(), Name = "unit-testing" },
            new Tag { Id = Guid.NewGuid(), Name = "integration-testing" },
            new Tag { Id = Guid.NewGuid(), Name = "e2e-testing" },
            new Tag { Id = Guid.NewGuid(), Name = "mocking" },
            new Tag { Id = Guid.NewGuid(), Name = "test-doubles" },
            
            // Error Handling
            new Tag { Id = Guid.NewGuid(), Name = "error-handling" },
            new Tag { Id = Guid.NewGuid(), Name = "exception" },
            new Tag { Id = Guid.NewGuid(), Name = "logging" },
            new Tag { Id = Guid.NewGuid(), Name = "debugging" },
            new Tag { Id = Guid.NewGuid(), Name = "monitoring" },
            
            // Web Development
            new Tag { Id = Guid.NewGuid(), Name = "api" },
            new Tag { Id = Guid.NewGuid(), Name = "rest" },
            new Tag { Id = Guid.NewGuid(), Name = "graphql" },
            new Tag { Id = Guid.NewGuid(), Name = "websocket" },
            new Tag { Id = Guid.NewGuid(), Name = "http" },
            new Tag { Id = Guid.NewGuid(), Name = "middleware" },
            
            // Frontend
            new Tag { Id = Guid.NewGuid(), Name = "react" },
            new Tag { Id = Guid.NewGuid(), Name = "angular" },
            new Tag { Id = Guid.NewGuid(), Name = "vue" },
            new Tag { Id = Guid.NewGuid(), Name = "state-management" },
            new Tag { Id = Guid.NewGuid(), Name = "ui" },
            
            // Utilities & Helpers
            new Tag { Id = Guid.NewGuid(), Name = "helper" },
            new Tag { Id = Guid.NewGuid(), Name = "utility" },
            new Tag { Id = Guid.NewGuid(), Name = "extension-method" },
            new Tag { Id = Guid.NewGuid(), Name = "validation" },
            new Tag { Id = Guid.NewGuid(), Name = "formatting" },
            
            // Algorithms
            new Tag { Id = Guid.NewGuid(), Name = "algorithm" },
            new Tag { Id = Guid.NewGuid(), Name = "sorting" },
            new Tag { Id = Guid.NewGuid(), Name = "searching" },
            new Tag { Id = Guid.NewGuid(), Name = "data-structure" },
            new Tag { Id = Guid.NewGuid(), Name = "recursion" },
            
            // DevOps & Deployment
            new Tag { Id = Guid.NewGuid(), Name = "ci-cd" },
            new Tag { Id = Guid.NewGuid(), Name = "docker" },
            new Tag { Id = Guid.NewGuid(), Name = "kubernetes" },
            new Tag { Id = Guid.NewGuid(), Name = "deployment" },
            new Tag { Id = Guid.NewGuid(), Name = "configuration" },
            
            // Common Functionality
            new Tag { Id = Guid.NewGuid(), Name = "date-time" },
            new Tag { Id = Guid.NewGuid(), Name = "string-manipulation" },
            new Tag { Id = Guid.NewGuid(), Name = "file-handling" },
            new Tag { Id = Guid.NewGuid(), Name = "networking" },
            new Tag { Id = Guid.NewGuid(), Name = "regex" }
        };
    }
}
