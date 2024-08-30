using Candidates.API.Models;
using Candidates.API.Repositories;

namespace Candidates.API.Services
{
    public class CandidateService(ICandidateRepository candidateRepository) : ICandidateService
    {
        public async Task<bool> Create(Candidate candidate)
        {
            return await candidateRepository.Create(candidate);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await candidateRepository.ExistsByEmail(email);
        }

        public async Task<Candidate?> Update(Candidate candidate)
        {

            var result = await candidateRepository.Update(candidate);
            if (result)
            {
                return candidate;
            }
            return null;
        }
    }
}
