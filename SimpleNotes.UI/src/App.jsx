import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import Home from "./pages/Home";
import Dashboard from "./pages/Dashboard";
import ProtectedRoute from "./components/ProtectedRoute";
import CreateUser from "./pages/CreateUserForm";
import AuthGate from "./components/AuthGate";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route
          path="/dashboard"
          element={
            <AuthGate>
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            </AuthGate>
          }
        />
        <Route
          path="/create-user"
          element={
            <AuthGate>
              <ProtectedRoute>
                <CreateUser />
              </ProtectedRoute>
            </AuthGate>
          }
        />
      </Routes>
    </Router>
  );
}

export default App;
