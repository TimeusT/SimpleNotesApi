import { useAuth0 } from "@auth0/auth0-react";

const Home = () => {
  const { user, isAuthenticated } = useAuth0();

  return (
    <>
      <h1>Home Page</h1>
      {isAuthenticated && (
        <div>
          <p>
            Welcome, <strong>{user.name}</strong>
          </p>
          <p>
            Your nickname is <strong>{user.nickname}</strong>
          </p>
        </div>
      )}
    </>
  );
};

export default Home;
