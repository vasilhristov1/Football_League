// UTILS
import { call } from "../apiCalls/axiosConfig"
import { createQueryString } from "./apiCallsUtils"

// TYPES
import type {
  LoginRequest,
  SignupRequest,
  MatchQuery,
  MatchCreateModel,
  MatchResponse,
  PaginatedResponse,
  TeamCreateModel,
  TeamQuery,
  TeamResponse,
  MatchPaginationMetadata,
  TeamPaginationMetadata,
  TeamStandingModel
} from './types'

const login = (credentials: LoginRequest): Promise<{ token: string }> =>
  call<{ token: string }>({
    url: `/auth/login`,
    method: "POST",
    data: credentials,
  }).then(res => res.data);

const signup = (data: SignupRequest): Promise<void> =>
  call<void>({
    url: `/auth/signup`,
    method: "POST",
    data,
  }).then(() => { });

const getTeamsPaginated = (query: TeamQuery) =>
  call<PaginatedResponse<TeamResponse, TeamPaginationMetadata>>({
    url: `/team/paginated${createQueryString(query)}`,
    method: "GET",
  }).then(res => res.data)

const getTeams = () =>
  call<TeamResponse[]>({
    url: `/team`,
    method: "GET",
  }).then(res => res.data)

const createTeam = (data: TeamCreateModel) =>
  call<void>({
    url: `/team`,
    method: "POST",
    data,
  })

const updateTeam = (teamId: number, data: TeamCreateModel) =>
  call<void>({
    url: `/team/${teamId}`,
    method: "PUT",
    data,
  })

const deleteTeam = (teamId: number) =>
  call<void>({
    url: `/team/${teamId}`,
    method: "DELETE",
  })

const getMatchesPaginated = (query: MatchQuery) =>
  call<PaginatedResponse<MatchResponse, MatchPaginationMetadata>>({
    url: `/match${createQueryString(query)}`,
    method: "GET",
  }).then(res => res.data)

const createMatch = (data: MatchCreateModel) =>
  call<void>({
    url: `/match`,
    method: "POST",
    data,
  })

const deleteMatch = (matchId: number) =>
  call<void>({
    url: `/match/${matchId}`,
    method: "DELETE",
  })

const getTeamRanking = (): Promise<TeamStandingModel[]> =>
  call<TeamStandingModel[]>({
    url: `/team/ranking`,
    method: "GET",
  }).then(res => res.data)

export const apiCalls = {
  login,
  signup,
  getTeamsPaginated,
  getTeams,
  createTeam,
  updateTeam,
  deleteTeam,
  getMatchesPaginated,
  createMatch,
  deleteMatch,
  getTeamRanking,
}