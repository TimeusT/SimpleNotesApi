import { Outlet } from "react-router-dom";
import Navbar from "./Navbar";
import { Box, Container } from "@mui/material";

export default function Layout() {
  return (
    <Container
      maxWidth="lg"
      disableGutters // removes left/right padding
      sx={{
        height: "100vh",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <Navbar />
      <main>
        <Outlet /> {/* This is where Home, CreateUser, etc., will render */}
      </main>
    </Container>
  );
}
