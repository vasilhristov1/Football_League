import { useContext } from 'react';
import { AuthContext } from './AuthContext';

// TYPES
import type { AuthContextType } from '../shared/types';

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth must be used within an AuthProvider');
  return context;
};