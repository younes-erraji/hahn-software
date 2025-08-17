import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getPostBySlug, likePost, dislikePost, bookmarkPost, unbookmarkPost } from '../../services/postService';
import { createComment, getComments } from '../../services/commentService';
import { Typography, Box, Button, IconButton, TextField } from '@mui/material';
import ReactMarkdown from 'react-markdown';
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import BookmarkIcon from '@mui/icons-material/Bookmark';
import BookmarkBorderIcon from '@mui/icons-material/BookmarkBorder';
import { useAuthentication } from '../../contexts/AuthenticationContext';

export default function Post() {
  const { slug } = useParams();
  const [comment, setComment] = useState('');
  const [comments, setComments] = useState(null);
  const [post, setPost] = useState(null);
  const [isLiked, setIsLiked] = useState(false);
  const [isDisliked, setIsDisliked] = useState(false);
  const [isBookmarked, setIsBookmarked] = useState(false);
  const { authentication } = useAuthentication();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPost = async () => {
      try {
        const response = await getPostBySlug(slug);
        setPost(response.data);
      } catch (exception) {
        console.error('Failed to fetch post:', exception);
      }
    };

    fetchPost();
  }, [slug]);

  useEffect(() => {
    const fetchComments = async () => {
      try {
        const response = await getComments(post.id);
        setComments(response.data);
      } catch (exception) {
        console.error('Failed to fetch comments:', exception);
      }
    };

    fetchComments();
  }, [slug, comments]);

  const handleLike = async () => {
    if (authentication) {
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
    } else {
      navigate("/login")
    }
  };

  const handleDislike = async () => {
    if (authentication) {
      try {
        await dislikePost(post.id);
        setIsLiked(false);
        setIsDisliked(true);
        setPost(prev => ({
          ...prev,
          dislikes: prev.dislikes + 1,
          likes: isLiked ? prev.likes - 1 : prev.likes
        }));
      } catch (exception) {
        console.error('Failed to dislike post:', exception);
      }
    } else {
      navigate("/login")
    }
  };

  const handleBookmark = async () => {
    if (authentication) {
      try {
        if (isBookmarked) {
          await unbookmarkPost(post.id);
        } else {
          await bookmarkPost(post.id);
        }
        setIsBookmarked(!isBookmarked);
      } catch (exception) {
        console.error('Failed to toggle bookmark:', exception);
      }
    } else {
      navigate("/login")
    }
  };

  const handleCommentSubmit = async (event) => {
    if (!comment && comment.trim() === '') {
      return;
    }

    await createComment({comment});
  }

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

      <hr />

      {/* Comments Section */}
      <Box sx={{ mt: 6 }}>
        <Typography variant="h5" gutterBottom>
          Comments ({post.comments?.length || 0})
        </Typography>

        {/* Comment form */}
        <form component="form" sx={{ mb: 4 }}>
          <TextField
            fullWidth
            multiline
            rows={4}
            variant="outlined"
            placeholder="Add your comment..."
            onChange={(e) => setComment(e.target.value)}
            sx={{ mb: 2 }}
          />
          <Button variant="contained" color="primary" onSubmit={handleCommentSubmit}>
            Post Comment
          </Button>
        </form>

        {/* Comments list */}
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
          {post.comments?.map(comment => (
            <Box key={comment.id} sx={{ p: 2, border: '1px solid #eee', borderRadius: 1 }}>
              <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>
                {comment.user}
              </Typography>
              <Typography variant="caption" color="text.secondary">
                {new Date(comment.date).toLocaleString()}
              </Typography>
              <Typography variant="body1" sx={{ mt: 1 }}>
                {comment.text}
              </Typography>
            </Box>
          )) || (
              <Typography variant="body1" color="text.secondary">
                No comments yet. Be the first to comment!
              </Typography>
            )}
        </Box>
      </Box>
    </Box>
  );
}