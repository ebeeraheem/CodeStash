using CodeStash.Core.Entities;

namespace CodeStash.Infrastructure.Seeder;
public static class SeedTags
{
    public static List<Tag> GetInitialTags()
    {
        return new List<Tag>
        {
            // Patterns & Architecture
            new Tag { Id = Guid.NewGuid().ToString(), Name = "design-patterns" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "solid-principles" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "clean-code" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "clean-architecture" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "dry" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "mvc" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "mvvm" },
            
            // Security
            new Tag { Id = Guid.NewGuid().ToString(), Name = "authentication" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "authorization" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "encryption" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "hashing" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "jwt" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "oauth" },
            
            // Performance
            new Tag { Id = Guid.NewGuid().ToString(), Name = "caching" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "optimization" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "memory-management" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "performance" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "lazy-loading" },
            
            // Data & Storage
            new Tag { Id = Guid.NewGuid().ToString(), Name = "database" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "orm" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "linq" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "entity-framework" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "migration" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "serialization" },
            
            // Testing
            new Tag { Id = Guid.NewGuid().ToString(), Name = "unit-testing" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "integration-testing" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "e2e-testing" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "mocking" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "test-doubles" },
            
            // Error Handling
            new Tag { Id = Guid.NewGuid().ToString(), Name = "error-handling" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "exception" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "logging" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "debugging" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "monitoring" },
            
            // Web Development
            new Tag { Id = Guid.NewGuid().ToString(), Name = "api" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "rest" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "graphql" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "websocket" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "http" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "middleware" },
            
            // Frontend
            new Tag { Id = Guid.NewGuid().ToString(), Name = "react" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "angular" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "vue" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "state-management" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "ui" },
            
            // Utilities & Helpers
            new Tag { Id = Guid.NewGuid().ToString(), Name = "helper" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "utility" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "extension-method" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "validation" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "formatting" },
            
            // Algorithms
            new Tag { Id = Guid.NewGuid().ToString(), Name = "algorithm" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "sorting" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "searching" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "data-structure" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "recursion" },
            
            // DevOps & Deployment
            new Tag { Id = Guid.NewGuid().ToString(), Name = "ci-cd" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "docker" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "kubernetes" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "deployment" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "configuration" },
            
            // Common Functionality
            new Tag { Id = Guid.NewGuid().ToString(), Name = "date-time" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "string-manipulation" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "file-handling" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "networking" },
            new Tag { Id = Guid.NewGuid().ToString(), Name = "regex" }
        };
    }
}
