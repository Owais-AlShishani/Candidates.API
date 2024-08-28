using Candidates.API.Data;
using Candidates.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Candidates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController(ApplicationDbContext dbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCandidate = await dbContext.Candidates.SingleOrDefaultAsync(c => c.Email == candidate.Email);
            if (existingCandidate is null)
            {
                // Creation
                dbContext.Add(candidate);
                int affected = await dbContext.SaveChangesAsync();
                return Created();
            }

            else
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comments = candidate.Comments;
                int afftected =await dbContext.SaveChangesAsync();
                return Ok(candidate);
            }
        }
    }
}
