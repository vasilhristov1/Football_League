import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

// COMPONENTS
import Button from '@mui/material/Button'
import Container from '@mui/material/Container'
import TextField from '@mui/material/TextField'
import Typography from '@mui/material/Typography'
import Box from '@mui/material/Box'

// UTILS
import { apiCalls } from '../shared/apiCalls';

// TYPES
import type { SignupRequest } from '../shared/types';

const SignupPage = () => {
  const navigate = useNavigate();
  const { register, handleSubmit, formState: { errors } } = useForm<SignupRequest>();

  const onSubmit = async (data: SignupRequest) => {
    try {
      await apiCalls.signup(data)
      alert('Account created successfully. Please log in.')
      navigate('/login')
    } catch {
      alert('Username already taken.')
    }
  };

  return (
    <Container maxWidth="xs">
      <Box mt={8}>
        <Typography variant="h5" mb={3}>Sign Up</Typography>
        <form onSubmit={handleSubmit(onSubmit)}>
          <TextField
            fullWidth
            label="Username"
            margin="normal"
            {...register('username', { required: 'Username is required' })}
            error={!!errors.username}
            helperText={errors.username?.message}
          />
          <TextField
            fullWidth
            label="Password"
            type="password"
            margin="normal"
            {...register('password', { required: 'Password is required' })}
            error={!!errors.password}
            helperText={errors.password?.message}
          />
          <Button type="submit" variant="contained" fullWidth sx={{ mt: 2 }}>
            Sign Up
          </Button>
        </form>
      </Box>
    </Container>
  );
};

export default SignupPage;