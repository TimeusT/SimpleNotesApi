import { useQueryClient, useMutation } from "@tanstack/react-query";
import { createNote } from "../api/notes";
import { useAuth0 } from "@auth0/auth0-react";
import { Note } from "../types/Note";

export function useCreateNote() {
  const queryClient = useQueryClient();
  const { getAccessTokenSilently } = useAuth0();

  return useMutation({
    mutationFn: async (newNote: Note) => {
      const token = await getAccessTokenSilently();
      return createNote(newNote, token);
    },
    onSuccess: (createdNote: Note) => {
      queryClient.invalidateQueries({
        queryKey: ["user", "note", createdNote.userId],
      });
    },
  });
}
