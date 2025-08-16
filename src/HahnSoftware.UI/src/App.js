import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthenticationProvider } from './contexts/AuthenticationContext';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import Dashboard from './pages/Dashboard';
import PostPage from './pages/PostPage';
import Login from './components/Authentication/Login';
import Register from './components/Authentication/Register';
import PostForm from './components/Blog/PostForm';
import PrivateRoute from './routers/PrivateRoute';
import AuthenticationRoute from './routers/AuthenticationRoute';
// import ForgotPassword from './components/Auth/ForgotPassword';
// import ResetPassword from './components/Auth/ResetPassword';

function App() {
  return (
    <Router>
      <AuthenticationProvider>
        <Navbar />
        <div className="container">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/login" element={
              <AuthenticationRoute>
                <Login />
              </AuthenticationRoute>} />
            <Route path="/register" element={<AuthenticationRoute>
              <Register />
            </AuthenticationRoute>} />
            <Route path="/post/:slug" element={<PostPage />} />
            <Route
              path="/dashboard"
              element={
                <PrivateRoute>
                  <Dashboard />
                </PrivateRoute>
              }
            />
            <Route
              path="/create-post"
              element={
                <PrivateRoute>
                  <PostForm />
                </PrivateRoute>
              }
            />
            <Route
              path="/edit-post/:id"
              element={
                <PrivateRoute>
                  <PostForm />
                </PrivateRoute>
              }
            />
          </Routes>
        </div>
      </AuthenticationProvider>
    </Router>
  );
}

export default App;