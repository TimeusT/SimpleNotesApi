import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import useUserByEmail from "./hooks/useUserByEmail";
import Home from "./pages/Home";
import CreateUser from "./pages/CreateUserForm";
import CreateNote from "./pages/CreateNoteForm";
import Layout from "./components/Layout";
import {
  GuardHomePage,
  RequireAuthNoUser,
  RequireAuthWithUser,
} from "./components/guard";

function App() {
  const { isAuthenticated, isLoading: authLoading } = useAuth0();
  const { data: user, isLoading: userLoading } = useUserByEmail();

  if (authLoading || (isAuthenticated && userLoading)) {
    return <div>Loading...</div>;
  }

  return (
    <Router>
      <Routes>
        <Route element={<Layout />}>
          <Route
            path="/"
            element={
              <GuardHomePage
                isAuthenticated={isAuthenticated}
                user={user}>
                <Home />
              </GuardHomePage>
            }
          />
          <Route
            path="/create-user"
            element={
              <RequireAuthNoUser isAuthenticated={isAuthenticated} user={user}>
                <CreateUser />
              </RequireAuthNoUser>
            }
          />
          <Route
            path="/create-note"
            element={
              <RequireAuthWithUser
                isAuthenticated={isAuthenticated}
                user={user}
              >
                <CreateNote />
              </RequireAuthWithUser>
            }
          />
          <Route path="*" element={<Navigate to="/" />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
