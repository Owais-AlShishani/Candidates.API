using Candidates.API.Models;

namespace Candidates.API.Services
{
    public interface ICandidateService
    {
        Task<bool> Create(Candidate candidate);
        Task<Candidate?> Update(Candidate candidate);
        Task<bool> ExistsByEmail(string email);
    }
}
