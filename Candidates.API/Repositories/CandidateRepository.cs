using Candidates.API.Data;
using Candidates.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Candidates.API.Repositories
{
    public class CandidateRepository(ApplicationDbContext dbContext, IMemoryCache cache) : ICandidateRepository
    {
        public async Task<bool> Create(Candidate candidate)
        {
            dbContext.Add(candidate);
            cache.Remove($"Candidate_Exists_{candidate.Email}");
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var cacheKey = $"Candidate_Exists_{email}";

            if (cache.TryGetValue(cacheKey, out bool exists))
            {
                return exists;
            }

            exists = await dbContext.Candidates.AnyAsync(c => c.Email == email);

            cache.Set(cacheKey, exists, TimeSpan.FromMinutes(5));

            return exists;
        }

        public async Task<bool> Update(Candidate candidate)
        {
            var existingCandidate = await dbContext.Candidates.SingleOrDefaultAsync(c => c.Email == candidate.Email);

            if (existingCandidate is not null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comments = candidate.Comments;
            }
            cache.Remove($"Candidate_Exists_{candidate.Email}");
            return await dbContext.SaveChangesAsync() > 0;

        }
    }
}
