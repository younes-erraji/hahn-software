import { useState } from 'react';
import { createPost, updatePost } from '../../services/postService';
import { useNavigate } from 'react-router-dom';
import { TextField, Button, Container, Typography, Box, Chip } from '@mui/material';

export default function PostForm({ postToEdit }) {
  const [title, setTitle] = useState(postToEdit?.title || '');
  const [body, setBody] = useState(postToEdit?.body || '');
  const [tags, setTags] = useState(postToEdit?.tags || []);
  const [tagInput, setTagInput] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const postData = { title, body, tags };
      if (postToEdit) {
        postData.id = postToEdit.id;
        await updatePost(postData);
      } else {
        await createPost(postData);
      }
      navigate('/');
    } catch (err) {
      setError(err.response?.data?.message || 'Failed to save post');
    }
  };

  const handleAddTag = () => {
    if (tagInput && !tags.includes(tagInput)) {
      setTags([...tags, tagInput]);
      setTagInput('');
    }
  };

  const handleDeleteTag = (tagToDelete) => {
    setTags(tags.filter(tag => tag !== tagToDelete));
  };

  return (
    <Container maxWidth="md">
      <Box sx={{ mt: 4 }}>
        <Typography variant="h4" gutterBottom>
          {postToEdit ? 'Edit Post' : 'Create New Post'}
        </Typography>
        {error && <Typography color="error">{error}</Typography>}
        <form onSubmit={handleSubmit}>
          <TextField
            label="Title"
            fullWidth
            margin="normal"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
          />
          <TextField
            label="Content (Markdown supported)"
            fullWidth
            margin="normal"
            multiline
            rows={10}
            value={body}
            onChange={(e) => setBody(e.target.value)}
            required
          />
          <Box sx={{ mt: 2, mb: 2 }}>
            <TextField
              label="Add Tag"
              value={tagInput}
              onChange={(e) => setTagInput(e.target.value)}
              onKeyPress={(e) => e.key === 'Enter' && handleAddTag()}
            />
            <Button onClick={handleAddTag} sx={{ ml: 1 }}>
              Add
            </Button>
            <Box sx={{ mt: 1 }}>
              {tags.map((tag) => (
                <Chip
                  key={tag}
                  label={tag}
                  onDelete={() => handleDeleteTag(tag)}
                  sx={{ mr: 1, mb: 1 }}
                />
              ))}
            </Box>
          </Box>
          <Button type="submit" variant="contained" sx={{ mt: 2 }}>
            {postToEdit ? 'Update Post' : 'Publish'}
          </Button>
        </form>
      </Box>
    </Container>
  );
}