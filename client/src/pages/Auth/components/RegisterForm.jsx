import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../hooks/useAuth';
import Input from './Input';
import PasswordInput from './PasswordInput';
import AuthToggle from './AuthToggle';

const UserIcon = () => (
  <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2" />
    <circle cx="12" cy="7" r="4" />
  </svg>
);

const MailIcon = () => (
  <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <rect x="2" y="4" width="20" height="16" rx="2" />
    <path d="M22 4 12 13 2 4" />
  </svg>
);

const RegisterForm = ({ onToggle }) => {
  const navigate = useNavigate();
  const { register, error: authError } = useAuth();
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState('');

  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    password: '',
    confirmPassword: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMsg('');
    if (formData.password !== formData.confirmPassword) {
      setErrorMsg('Passwords do not match!');
      return;
    }

    setLoading(true);
    try {
      await register(formData.fullName, formData.email, formData.password);
      navigate('/dashboard');
    } catch (err) {
      setErrorMsg(err.message || 'Registration failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="auth-form-panel">
      <div className="auth-form-content">
        <h2 className="auth-form-title">CREATE ACCOUNT</h2>
        <p className="auth-form-subtitle">Start your journey. Become a legend.</p>

        <form onSubmit={handleSubmit} className="auth-form" autoComplete="on">
          {errorMsg && <div className="auth-error-msg" style={{ color: '#ff4b4b', fontSize: '0.875rem', marginBottom: '1rem', textAlign: 'center' }}>{errorMsg}</div>}
          {authError && !errorMsg && <div className="auth-error-msg" style={{ color: '#ff4b4b', fontSize: '0.875rem', marginBottom: '1rem', textAlign: 'center' }}>{authError}</div>}

          <Input
            type="text"
            placeholder="Full Name"
            name="fullName"
            value={formData.fullName}
            onChange={handleChange}
            required
            autoComplete="name"
            icon={<UserIcon />}
          />

          <Input
            type="email"
            placeholder="Email Address"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
            autoComplete="email"
            icon={<MailIcon />}
          />

          <PasswordInput
            placeholder="Password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
            autoComplete="new-password"
          />

          <PasswordInput
            placeholder="Confirm Password"
            name="confirmPassword"
            value={formData.confirmPassword}
            onChange={handleChange}
            required
            autoComplete="new-password"
          />

          <button type="submit" className="auth-submit-btn" id="register-btn" disabled={loading}>
            <span>{loading ? 'CREATING ACCOUNT...' : 'CREATE ACCOUNT'}</span>
            <span className="auth-btn-arrow">→</span>
          </button>
        </form>

        <AuthToggle isLogin={false} onToggle={onToggle} />
      </div>
    </div>
  );
};

export default RegisterForm;
