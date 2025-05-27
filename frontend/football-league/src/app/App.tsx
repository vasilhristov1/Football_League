import { Routes, Route, Navigate } from 'react-router-dom';

// COMPONENTS
import Navbar from '../shared/Navbar';
import LoginPage from '../pages/LoginPage';
import SignupPage from '../pages/SignupPage';
import TeamPage from '../pages/TeamPage';
import MatchPage from '../pages/MatchPage';
import ProtectedRoute from '../auth/ProtectedRoute';
import TeamCreatePage from '../pages/TeamCreatePage';
import MatchCreatePage from '../pages/MatchCreatePage';
import { PrivateRoute } from '../auth/PrivateRoute';
import RankingPage from '../pages/RankingPage';

function App() {
  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/" element={<Navigate to="/ranking" />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/signup" element={<SignupPage />} />
        <Route path="/teams" element={<PrivateRoute><TeamPage /></PrivateRoute>} />
        <Route path="/matches" element={<PrivateRoute><MatchPage /></PrivateRoute>} />
        <Route path="/ranking" element={<PrivateRoute><RankingPage /></PrivateRoute>} />
        <Route path="/teams/create" element={
          <ProtectedRoute roles={['Admin']}>
            <TeamCreatePage />
          </ProtectedRoute>
        } />
        <Route path="/matches/create" element={
          <ProtectedRoute roles={['Admin']}>
            <MatchCreatePage />
          </ProtectedRoute>
        } />
      </Routes>
    </>
  )
}

export default App
