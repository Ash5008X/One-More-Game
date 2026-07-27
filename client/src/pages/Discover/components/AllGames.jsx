// Game thumbnail imports
import gamePixelKombat from '../../../assets/images/game_pixel_kombat.png';
import gameSnakeArena from '../../../assets/images/game_snake_arena.png';
import gameByteCity from '../../../assets/images/game_byte_city.png';
import gameCheckmate from '../../../assets/images/game_checkmate.png';
import gameMinesweeper from '../../../assets/images/game_minesweeper.png';
import gameSudoku from '../../../assets/images/game_sudoku.png';
import gameConnectFour from '../../../assets/images/game_connect_four.png';
import gameThumbChess from '../../../assets/images/game_thumb_chess.png';
import gameThumbArcade from '../../../assets/images/game_thumb_arcade.png';
import gameThumbPuzzle from '../../../assets/images/game_thumb_puzzle.png';
import gameThumbRacing from '../../../assets/images/game_thumb_racing.png';
import gameThumbTower from '../../../assets/images/game_thumb_tower.png';

const allGames = [
  { id: 1,  title: 'PIXEL KOMBAT',      category: 'FIGHTING',  difficulty: 'Hard',   img: gamePixelKombat },
  { id: 2,  title: 'SNAKE ARENA',       category: 'ARCADE',    difficulty: 'Medium', img: gameSnakeArena },
  { id: 3,  title: 'BYTE CITY 2049',    category: 'STRATEGY',  difficulty: 'Hard',   img: gameByteCity },
  { id: 4,  title: 'CHECKMATE',         category: 'BOARD',     difficulty: 'Medium', img: gameCheckmate },
  { id: 5,  title: 'MINESWEEPER PRO',   category: 'PUZZLE',    difficulty: 'Easy',   img: gameMinesweeper },
  { id: 6,  title: 'SUDOKU MASTER',     category: 'PUZZLE',    difficulty: 'Medium', img: gameSudoku },
  { id: 7,  title: 'CONNECT FOUR',      category: 'BOARD',     difficulty: 'Easy',   img: gameConnectFour },
  { id: 8,  title: 'NEON CHESS',        category: 'STRATEGY',  difficulty: 'Hard',   img: gameThumbChess },
  { id: 9,  title: 'GALACTIC BLASTER',  category: 'ARCADE',    difficulty: 'Medium', img: gameThumbArcade },
  { id: 10, title: 'BLOCK FUSION',      category: 'PUZZLE',    difficulty: 'Easy',   img: gameThumbPuzzle },
  { id: 11, title: 'VELOCITY RUSH',     category: 'SPORTS',    difficulty: 'Hard',   img: gameThumbRacing },
  { id: 12, title: 'TOWER SIEGE',       category: 'STRATEGY',  difficulty: 'Medium', img: gameThumbTower },
  { id: 13, title: 'ARENA CLASH',       category: 'FIGHTING',  difficulty: 'Hard',   img: gamePixelKombat },
  { id: 14, title: 'RETRO SNAKE',       category: 'ARCADE',    difficulty: 'Easy',   img: gameSnakeArena },
  { id: 15, title: 'CITY BUILDER',      category: 'STRATEGY',  difficulty: 'Medium', img: gameByteCity },
];

const DIFFICULTY_COLORS = {
  Easy: 'easy',
  Medium: 'medium',
  Hard: 'hard',
};

/* Split games into rows of 5 */
const chunkArray = (arr, size) => {
  const chunks = [];
  for (let i = 0; i < arr.length; i += size) {
    chunks.push(arr.slice(i, i + size));
  }
  return chunks;
};

const AllGames = () => {
  const rows = chunkArray(allGames, 5);

  return (
    <section className="discover-all" aria-label="All Games">
      <div className="discover-section__header">
        <h2 className="discover-section__title">ALL GAMES</h2>
        <span className="discover-section__count">{allGames.length} GAMES</span>
      </div>

      <div className="discover-all__rows">
        {rows.map((row, rowIdx) => (
          <div key={rowIdx} className="discover-all__row no-scrollbar">
            {row.map((game) => (
              <article key={game.id} className="discover-all__card brutal-card">
                <img
                  src={game.img}
                  alt={game.title}
                  className="discover-all__card-img"
                />
                <div className="discover-all__card-body">
                  <div className="discover-all__card-top">
                    <h3 className="discover-all__card-title">{game.title}</h3>
                    <div className="discover-all__card-meta">
                      <span className="discover-all__card-category">{game.category}</span>
                      <span className={`discover-all__card-difficulty discover-all__card-difficulty--${DIFFICULTY_COLORS[game.difficulty]}`}>
                        {game.difficulty.toUpperCase()}
                      </span>
                    </div>
                  </div>
                  <button className="discover-all__play-btn" aria-label={`Play ${game.title}`}>
                    <span className="material-symbols-outlined" style={{ fontVariationSettings: "'FILL' 1" }}>play_arrow</span>
                    PLAY
                  </button>
                </div>
              </article>
            ))}
          </div>
        ))}
      </div>
    </section>
  );
};

export default AllGames;
