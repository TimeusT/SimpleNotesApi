import { Navigate } from 'react-router-dom';

export function RequireAuthNoUser({isAuthenticated, user, children}) {
  if (!isAuthenticated) return <Navigate to="/" />
  if (user) return <Navigate to="/create-note" />
  return children;
}

export function RequireAuthWithUser({ isAuthenticated, user, children }) {
  if (!isAuthenticated) return <Navigate to="/" />
  if (!user) return <Navigate to="/create-user" />
  return children;
}

export function GuardHomePage({ isAuthenticated, user, children }) {
  if (isAuthenticated && !user) return <Navigate to="/create-user" />
  return children;
}