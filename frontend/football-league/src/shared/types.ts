export type LoginRequest = {
  username: string
  password: string
}

export type MatchCreateModel = {
  homeTeamId: number
  awayTeamId: number
  homeScore: number
  awayScore: number
  playedAt: string
}

export type MatchResponse = {
  id: number;
  homeTeam: { name: string }
  awayTeam: { name: string }
  homeScore: number
  awayScore: number
  playedAt: string
}

export type SignupRequest = {
  username: string
  password: string
}

export type TeamCreateModel = {
  name: string
}

export type TeamResponse = {
  id: number
  name: string
  points: number
}

export type MatchQuery = {
  searchTerm?: string
  page: number
  pageSize: number
  sortBy?: string
  sortDirection?: "asc" | "desc"
}

export type TeamQuery = {
  searchTerm?: string
  page: number
  pageSize: number
  sortBy?: string
  sortDirection?: "asc" | "desc"
}

export type TeamPaginationMetadata = {
  page: number
  pageSize: number
  totalCount: number
}

export type MatchPaginationMetadata = {
  page: number
  pageSize: number
  totalCount: number
}

export type PaginatedResponse<T, M> = {
  items: T[]
  metadata: M
}

export type Role = 'User' | 'Admin'

export interface DecodedToken {
  name: string
  nameid: string
  role: Role
  exp: number
}

export type TeamStandingModel = {
  name: string
  matchesPlayed: number
  wins: number
  draws: number
  losses: number
  points: number
}

export interface AuthContextType {
  user: DecodedToken | null
  token: string | null
  login: (token: string) => void
  logout: () => void
  isLoading: boolean
}