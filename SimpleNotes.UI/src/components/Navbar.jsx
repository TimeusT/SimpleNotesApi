import { useAuth0 } from "@auth0/auth0-react";
import { Link } from "react-router-dom";
import useUserByEmail from "../hooks/useUserByEmail";

const Navbar = () => {
  const { loginWithRedirect, logout, isAuthenticated } = useAuth0();
  const { data: user } = useUserByEmail(); // Get cached DB user

  return (
    <nav
      style={{
        display: "flex",
        gap: "20px",
        padding: "1rem",
        borderBottom: "1px solid #ccc",
      }}
    >
      <Link to="/">Home</Link>
      {isAuthenticated && (
        <>
          <Link to="/create-note">Create Note</Link>
          {/* Show DB username if they exist */}
          <span>Logged in as: {user?.firstName || "New User"}</span>
          <button onClick={() => logout()}>Log Out</button>
        </>
      )}
      {!isAuthenticated && (
        <button onClick={() => loginWithRedirect()}>Log In</button>
      )}
    </nav>
  );
};

export default Navbar;
