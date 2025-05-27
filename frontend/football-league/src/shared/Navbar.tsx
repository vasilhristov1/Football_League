import { Link, useNavigate } from 'react-router-dom';

// UTILS
import { useAuth } from '../auth/useAuth';

// COMPONENTS
import AppBar from '@mui/material/AppBar'
import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'
import Button from '@mui/material/Button'
import Box from '@mui/material/Box'

const Navbar = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          Football League
        </Typography>

        {user ? (
          <Box display="flex" gap={1}>
            <Button color="inherit" component={Link} to="/ranking">
              Ranking
            </Button>
            <Button color="inherit" component={Link} to="/teams">
              Teams
            </Button>
            <Button color="inherit" component={Link} to="/matches">
              Matches
            </Button>
            <Button color="inherit" onClick={handleLogout}>
              Logout
            </Button>
          </Box>
        ) : (
          <Box display="flex" gap={1}>
            <Button color="inherit" component={Link} to="/login">
              Login
            </Button>
            <Button color="inherit" component={Link} to="/signup">
              Signup
            </Button>
          </Box>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;