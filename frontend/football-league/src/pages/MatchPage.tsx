import { useEffect, useState } from 'react';
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

const PAGE_SIZE = 10;

const MatchPage = () => {
  const [matches, setMatches] = useState<MatchResponse[]>([]);
  const [page, setPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const { user } = useAuth();
  const navigate = useNavigate();

  const totalPages = Math.ceil(totalCount / PAGE_SIZE);

  const fetchMatches = async (currentPage: number) => {
    try {
      const data = await apiCalls.getMatchesPaginated({
        page: currentPage,
        pageSize: PAGE_SIZE,
      });
      setMatches(data.items);
      setTotalCount(data.metadata.totalCount);
    } catch (err) {
      console.error('Error fetching matches:', err);
    }
  };

  useEffect(() => {
    fetchMatches(page);
  }, [page]);

  const handlePageChange = (_: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
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
              <TableCell>
                {match.homeScore} - {match.awayScore}
              </TableCell>
              <TableCell>{match.awayTeam.name}</TableCell>
              <TableCell>
                {new Date(match.playedAt).toLocaleDateString()}
              </TableCell>
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
