using Candidates.API.Models;

namespace Candidates.API.Repositories
{
    public interface ICandidateRepository
    {
        Task<bool> Create(Candidate candidate);
        Task<bool> Update(Candidate candidate);
        Task<bool> ExistsByEmail(string email);
    }
}
