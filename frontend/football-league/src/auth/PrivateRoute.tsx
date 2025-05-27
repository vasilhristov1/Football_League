import type { ReactNode } from "react";
import { Navigate } from "react-router-dom";

// UTILS
import { useAuth } from "./useAuth";

export const PrivateRoute = ({ children }: { children: ReactNode }) => {
  const { token } = useAuth();
  return token ? children : <Navigate to="/login" />
}