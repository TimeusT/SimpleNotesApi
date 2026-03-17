import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import ProtectedRoute from './components/ProtectedRoute';
import Home from './pages/Home';
import AuthButton from './components/AuthButton';
import CreateNote from './pages/CreateNoteForm';
import CreateUser from './pages/CreateUserForm';

function App() {
  const { isAuthenticated, user:AuthUser } = useAuth0();

  const getUser = () => axios.get("https://localhost:7183/api/User").then(x => x.data);
  const { data: user } = useQuery(['users'], getUsers);

  const emailExists = isAuthenticated && users?.some(u => u.email === authUser.email);
  
  return(
    <Router>
      <nav>
        <Link to="/">Home</Link> | 
        {isAuthenticated && emailExists && ( // IF logged in AND email = user.GetEmail(email)
          <Link to="/create-note">Create Note</Link>
        )}
        
        {isAuthenticated && !emailExists && ( // IF logged in AND email != user.GetEmail(email)
          <Link path="/create-user" element={<ProtectedRoute><CreateUser /></ProtectedRoute>}></Link>
        )}
        <AuthButton />
      </nav>

      <Routes>
        {isAuthenticated && emailExists && (
          <Route path="/create-note" element={<ProtectedRoute><CreateNote /></ProtectedRoute>} />
        )}

        {isAuthenticated && !emailExists &&(
          <Route path="/" element={<Home />} />
        )}
      </Routes>
    </Router>
  );
}

export default App;