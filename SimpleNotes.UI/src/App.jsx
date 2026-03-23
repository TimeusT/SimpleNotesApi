import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link,
  Navigate,
} from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import useUserByEmail from "./hooks/useUserByEmail"; // The hook we created earlier
import Home from "./pages/Home";
import CreateUser from "./pages/CreateUserForm";
import CreateNote from "./pages/CreateNoteForm";
import Layout from "./components/Layout";

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
          {/* 1. Home: Accessible to everyone, but redirects if logged in */}
          <Route
            path="/"
            element={
              !isAuthenticated ? <Home /> : <Navigate to="/check-status" />
            }
          />
          {/* 2. Traffic Controller: Logic to decide where a logged-in user goes */}
          <Route
            path="/check-status"
            element={
              user ? (
                <Navigate to="/create-note" />
              ) : (
                <Navigate to="/create-user" />
              )
            }
          />
          {/* 3. Create User: Only if Authenticated but NOT in DB */}
          <Route
            path="/create-user"
            element={
              isAuthenticated && !user ? <CreateUser /> : <Navigate to="/" />
            }
          />
          {/* 4. Create Note: Only if Authenticated AND in DB */}
          <Route
            path="/create-note"
            element={
              isAuthenticated && user ? (
                <CreateNote />
              ) : (
                <Navigate to="/check-status" />
              )
            }
          />
          {/* Fallback */}
          <Route path="*" element={<Navigate to="/" />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
