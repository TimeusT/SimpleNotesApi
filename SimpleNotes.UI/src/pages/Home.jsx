import { useAuth0 } from "@auth0/auth0-react";

const Home = () => {
  const { user, isAuthenticated } = useAuth0();

  if (isAuthenticated) {
    return(
      <div>
        <h1>Home Page</h1>
        <p>Welcome, <strong>{user.name}</strong></p>
        <p>Your nickname is <strong>{user.nickname}</strong></p>
      </div>
    );
  }

  return<h1>Home Page</h1>;
};

export default Home;