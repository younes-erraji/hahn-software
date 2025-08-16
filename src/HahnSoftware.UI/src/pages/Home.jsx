// src/pages/Home.jsx
import PostList from '../components/Blog/PostList';
import { Typography, Box } from '@mui/material';

export default function Home() {
  return (
    <Box sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        Blog Posts
      </Typography>
      <PostList />
    </Box>
  );
}