import { Outlet } from "react-router-dom";
import Navbar from "./Navbar";

const Layout = () => {
  return (
    <>
      <Navbar />
      <main style={{ padding: "20px" }}>
        <Outlet /> {/* This is where Home, CreateUser, etc., will render */}
      </main>
    </>
  );
};

export default Layout;
