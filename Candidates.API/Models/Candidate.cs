using System.ComponentModel.DataAnnotations;

namespace Candidates.API.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [MaxLength(50)]
        public required string LastName { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public string? CallTimeInterval { get; set; }
        [Url]
        public string? LinkedInProfileUrl { get; set; }
        [Url]
        public string? GitHubProfileUrl { get; set; }
        public required string Comments { get; set; }
    }
}
