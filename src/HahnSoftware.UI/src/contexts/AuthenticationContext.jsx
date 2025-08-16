import { createContext, useContext, useEffect, useState } from 'react';
import { refreshToken, logoutUser } from '../services/authenticationService';
import { loginUser } from '../services/authenticationService';

const AuthenticationContext = createContext();

export function useAuthentication() {
  return useContext(AuthenticationContext);
}

export function AuthenticationProvider({ children }) {
  const [authentication, setAuthentication] = useState(false);
  const [authenticationUser, setAuthenticationUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem('token');
    const refresh = localStorage.getItem('refreshToken');
    if (refresh) {
      refreshToken(refresh)
      .then(response => {
        localStorage.setItem('token', response.data.payload.token.accessToken);
        localStorage.setItem('refreshToken', response.data.payload.token.refreshToken);
        setAuthenticationUser(response.data.payload);
        setAuthentication(true);
      })
      .catch(() => {
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        console.log('some error occured');
      });
    }
    
    setLoading(false);
  }, []);

  const login = async (email, password, rememberMe) => {
    const response = await loginUser({ email, password, rememberMe });
    if (response.status) {
      localStorage.setItem('token', response.data.payload.token.accessToken);
      localStorage.setItem('refreshToken', response.data.payload.token.refreshToken);
      setAuthenticationUser(response.data.payload);
      setAuthentication(true);
      return true;
    }

    return false
  };

  const logout = async () => {
    const refresh = localStorage.getItem('refreshToken');
    if (refresh) {
      await logoutUser(refresh);
    }
    
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    setAuthenticationUser(null);
    setAuthentication(false);
  };

  return (
    <AuthenticationContext.Provider value={{
      authenticationUser,
      login,
      logout,
      authentication
    }}>
      {!loading && children}
    </AuthenticationContext.Provider>
  );
}