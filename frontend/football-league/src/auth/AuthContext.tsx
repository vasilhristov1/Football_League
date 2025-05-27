import React, { createContext, useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';

// TYPES
import type { AuthContextType, DecodedToken } from '../shared/types';

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<DecodedToken | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    if (storedToken) {
      try {
        const decoded = jwtDecode<DecodedToken>(storedToken);
        if (decoded.exp * 1000 > Date.now()) {
          setUser(decoded);
          setToken(storedToken);
        } else {
          localStorage.removeItem('token');
        }
      } catch {
        localStorage.removeItem('token');
      }
    }

    setIsLoading(false);
  }, []);

  const login = (newToken: string) => {
    const decoded = jwtDecode<DecodedToken>(newToken);
    setUser(decoded);
    setToken(newToken);
    localStorage.setItem('token', newToken);
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    localStorage.removeItem('token');
  };

  return (
    <AuthContext.Provider value={{ user, token, login, logout, isLoading }}>
      {children}
    </AuthContext.Provider>
  );
};