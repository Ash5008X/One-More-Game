const SearchBar = () => {
  return (
    <div className="discover-search">
      <div className="discover-search__input-wrap">
        <span className="material-symbols-outlined discover-search__icon">search</span>
        <input
          type="text"
          className="discover-search__input"
          placeholder="Search games..."
          aria-label="Search games"
          readOnly
        />
      </div>
    </div>
  );
};

export default SearchBar;
