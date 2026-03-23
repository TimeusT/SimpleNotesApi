import { useAuth0 } from "@auth0/auth0-react";
import { Link } from "react-router-dom";
import AuthButton from '../components/AuthButton';

const Home = () => {
  const { user, isAuthenticated } = useAuth0();


    return(
      <div>
        <nav>
          <Link to="/">Home</Link> | <Link to="/dashboard">Dashboard</Link> |
          <Link to="/create-user">Create User</Link>  |
           <AuthButton />
        </nav>
        <h1>Home Page</h1>
        {isAuthenticated &&
        <div>
          <p>Welcome, <strong>{user.name}</strong></p>
          <p>Your nickname is <strong>{user.nickname}</strong></p>
        </div>}
      </div>
    );
  
};

export default Home;