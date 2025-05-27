import type { ReactNode } from 'react';
import { Navigate } from 'react-router-dom';

// UTILS
import { useAuth } from '../auth/useAuth';

const ProtectedRoute = ({ children, roles }: { children: ReactNode; roles: string[] }) => {
  const { user, isLoading } = useAuth();

  if (isLoading) return null;

  if (!user || !roles.includes(user.role)) return <Navigate to="/login" />;
  return children;
};

export default ProtectedRoute;