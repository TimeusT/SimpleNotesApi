import { useAuth0 } from '@auth0/auth0-react';

const Dashboard = () => {
  const { user } = useAuth0();

  return(
    <div>
      <h1>Dashboard</h1>
      <p>Welcome, {user.name}</p>
    </div>
  );
};

export default Dashboard;