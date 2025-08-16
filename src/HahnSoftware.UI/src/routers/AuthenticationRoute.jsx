import { Navigate } from 'react-router-dom';
import { useAuthentication } from '../contexts/AuthenticationContext';

export default function AuthenticationRoute({ children }) {
  const { authentication } = useAuthentication();
  return authentication === false ? children : <Navigate to="/dashboard" />;
}