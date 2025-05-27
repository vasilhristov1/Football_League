import { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/useAuth';
import { apiCalls } from '../shared/apiCalls';

// COMPONENTS
import Container from '@mui/material/Container'
import Typography from '@mui/material/Typography'
import Table from '@mui/material/Table'
import TableHead from '@mui/material/TableHead'
import TableRow from '@mui/material/TableRow'
import TableCell from '@mui/material/TableCell'
import TableBody from '@mui/material/TableBody'
import Button from '@mui/material/Button'
import Box from '@mui/material/Box'
import IconButton from '@mui/material/IconButton'
import Pagination from '@mui/material/Pagination'
import Dialog from '@mui/material/Dialog'
import DialogTitle from '@mui/material/DialogTitle'
import DialogContent from '@mui/material/DialogContent'
import DialogActions from '@mui/material/DialogActions'
import TextField from '@mui/material/TextField'
import InputAdornment from '@mui/material/InputAdornment';
import SearchIcon from '@mui/icons-material/Search';
import StarIcon from '@mui/icons-material/Star';
import Tooltip from '@mui/material/Tooltip';

// ICONS
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

// TYPES & CONSTANTS
import { PAGE_SIZE } from '../shared/constants';
import type { TeamResponse } from '../shared/types';

const TeamPage = () => {
  const [teams, setTeams] = useState<TeamResponse[]>([]);
  const [page, setPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');
  const [sortDirection, setSortDirection] = useState<'asc' | 'desc'>('asc');
  const { user } = useAuth();
  const navigate = useNavigate();

  const [openEditDialog, setOpenEditDialog] = useState(false);
  const [editTeamId, setEditTeamId] = useState<number | null>(null);
  const [editTeamName, setEditTeamName] = useState('');

  const totalPages = Math.ceil(totalCount / PAGE_SIZE);

  const fetchTeams = useCallback(async (currentPage: number) => {
    try {
      const data = await apiCalls.getTeamsPaginated({
        page: currentPage,
        pageSize: PAGE_SIZE,
        searchTerm,
        sortBy: 'points',
        sortDirection,
      });
      setTeams(data.items);
      setTotalCount(data.metadata.totalCount);
    } catch (err) {
      console.error('Failed to fetch teams', err);
    }
  }, [searchTerm, sortDirection]);

  useEffect(() => {
    fetchTeams(page);
  }, [page, fetchTeams]);

  const handlePageChange = (_: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };

  const handleDelete = async (id: number) => {
    if (!window.confirm('Are you sure you want to delete this team?')) return;
    try {
      await apiCalls.deleteTeam(id);
      fetchTeams(page);
    } catch (err) {
      console.error('Failed to delete team', err);
    }
  };

  const openEditModal = (team: TeamResponse) => {
    setEditTeamId(team.id);
    setEditTeamName(team.name);
    setOpenEditDialog(true);
  };

  const handleEditSubmit = async () => {
    if (!editTeamId) return;
    try {
      await apiCalls.updateTeam(editTeamId, { name: editTeamName });
      setOpenEditDialog(false);
      fetchTeams(page);
    } catch (err) {
      console.error('Failed to update team', err);
    }
  };

  return (
    <Container>
      <Box display="flex" justifyContent="space-between" mt={4} mb={2}>
        <Typography variant="h5">Teams</Typography>
        {user?.role === 'Admin' && (
          <Button variant="contained" onClick={() => navigate('/teams/create')}>
            Add Team
          </Button>
        )}
      </Box>

      <Box display="flex" justifyContent="space-between" alignItems="center" mt={4} mb={2} flexWrap="wrap" gap={2}>
        <TextField
          placeholder="Search by name"
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

        <Tooltip title={`Sort by points (${sortDirection})`}>
          <IconButton
            onClick={() => {
              setSortDirection(prev => (prev === 'asc' ? 'desc' : 'asc'));
              setPage(1);
            }}
          >
            <StarIcon />
          </IconButton>
        </Tooltip>
      </Box>

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell>Points</TableCell>
            {user?.role === 'Admin' && <TableCell>Actions</TableCell>}
          </TableRow>
        </TableHead>
        <TableBody>
          {teams.map(team => (
            <TableRow key={team.id}>
              <TableCell>{team.name}</TableCell>
              <TableCell>{team.points}</TableCell>
              {user?.role === 'Admin' && (
                <TableCell>
                  <IconButton color="primary" onClick={() => openEditModal(team)}>
                    <EditIcon />
                  </IconButton>
                  <IconButton color="error" onClick={() => handleDelete(team.id)}>
                    <DeleteIcon />
                  </IconButton>
                </TableCell>
              )}
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

      <Dialog open={openEditDialog} onClose={() => setOpenEditDialog(false)}>
        <DialogTitle>Edit Team</DialogTitle>
        <DialogContent>
          <TextField
            fullWidth
            label="Team Name"
            value={editTeamName}
            onChange={(e) => setEditTeamName(e.target.value)}
            margin="normal"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenEditDialog(false)}>Cancel</Button>
          <Button variant="contained" onClick={handleEditSubmit}>Save</Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default TeamPage;
