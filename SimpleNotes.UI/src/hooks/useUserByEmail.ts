import { useQuery, UseQueryResult, useQueryClient, useMutation } from "@tanstack/react-query";
import { getUserByEmail, createUser } from "../api/users";
import { useAuth0 } from "@auth0/auth0-react";
import { User } from "../types/User";

export default function useUserByEmail(): UseQueryResult<User | null, Error> {
  const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();

  return useQuery<User | null, Error>({
    queryKey: ["user", user?.email],
    queryFn: async (): Promise<User | null> => {
      if (!user?.email) throw new Error("User email not available");

      const token = await getAccessTokenSilently();
      return getUserByEmail(user.email, token); // already returns User | null
    },
    enabled: isAuthenticated && !!user?.email,
    staleTime: Infinity, // user is immutable
    refetchOnWindowFocus: false,
    refetchOnReconnect: false,
    retry: false
  });
}

export function useCreateUser() {
  const queryClient = useQueryClient();
  const { getAccessTokenSilently, user } = useAuth0();

  return useMutation({
    mutationFn: async (newUser: User) => {
      const token = await getAccessTokenSilently();
      return createUser(newUser, token);
    },
    onSuccess: (createdUser) => {
      // ✅ instantly update cache (no refetch needed)
      queryClient.setQueryData(
        ["user", createdUser.email],
        createdUser
      );
    },
  });
}