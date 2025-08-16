import api from './axiosConfig';

export const registerUser = async (userData) => {
  return api.post('/authentication/register', userData);
};

export const loginUser = async (credentials) => {
  return api.post('/authentication/login', credentials);
};

export const refreshToken = async (refreshToken) => {
  return api.post('/authentication/refresh-token', { refreshToken });
};

export const logoutUser = async (refreshToken) => {
  return api.post('/authentication/revoke-token', { refreshToken });
};
