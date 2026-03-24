import { useQueryClient, useMutation, useQuery } from "@tanstack/react-query";
import { createNote } from "../api/notes";
import { useAuth0, User } from "@auth0/auth0-react";
import { Note } from "../types/Note";

export function useCreateNote() {
  const queryClient = useQueryClient();
  const { getAccessTokenSilently, user } = useAuth0();

  return useMutation({
    mutationFn: async (newNote: Note) => {
      const token = await getAccessTokenSilently();
      return createNote(newNote, token);
    },
    onSuccess: (createdNote) => {
      queryClient.setQueryData(
        ["note", createdNote.id],
        createdNote
      );
    },
  });
}