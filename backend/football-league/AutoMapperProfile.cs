using AutoMapper;
using football_league.Data.ViewModels;
using football_league.Data.Models;
using football_league.Data.Models.DTOs;
using football_league.Data.Models.Enums;

namespace football_league;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        AddGlobalIgnore("CreatedDateTime");

        CreateUtilityMaps();
        CreateMatchesMaps();
        CreateTeamsMaps();
        CreateUsersMaps();
    }

    private void CreateUtilityMaps()
    {
        CreateMap(typeof(PaginatedResponse<,>), typeof(PaginatedResponse<,>));
    }
    
    private void CreateTeamsMaps()
    {
        CreateMap<Team, TeamResultModel>();
        CreateMap<UpdateTeamModel, Team>();
        CreateMap<CreateTeamModel, Team>();
    }

    private void CreateMatchesMaps()
    {
        CreateMap<Match, MatchResultModel>();
        CreateMap<CreateMatchModel, Match>();
    }

    private void CreateUsersMaps()
    {
        CreateMap<RegisterUserModel, User>()
            .ForMember(x => x.PasswordHash, opt => opt.MapFrom(x => x.Password))
            .ForMember(x => x.Role, opt => opt.MapFrom(_ => Role.User));
    }
}