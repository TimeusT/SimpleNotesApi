import { useQueryClient, useMutation } from "@tanstack/react-query";
import { editNote } from "../api/notes";
import { useAuth0 } from "@auth0/auth0-react";
import { Note } from "../types/Note";

export function useEditNote() {
  const queryClient = useQueryClient();
  const { getAccessTokenSilently } = useAuth0();

  return useMutation({
    mutationFn: async (note: Note) => {
      const token = await getAccessTokenSilently();
      return editNote(note, token);
    },
    onSuccess: (updatedNote: Note) => {
      queryClient.invalidateQueries({
        queryKey: ["user", "note", updatedNote.userId],
      });
    },
  });
}
