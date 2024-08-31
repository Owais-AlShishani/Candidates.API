using Candidates.API.Data;
using Candidates.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Candidates.API.Repositories
{
    public class CandidateRepository(ApplicationDbContext dbContext) : ICandidateRepository
    {
        public async Task<bool> Create(Candidate candidate)
        {
            dbContext.Add(candidate);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await dbContext.Candidates.AnyAsync(c => c.Email == email);
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
            return await dbContext.SaveChangesAsync() > 0;

        }
    }
}
