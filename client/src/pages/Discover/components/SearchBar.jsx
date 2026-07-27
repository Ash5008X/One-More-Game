import { useState } from 'react';

const SearchBar = () => {
  const [searchTerm, setSearchTerm] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
  };

  return (
    <div className="discover-search">
      <form className="discover-search__form" onSubmit={handleSubmit}>
        <div className="discover-search__input-wrap">
          <span className="material-symbols-outlined discover-search__icon">search</span>
          <input
            type="text"
            className="discover-search__input"
            placeholder="Search games..."
            aria-label="Search games"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <button type="submit" className="discover-search__btn" aria-label="Search">
          <span className="material-symbols-outlined">search</span>
          <span className="discover-search__btn-text">SEARCH</span>
        </button>
      </form>
    </div>
  );
};

export default SearchBar;
