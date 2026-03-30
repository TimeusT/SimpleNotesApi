import { Outlet } from "react-router-dom";
import Navbar from "./Navbar";
import { Box, Container } from "@mui/material";

export default function Layout() {
  return (
    <>
    <Navbar/>
      <main>
        <Outlet /> {/* This is where Home, CreateUser, etc., will render */}
      </main>
    </> 
  );
}
