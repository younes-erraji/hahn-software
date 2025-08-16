import { Navigate } from 'react-router-dom';
import { useAuthentication } from '../contexts/AuthenticationContext';

export default function PrivateRoute({ children }) {
  const { authentication } = useAuthentication();
  return authentication ? children : <Navigate to="/login" />;
}