import type { ReactNode } from "react";
import { Navigate } from "react-router-dom";

// UTILS
import { useAuth } from "./useAuth";

export const PrivateRoute = ({ children }: { children: ReactNode }) => {
  const { token, isLoading } = useAuth();
  
  if (isLoading) return null
  
  return token ? children : <Navigate to="/login" />
}