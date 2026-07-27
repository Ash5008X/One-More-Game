import { Link, NavLink, useNavigate } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import './DashboardNavbar.css';

const NAV_ITEMS = [
  { path: '/dashboard', label: 'DASHBOARD' },
  { path: '/discover', label: 'DISCOVER' },
  { path: '/library', label: 'LIBRARY' },
  { path: '/leaderboards', label: 'LEADERBOARDS' },
  { path: '/achievements', label: 'ACHIEVEMENTS' },
];

const DashboardNavbar = ({ user }) => {
  const navigate = useNavigate();
  const { logout } = useAuth();

  const handleLogout = () => {
    logout();
    navigate('/auth');
  };

  return (
    <header className="dash-nav">
      {/* Logo Area */}
      <div className="dash-nav__brand">
        <Link to="/" className="dash-nav__wordmark">ONE MORE GAME</Link>
        <div className="dash-nav__badge">JUST ONE MORE....</div>
      </div>

      {/* Navigation Links */}
      <nav className="dash-nav__links">
        {NAV_ITEMS.map((item) => (
          <NavLink
            key={item.path}
            to={item.path}
            className={({ isActive }) =>
              `dash-nav__link${isActive ? ' dash-nav__link--active' : ''}`
            }
          >
            {item.label}
          </NavLink>
        ))}
      </nav>

      {/* Trailing Actions */}
      <div className="dash-nav__actions">
        <button className="dash-nav__icon-btn dash-nav__icon-btn--notify" aria-label="Notifications">
          <span className="material-symbols-outlined">notifications</span>
          <span className="dash-nav__notify-dot"></span>
        </button>
        <button className="dash-nav__icon-btn" aria-label="Logout" onClick={handleLogout} title="Logout">
          <span className="material-symbols-outlined">logout</span>
        </button>
        <div className="dash-nav__avatar" onClick={() => navigate('/dashboard')}>
          <span className="material-symbols-outlined" style={{ fontSize: '24px', color: '#00ff0d' }}>person</span>
        </div>
      </div>
    </header>
  );
};

export default DashboardNavbar;
