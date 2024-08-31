using Candidates.API.Controllers;
using Candidates.API.Models;
using Candidates.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Candidates.Tests
{
    public class CandidatesControllerTests
    {
        private readonly CandidatesController _controller;
        private readonly Mock<ICandidateService> _mockCandidateService;

        public CandidatesControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidatesController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task CreateOrUpdate_ShouldCreateCandidate_WhenNotExists()
        {
            var candidate = new Candidate
            {
                Email = "awais.alshishani@gmail.com",
                FirstName = "Owais",
                LastName = "Alshishani",
                PhoneNumber = "1234567890",
                CallTimeInterval = "9AM-5PM",
                LinkedInProfileUrl = "https://www.linkedin.com/in/owaisalshishani/",
                GitHubProfileUrl = "https://github.com/Owais-AlShishani/Candidates.API",
                Comments = "test candidate"
            };

            _mockCandidateService.Setup(service => service.ExistsByEmail(candidate.Email))
                .ReturnsAsync(false);

            _mockCandidateService.Setup(service => service.Create(candidate))
                .ReturnsAsync(true);

            var result = await _controller.CreateOrUpdate(candidate);

            var createdResult = Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task CreateOrUpdate_ShouldUpdateCandidate_WhenExists()
        {
            var candidate = new Candidate
            {
                Email = "awais.alshishani@gmail.com",
                FirstName = "OwaisSs",
                LastName = "Alshishani",
                PhoneNumber = "1234567890",
                CallTimeInterval = "9AM-5PM",
                LinkedInProfileUrl = "https://www.linkedin.com/in/owaisalshishani/",
                GitHubProfileUrl = "https://github.com/Owais-AlShishani/Candidates.API",
                Comments = "updated candidate FirstName"
            };

            _mockCandidateService.Setup(service => service.ExistsByEmail(candidate.Email))
                .ReturnsAsync(true);

            _mockCandidateService.Setup(service => service.Update(candidate))
                .ReturnsAsync(candidate);

            var result = await _controller.CreateOrUpdate(candidate);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal(candidate.Email, returnValue.Email);
        }

        [Fact]
        public async Task CreateOrUpdate_ShouldReturnBadRequest_WhenUpdateFails()
        {
            var candidate = new Candidate
            {
                Email = "fail@example.com",
                FirstName = "Fail",
                LastName = "Case",
                PhoneNumber = "0000000000",
                CallTimeInterval = "Never",
                LinkedInProfileUrl = "https://www.linkedin.com/in/failcase",
                GitHubProfileUrl = "https://github.com/failcase",
                Comments = "This should fail"
            };

            _mockCandidateService.Setup(service => service.ExistsByEmail(candidate.Email))
                .ReturnsAsync(true);

            _mockCandidateService.Setup(service => service.Update(candidate))
                .ReturnsAsync((Candidate)null);

            var result = await _controller.CreateOrUpdate(candidate);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}