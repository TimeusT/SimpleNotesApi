import axios, { Axios, AxiosError } from "axios";
import { Note } from "../types/Note";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const createNote = async (note: Note, token: string): Promise<Note> => {
  if (!note) throw new Error("Note is required");

  const response = await axios.post(`${API_BASE_URL}/Note`, note, {
    headers: { Authorization: `Bearer ${token}` },
  });

  return response.data;
};

export const getUserNotes = async (
  userId: number,
  token: string,
): Promise<Note[]> => {
  try {
    const response = await axios.get(`${API_BASE_URL}/User/${userId}/note`, {
      headers: { Authorization: `Bearer ${token}` },
    });

    return response.data;
  } catch (error) {
    const axiosError = error as AxiosError;
    if (axiosError.response?.status === 404) return []; // user not found
    if (axiosError.response?.status === 400) return []; // user not found
    throw error;
  }
};

export const deleteNote = async (id: number, token: string): Promise<null> => {
  const response = await axios.delete(`${API_BASE_URL}/Note/${id}`, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return response.data;
}

export const editNote = async (note: Note, token: string): Promise<Note> => {
  const response = await axios.put(`${API_BASE_URL}/Note/${note.id}`, note, {
    headers: { Authorization: `Bearer ${token}` },
  });
  return response.data;
};