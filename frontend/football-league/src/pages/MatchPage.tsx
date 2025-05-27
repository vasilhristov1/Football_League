import { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

// TYPES
import type { MatchResponse } from '../shared/types';

// UTILS
import { useAuth } from '../auth/useAuth';
import { apiCalls } from '../shared/apiCalls';

// COMPONENTS
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Table from '@mui/material/Table';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import TableBody from '@mui/material/TableBody';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Pagination from '@mui/material/Pagination';
import TextField from '@mui/material/TextField';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import Tooltip from '@mui/material/Tooltip';

// ICONS
import SearchIcon from '@mui/icons-material/Search';
import EventIcon from '@mui/icons-material/Event';
import HomeIcon from '@mui/icons-material/Home';
import FlightTakeoffIcon from '@mui/icons-material/FlightTakeoff';

const PAGE_SIZE = 10;

const MatchPage = () => {
  const [matches, setMatches] = useState<MatchResponse[]>([]);
  const [page, setPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');
  const [sortBy, setSortBy] = useState<string>('date');
  const [sortDirection, setSortDirection] = useState<'asc' | 'desc'>('desc');

  const { user } = useAuth();
  const navigate = useNavigate();

  const totalPages = Math.ceil(totalCount / PAGE_SIZE);

  const fetchMatches = useCallback(async (currentPage: number) => {
    try {
      const data = await apiCalls.getMatchesPaginated({
        page: currentPage,
        pageSize: PAGE_SIZE,
        searchTerm,
        sortBy,
        sortDirection,
      });
      setMatches(data.items);
      setTotalCount(data.metadata.totalCount);
    } catch (err) {
      console.error('Error fetching matches:', err);
    }
  }, [searchTerm, sortBy, sortDirection]);

  useEffect(() => {
    fetchMatches(page);
  }, [page, fetchMatches]);

  const handlePageChange = (_: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };

  const toggleSort = (field: string) => {
    if (sortBy === field) {
      setSortDirection(prev => (prev === 'asc' ? 'desc' : 'asc'));
    } else {
      setSortBy(field);
      setSortDirection('asc');
    }
    setPage(1);
  };

  return (
    <Container>
      <Box display="flex" justifyContent="space-between" mt={4} mb={2}>
        <Typography variant="h5">Matches</Typography>
        {user?.role === 'Admin' && (
          <Button variant="contained" onClick={() => navigate('/matches/create')}>
            Add Match
          </Button>
        )}
      </Box>

      <Box display="flex" alignItems="center" gap={2} mb={2} flexWrap="wrap">
        <TextField
          placeholder="Search teams"
          size="small"
          value={searchTerm}
          onChange={(e) => {
            setSearchTerm(e.target.value);
            setPage(1);
          }}
          slotProps={{
            input: {
              startAdornment: (
                <InputAdornment position="start">
                  <SearchIcon />
                </InputAdornment>
              )
            }
          }}
        />


        <Tooltip title={`Sort by date (${sortDirection})`}>
          <IconButton onClick={() => toggleSort('date')}>
            <EventIcon />
          </IconButton>
        </Tooltip>

        <Tooltip title={`Sort by home score (${sortDirection})`}>
          <IconButton onClick={() => toggleSort('homeScore')}>
            <HomeIcon />
          </IconButton>
        </Tooltip>

        <Tooltip title={`Sort by away score (${sortDirection})`}>
          <IconButton onClick={() => toggleSort('awayScore')}>
            <FlightTakeoffIcon />
          </IconButton>
        </Tooltip>
      </Box>

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Home</TableCell>
            <TableCell>Score</TableCell>
            <TableCell>Away</TableCell>
            <TableCell>Date</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {matches.map((match) => (
            <TableRow key={match.id}>
              <TableCell>{match.homeTeam.name}</TableCell>
              <TableCell>{match.homeScore} - {match.awayScore}</TableCell>
              <TableCell>{match.awayTeam.name}</TableCell>
              <TableCell>{new Date(match.playedAt).toLocaleDateString()}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <Box mt={3} display="flex" justifyContent="center">
        <Pagination
          count={totalPages}
          page={page}
          onChange={handlePageChange}
          color="primary"
        />
      </Box>
    </Container>
  );
};

export default MatchPage;
