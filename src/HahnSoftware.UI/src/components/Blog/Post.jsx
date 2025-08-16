import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getPostBySlug, likePost, dislikePost, bookmarkPost, unbookmarkPost } from '../../services/postService';
import { Typography, Box, Button, IconButton } from '@mui/material';
import ReactMarkdown from 'react-markdown';
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import BookmarkIcon from '@mui/icons-material/Bookmark';
import BookmarkBorderIcon from '@mui/icons-material/BookmarkBorder';

export default function Post() {
  const { slug } = useParams();
  const [post, setPost] = useState(null);
  const [isLiked, setIsLiked] = useState(false);
  const [isDisliked, setIsDisliked] = useState(false);
  const [isBookmarked, setIsBookmarked] = useState(false);

  useEffect(() => {
    const fetchPost = async () => {
      try {
        const response = await getPostBySlug(slug);
        setPost(response.data);
      } catch (err) {
        console.error('Failed to fetch post:', err);
      }
    };

    fetchPost();
  }, [slug]);

  const handleLike = async () => {
    try {
      await likePost(post.id);
      setIsLiked(true);
      setIsDisliked(false);
      setPost(prev => ({
        ...prev,
        likes: prev.likes + 1,
        dislikes: isDisliked ? prev.dislikes - 1 : prev.dislikes
      }));
    } catch (err) {
      console.error('Failed to like post:', err);
    }
  };

  const handleDislike = async () => {
    try {
      await dislikePost(post.id);
      setIsLiked(false);
      setIsDisliked(true);
      setPost(prev => ({
        ...prev,
        dislikes: prev.dislikes + 1,
        likes: isLiked ? prev.likes - 1 : prev.likes
      }));
    } catch (err) {
      console.error('Failed to dislike post:', err);
    }
  };

  const handleBookmark = async () => {
    try {
      if (isBookmarked) {
        await unbookmarkPost(post.id);
      } else {
        await bookmarkPost(post.id);
      }
      setIsBookmarked(!isBookmarked);
    } catch (err) {
      console.error('Failed to toggle bookmark:', err);
    }
  };

  if (!post) return <div>Loading...</div>;

  return (
    <Box sx={{ mt: 4 }}>
      <Typography variant="h3" gutterBottom>
        {post.title}
      </Typography>
      <Typography variant="subtitle1" gutterBottom>
        By {post.user} • {new Date(post.creationDate).toLocaleDateString()}
      </Typography>

      <Box sx={{ display: 'flex', gap: 2, mb: 3 }}>
        <Button
          startIcon={<ThumbUpIcon />}
          color={isLiked ? 'primary' : 'inherit'}
          onClick={handleLike}
        >
          {post.likes}
        </Button>
        <Button
          startIcon={<ThumbDownIcon />}
          color={isDisliked ? 'secondary' : 'inherit'}
          onClick={handleDislike}
        >
          {post.dislikes}
        </Button>
        <IconButton onClick={handleBookmark}>
          {isBookmarked ? <BookmarkIcon color="primary" /> : <BookmarkBorderIcon />}
        </IconButton>
      </Box>

      <ReactMarkdown>{post.body}</ReactMarkdown>
      {post.tags.map(x => <span style={{
        padding: 10,
        backgroundColor: '#f0f0f0',
        borderRadius: 5,
        marginRight: 5,
        display: 'inline-block'
      }}>{x}</span>)}
    </Box>
  );
}