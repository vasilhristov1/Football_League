using AutoMapper;
using football_league.Data.ViewModels;
using football_league.Managers.Abstractions;
using football_league.Data.Models;
using football_league.Data.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace football_league.Controllers;

using TeamsResponse = PaginatedResponse<TeamResultModel, TeamPaginationMetadata>;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TeamController(ITeamManager teamManager, IMapper mapper) : ControllerBase
{
    private readonly ITeamManager _teamManager = teamManager;
    private readonly IMapper _mapper = mapper;

    [HttpGet("paginated")]
    public async Task<IActionResult> GetAllPaginated([FromQuery] TeamQuery queryParams)
    {
        return Ok(_mapper.Map<TeamsResponse>(await _teamManager.GetAllTeamsAsync(queryParams)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_mapper.Map<TeamResultModel>(await _teamManager.GetAll()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var team = await _teamManager.GetTeamByIdAsync(id);
        
        if (team == null) return NotFound();
        
        return Ok(_mapper.Map<TeamResultModel>(team));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateTeamModel model)
    {
        var team = _mapper.Map<Team>(model);
        
        return Ok(_mapper.Map<TeamResultModel>(await _teamManager.CreateTeamAsync(team)));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamModel model)
    {
        var team = _mapper.Map<Team>(model);
        var result = await _teamManager.UpdateTeamAsync(id, team);
        
        return result != null ? Ok(_mapper.Map<TeamResultModel>(result)) : NotFound();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _teamManager.DeleteTeamAsync(id) ? Ok() : NotFound();
    }

    [HttpGet("ranking")]
    public async Task<IActionResult> GetRanking()
    {
        return Ok(await _teamManager.GetRankingsAsync());
    }
}