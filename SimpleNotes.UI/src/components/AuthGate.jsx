import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { useNavigate, Outlet } from "react-router-dom";

export default function AuthGate({ children }) {
  const { isAuthenticated, user, getAccessTokenSilently } = useAuth0();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated || !user) return;

    getAccessTokenSilently()
      .then(token =>
        fetch(`https://localhost:7183/api/User/${user.email}`, {
          headers: { Authorization: `Bearer ${token}` },
        })
      )
      .then(res => {
        if (res.status === 400) {
          navigate("/create-user");
        } else if (res.ok) {
          navigate("/create-note");
        }
      })
      .catch(console.error);
  }, [isAuthenticated, user, getAccessTokenSilently, navigate]);

  return children; // renders nested routes inside AuthGate
}