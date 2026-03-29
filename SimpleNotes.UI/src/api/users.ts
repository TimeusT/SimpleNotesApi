import axios, { AxiosError } from "axios";
import { User } from "../types/User";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const getUserByEmail = async (
  email: string,
  token: string,
): Promise<User | null> => {
  if (!email) throw new Error("Email is required");

  try {
    const response = await axios.get<User>(`${API_BASE_URL}/User/${email}`, {
      headers: { Authorization: `Bearer ${token}` },
    });

    return response.data;
  } catch (error: any) {
    const axiosError = error as AxiosError;
    if (axiosError.response?.status === 404) return null; // user not found
    if (axiosError.response?.status === 400) return null; // user not found
    throw error;
  }
};

export const createUser = async (user: User, token: string): Promise<User> => {
  if (!user) throw new Error("User is required");

  const response = await axios.post(`${API_BASE_URL}/User`, user, {
    headers: { Authorization: `Bearer ${token}` },
  });

  return response.data;
};
