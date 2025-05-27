using AutoMapper;
using football_league.Data.ViewModels;
using football_league.Managers.Abstractions;
using football_league.Data.Models;
using football_league.Data.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace football_league.Controllers;

using MatchesResponse = PaginatedResponse<MatchResultModel, MatchPaginationMetadata>;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MatchController(IMatchManager matchManager, IMapper mapper) : ControllerBase
{
    private readonly IMatchManager _matchManager = matchManager;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MatchQuery queryParams)
    {
        return Ok(_mapper.Map<MatchesResponse>(await _matchManager.GetAllMatchesAsync(queryParams)));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateMatch([FromBody] CreateMatchModel model)
    {
        if (model.HomeTeamId == model.AwayTeamId)
            return BadRequest("A team cannot play against itself.");

        return Ok(_mapper.Map<MatchResultModel>(await _matchManager.CreateMatchAsync(_mapper.Map<Match>(model))));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMatch(int id)
    {
        return await _matchManager.DeleteMatchAsync(id) ? Ok() : NotFound();
    }
}