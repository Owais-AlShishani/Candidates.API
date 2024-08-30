using Candidates.API.Data;
using Candidates.API.Models;
using Candidates.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Candidates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController(ICandidateService candidateService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(Candidate candidate)
        {
            bool exist = await candidateService.ExistsByEmail(candidate.Email);
            if (exist)
            {
                return await candidateService.Update(candidate) is not null ? Ok(candidate) : BadRequest();
            }
            return await candidateService.Create(candidate) ? Created() : BadRequest();

        }
    }
}
