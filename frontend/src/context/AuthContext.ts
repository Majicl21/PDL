// Exports AuthContext for global auth state (optional, for larger apps)
import { createContext } from 'react';

interface AuthContextType {
  token: string | null;
  saveToken: (token: string) => void;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextType | undefined>(undefined);
