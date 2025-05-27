import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

// TYPES
import type { TeamCreateModel } from '../shared/types';

// UTILS
import { apiCalls } from '../shared/apiCalls';

// COMPONENTS
import Container from '@mui/material/Container'
import TextField from '@mui/material/TextField'
import Typography from '@mui/material/Typography'
import Button from '@mui/material/Button'
import Box from '@mui/material/Box'

const TeamCreatePage = () => {
  const { register, handleSubmit } = useForm<TeamCreateModel>();
  const navigate = useNavigate();

  const onSubmit = async (data: TeamCreateModel) => {
    try {
      await apiCalls.createTeam(data);
      navigate('/teams');
    } catch (error) {
      console.error('Error creating team:', error);
      alert('Failed to create team.');
    }
  };

  return (
    <Container maxWidth="sm">
      <Box mt={5}>
        <Typography variant="h5" mb={2}>Create Team</Typography>
        <form onSubmit={handleSubmit(onSubmit)}>
          <TextField
            fullWidth
            label="Team Name"
            margin="normal"
            {...register('name', { required: true })}
          />
          <Button type="submit" variant="contained" sx={{ mt: 2 }}>
            Create
          </Button>
        </form>
      </Box>
    </Container>
  );
};

export default TeamCreatePage
