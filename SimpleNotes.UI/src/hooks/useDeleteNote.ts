import { useQueryClient, useMutation, useQuery } from "@tanstack/react-query";
import { deleteNote } from "../api/notes";
import { useAuth0 } from "@auth0/auth0-react";
import useUserByEmail from "./useUserByEmail";
import { useMessage } from "./useMessage";

export function useDeleteNote() {
  const queryClient = useQueryClient();
  const { getAccessTokenSilently } = useAuth0();
  const { data: user } = useUserByEmail();
  const { showMessage } = useMessage();

  return useMutation({
    mutationFn: async (id: number) => {
      const token = await getAccessTokenSilently();
      return deleteNote(id, token);
    },
    onSuccess: () => {
      if (user?.id) {
        showMessage("Note Deleted.");
        queryClient.invalidateQueries({
          queryKey: ["user", "note", user.id],
        });
      }
    },
  });
}
