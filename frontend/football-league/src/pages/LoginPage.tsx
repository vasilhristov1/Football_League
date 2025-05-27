import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

// COMPONENTS
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';

// UTILS
import { useAuth } from '../auth/useAuth';
import { apiCalls } from '../shared/apiCalls';

// TYPES
import type { LoginRequest } from '../shared/types';

const LoginPage = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const { register, handleSubmit, formState: { errors } } = useForm<LoginRequest>();

  const onSubmit = async (data: LoginRequest) => {
    try {
      const response = await apiCalls.login(data)
      login(response.token);
      navigate('/ranking');
    } catch {
      alert('Invalid credentials.');
    }
  };

  return (
    <Container maxWidth="xs">
      <Box mt={8}>
        <Typography variant="h5" mb={3}>Login</Typography>
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
            Login
          </Button>
        </form>
      </Box>
    </Container>
  );
};

export default LoginPage;
