import { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

// TYPES
import type { MatchCreateModel, TeamResponse } from '../shared/types';

// UTILS
import { apiCalls } from '../shared/apiCalls';

// COMPONENTS
import Container from '@mui/material/Container';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import MenuItem from '@mui/material/MenuItem';

const MatchCreatePage = () => {
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<MatchCreateModel>();

  const navigate = useNavigate();
  const [teams, setTeams] = useState<TeamResponse[]>([]);

  const homeTeamId = watch('homeTeamId');
  const awayTeamId = watch('awayTeamId');

  useEffect(() => {
    apiCalls.getTeamsPaginated({ page: 1, pageSize: 100 }).then((res) => {
      setTeams(res.items);
    });
  }, []);

  const onSubmit = async (data: MatchCreateModel) => {
    await apiCalls.createMatch(data);
    navigate('/matches');
  };

  return (
    <Container maxWidth="sm">
      <Box mt={5}>
        <Typography variant="h5" mb={2}>
          Create Match
        </Typography>
        <form onSubmit={handleSubmit(onSubmit)}>
          <TextField
            select
            fullWidth
            label="Home Team"
            margin="normal"
            error={!!errors.homeTeamId}
            helperText={errors.homeTeamId?.message}
            {...register('homeTeamId', {
              required: 'Home Team is required',
              validate: value =>
                value !== awayTeamId || 'Home and Away teams must be different',
            })}
          >
            {teams.map((team) => (
              <MenuItem key={team.id} value={team.id}>
                {team.name}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            select
            fullWidth
            label="Away Team"
            margin="normal"
            error={!!errors.awayTeamId}
            helperText={errors.awayTeamId?.message}
            {...register('awayTeamId', {
              required: 'Away Team is required',
              validate: value =>
                value !== homeTeamId || 'Home and Away teams must be different',
            })}
          >
            {teams.map((team) => (
              <MenuItem key={team.id} value={team.id}>
                {team.name}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            fullWidth
            label="Home Score"
            type="number"
            margin="normal"
            {...register('homeScore', { required: true })}
          />

          <TextField
            fullWidth
            label="Away Score"
            type="number"
            margin="normal"
            {...register('awayScore', { required: true })}
          />

          <TextField
            fullWidth
            label="Played At"
            type="date"
            margin="normal"
            slotProps={{ inputLabel: { shrink: true } }}
            {...register('playedAt', { required: true })}
          />

          <Button type="submit" variant="contained" sx={{ mt: 2 }}>
            Create
          </Button>
        </form>
      </Box>
    </Container>
  );
};

export default MatchCreatePage;
