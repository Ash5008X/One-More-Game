const categories = [
  'All',
  'Strategy',
  'Board',
  'Sports',
  'Arcade',
  'Puzzle',
  'Multiplayer',
  'Single Player',
];

const BrowseCategories = () => {
  return (
    <section className="discover-browse" aria-label="Browse by Category">
      <div className="discover-section__header">
        <h2 className="discover-section__title">BROWSE GAMES</h2>
      </div>

      <div className="discover-browse__chips-wrap no-scrollbar">
        {categories.map((cat, idx) => (
          <button
            key={cat}
            className={`discover-browse__chip${idx === 0 ? ' discover-browse__chip--active' : ''}`}
          >
            {cat.toUpperCase()}
          </button>
        ))}
      </div>
    </section>
  );
};

export default BrowseCategories;
