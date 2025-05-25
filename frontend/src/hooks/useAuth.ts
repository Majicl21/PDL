// Handles authentication state and token storage
import { useState } from 'react';

export function useAuth() {
  const [token, setToken] = useState<string | null>(() => localStorage.getItem('token'));

  const saveToken = (jwt: string) => {
    setToken(jwt);
    localStorage.setItem('token', jwt);
  };

  const logout = () => {
    setToken(null);
    localStorage.removeItem('token');
  };

  return { token, saveToken, logout };
}
