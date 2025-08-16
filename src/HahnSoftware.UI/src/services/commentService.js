import api from './axiosConfig';

export const getComments = async (postId) => {
  return api.get('/comments/comments', { params: { postId } });
};

export const createComment = async (commentData) => {
  return api.post('/comments/comments', commentData);
};

export const updateComment = async (commentData) => {
  return api.put('/comments/comments', commentData);
};

export const deleteComment = async (commentId) => {
  return api.delete('/comments/comments', { params: { commentId } });
};

