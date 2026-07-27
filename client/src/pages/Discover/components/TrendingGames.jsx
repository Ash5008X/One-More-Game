// Use existing game images as thumbnails for trending section
import gamePixelKombat from '../../../assets/images/game_pixel_kombat.png';
import gameSnakeArena from '../../../assets/images/game_snake_arena.png';
import gameByteCity from '../../../assets/images/game_byte_city.png';
import gameCheckmate from '../../../assets/images/game_checkmate.png';
import gameMinesweeper from '../../../assets/images/game_minesweeper.png';
import gameSudoku from '../../../assets/images/game_sudoku.png';
import gameConnectFour from '../../../assets/images/game_connect_four.png';

const trendingGames = [
  { id: 1, title: 'PIXEL KOMBAT', category: 'FIGHTING', img: gamePixelKombat },
  { id: 2, title: 'SNAKE ARENA', category: 'ARCADE', img: gameSnakeArena },
  { id: 3, title: 'BYTE CITY 2049', category: 'STRATEGY', img: gameByteCity },
  { id: 4, title: 'CHECKMATE', category: 'BOARD', img: gameCheckmate },
  { id: 5, title: 'MINESWEEPER PRO', category: 'PUZZLE', img: gameMinesweeper },
  { id: 6, title: 'SUDOKU MASTER', category: 'PUZZLE', img: gameSudoku },
  { id: 7, title: 'CONNECT FOUR', category: 'BOARD', img: gameConnectFour },
];

const TrendingGames = () => {
  return (
    <section className="discover-trending" aria-label="Trending Games">
      <div className="discover-section__header">
        <h2 className="discover-section__title">TRENDING GAMES</h2>
        <span className="discover-section__badge">
          <span className="material-symbols-outlined" style={{ fontSize: '14px', fontVariationSettings: "'FILL' 1" }}>trending_up</span>
          HOT
        </span>
      </div>

      <div className="discover-trending__track-wrap">
        <div className="discover-trending__track no-scrollbar">
          {trendingGames.map((game) => (
            <article key={game.id} className="discover-trending__card brutal-card">
              <img
                src={game.img}
                alt={game.title}
                className="discover-trending__card-img"
              />
              <div className="discover-trending__card-body">
                <h3 className="discover-trending__card-title">{game.title}</h3>
                <span className="discover-trending__card-category">{game.category}</span>
                <button className="discover-trending__play-btn" aria-label={`Play ${game.title}`}>
                  <span className="material-symbols-outlined" style={{ fontVariationSettings: "'FILL' 1" }}>play_arrow</span>
                </button>
              </div>
            </article>
          ))}
        </div>
      </div>
    </section>
  );
};

export default TrendingGames;
