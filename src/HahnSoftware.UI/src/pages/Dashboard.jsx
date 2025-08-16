import { useAuthentication } from '../contexts/AuthenticationContext';
import { Link } from 'react-router-dom';
import { Button, Typography, Box } from '@mui/material';
import PostList from '../components/Blog/PostList';

export default function Dashboard() {
  const { authenticationUser } = useAuthentication();

  return (
    <Box sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        Welcome, {authenticationUser.firstName} {authenticationUser.lastName}.
      </Typography>
      <Button component={Link} to="/create-post" variant="contained">
        Create New Post
      </Button>
      <PostList />
    </Box>
  );
}