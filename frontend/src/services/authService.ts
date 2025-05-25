// Service for authentication API calls
import axios from 'axios';

// Use import.meta.env for Vite/React projects instead of process.env
const API_URL = import.meta.env.REACT_APP_API_URL || 'http://localhost:5198'; // Adjust if needed

export const signIn = async (email: string, password: string) => {
  return axios.post(`${API_URL}/api/Auth/login`, { email, password });
};

export const signUp = async (user: { nom: string; prenom: string; email: string; motDePasse: string; role: number }) => {
  return axios.post(`${API_URL}/api/Auth/register`, user);
};
