import api from './axiosConfig';

export const getPosts = async (pageSize = 10, pageNumber = 1, search = '') => {
  return api.post('/posts', 
    { search },
    { params: { PageSize: pageSize, PageNumber: pageNumber } }
  );
};

export const getBookmarks = async (pageSize = 10, pageNumber = 1, search = '') => {
  return api.post('/posts/bookmarks', 
    { search },
    { params: { PageSize: pageSize, PageNumber: pageNumber } }
  );
};

export const createPost = async (postData) => {
  return api.post('/posts/create', postData);
};

export const getPostBySlug = async (slug) => {
  return api.get(`/posts/${slug}`);
};

export const updatePost = async (postData) => {
  return api.put('/posts', postData);
};

export const deletePost = async (postId) => {
  return api.delete('/posts', { params: { postId } });
};

export const likePost = async (postId) => {
  return api.get('/posts/reaction/like', { params: { postId } });
};

export const dislikePost = async (postId) => {
  return api.get('/posts/reaction/dislike', { params: { postId } });
};

export const bookmarkPost = async (postId) => {
  return api.get('/posts/bookmark', { params: { postId } });
};

export const unbookmarkPost = async (postId) => {
  return api.get('/posts/unbookmark', { params: { postId } });
};

export const removeReaction = async (postId) => {
  return api.delete('/posts/reaction', { params: { postId } });
};
