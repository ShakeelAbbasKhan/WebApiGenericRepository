using CourseProject.Common.Dtos.Address;
using CourseProject.Common.Dtos.Team;
using CourseProject.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTeam(TeamCreate teamCreate)
        {
            var id = await _teamService.CreateTeamAsync(teamCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateTeam(TeamUpdate teamUpdate)
        {
            await _teamService.UpdateTeamAsync(teamUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteTeam(TeamDelete teamDelete)
        {
            await _teamService.DeleteTeamAsync(teamDelete);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var team =await  _teamService.GetTeamAsync(id);
            return Ok(team);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _teamService.GetTeamsAsync();
            return Ok(teams);
        }
    }
}
