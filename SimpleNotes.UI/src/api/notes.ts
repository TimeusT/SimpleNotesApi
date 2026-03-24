import axios, { Axios, AxiosError } from "axios";
import { Note } from "../types/Note";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const createNote = async (note: Note, token: string): Promise<Note> => {
  if (!note) throw new Error("Note is required");

  const response = await axios.post(`${API_BASE_URL}/Note`, note, {
    headers: { Authorization: `Bearer ${token}` }
    //post user ID into note userId
  });

  return response.data;
};
