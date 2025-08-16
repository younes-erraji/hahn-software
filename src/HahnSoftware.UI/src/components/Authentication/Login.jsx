import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuthentication } from '../../contexts/AuthenticationContext';
import { TextField, Button, Container, Typography, Box } from '@mui/material';

export default function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);
  const { login } = useAuthentication();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await login(email, password, rememberMe);
      if (response) {
        navigate('/');
      } else  {
        throw new Error('Login failed. Please check your credentials.');
      }
    } catch (exception) {
      console.error(exception);
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ mt: 8 }}>
        <Typography variant="h4" gutterBottom>
          Login
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Email"
            type="email"
            fullWidth
            margin="normal"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <TextField
            label="Password"
            type="password"
            fullWidth
            margin="normal"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <Box sx={{ display: 'flex', alignItems: 'center', mt: 1 }}>
            <input
              type="checkbox"
              id="rememberMe"
              checked={rememberMe}
              onChange={(e) => setRememberMe(e.target.checked)}
            />
            <label htmlFor="rememberMe" style={{ marginLeft: '8px' }}>
              Remember me
            </label>
          </Box>
          <Button type="submit" variant="contained" sx={{ mt: 2 }}>
            Login
          </Button>
        </form>
        <Typography sx={{ mt: 2 }}>
          Don't have an account? <Link to="/register">Register</Link>
        </Typography>
        <Typography sx={{ mt: 1 }}>
          <Link to="/forgot-password">Forgot password?</Link>
        </Typography>
      </Box>
    </Container>
  );
}