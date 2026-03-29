// Using the API call, hook it into a usable state
import { useAuth0 } from "@auth0/auth0-react";
import { useQuery, UseQueryResult, useQueryClient, useMutation } from "@tanstack/react-query";
import { Note } from "../types/Note";
import useUserByEmail from "./useUserByEmail";
import { getUserNotes } from "../api/notes";

export function useGetUserNotes(): UseQueryResult<Note[], Error> {
  const { getAccessTokenSilently } = useAuth0();
  const {data: user} = useUserByEmail();

  return useQuery<Note[], Error>({
      queryKey: ["user", "note", user?.id],
      queryFn: async (): Promise<Note[]> => {
        if (!user?.id) throw new Error("User ID not available");
  
        const token = await getAccessTokenSilently();
        return getUserNotes(user.id, token); // already returns User | null
      },
      enabled: !!user?.id,
      staleTime: 60 * 1000, // user is immutable
      retry: false
    });
}