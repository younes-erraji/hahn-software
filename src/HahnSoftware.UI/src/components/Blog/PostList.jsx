import { useEffect, useState } from 'react';
import { getPosts } from '../../services/postService';
import { Link } from 'react-router-dom';
import { List, Pagination, Box } from '@mui/material';

export default function PostList() {
  const [posts, setPosts] = useState([]);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(1);

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const response = await getPosts(pageSize, page);
        setPosts(response.data.content);
        setTotalPages(response.data.pageable.totalPages);
      } catch (exception) {
        console.error('Failed to fetch posts:', exception);
      }
    };

    fetchPosts();
  }, [page, pageSize]);

  const handlePageChange = (event, value) => {
    setPage(value);
  };

  return (
    <div>
      <List>
        {posts.map(post => (
          // <ListItem key={post.id} component={Link} to={`/post/${post.slug}`}>
          //   <ListItemText primary={post.title} secondary={post.bdoy} />
          // </ListItem>

          <Link key={post.id} to={`/post/${post.slug}`}>
            <h4>{post.title}</h4>
            <p>{post.body}</p>
            {post.tags.map(tag => <span key={tag} style={{
              padding: 10,
              backgroundColor: '#f0f0f0',
              borderRadius: 5,
              marginRight: 5,
              display: 'inline-block'
            }}>{tag}</span>)}
            <hr />
          </Link>
        ))}
      </List>
      <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2, mb: 2 }}>
        <Pagination
          count={totalPages}
          page={page}
          onChange={handlePageChange}
          color="primary"
        />
      </Box>
    </div>
  );
}