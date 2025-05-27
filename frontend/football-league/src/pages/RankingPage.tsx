import { useEffect, useState } from 'react';

// UTILS
import { apiCalls } from '../shared/apiCalls';

// TYPES
import type { TeamStandingModel } from '../shared/types';

// COMPONENTS
import Container from '@mui/material/Container'
import Typography from '@mui/material/Typography'
import Table from '@mui/material/Table'
import TableHead from '@mui/material/TableHead'
import TableRow from '@mui/material/TableRow'
import TableCell from '@mui/material/TableCell'
import TableBody from '@mui/material/TableBody'
import Box from '@mui/material/Box'

const RankingPage = () => {
  const [standings, setStandings] = useState<TeamStandingModel[]>([]);

  useEffect(() => {
    apiCalls.getTeamRanking().then(setStandings).catch(err => {
      console.error("Failed to load rankings", err);
    });
  }, []);

  return (
    <Container>
      <Box mt={4} mb={2}>
        <Typography variant="h5">Team Rankings</Typography>
      </Box>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Rank</TableCell>
            <TableCell>Team</TableCell>
            <TableCell>Played</TableCell>
            <TableCell>Wins</TableCell>
            <TableCell>Draws</TableCell>
            <TableCell>Losses</TableCell>
            <TableCell>Points</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {standings.map((team, index) => (
            <TableRow key={team.name}>
              <TableCell>{index + 1}</TableCell>
              <TableCell>{team.name}</TableCell>
              <TableCell>{team.matchesPlayed}</TableCell>
              <TableCell>{team.wins}</TableCell>
              <TableCell>{team.draws}</TableCell>
              <TableCell>{team.losses}</TableCell>
              <TableCell>{team.points}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Container>
  );
};

export default RankingPage;
