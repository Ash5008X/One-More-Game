import mongoose from 'mongoose';

const leaderboardSchema = new mongoose.Schema(
  {
    user: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'User',
      required: [true, 'User reference is required'],
    },

    game: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'Game',
      required: [true, 'Game reference is required'],
    },

    rating: {
      type: Number,
      default: 0,
    },

    wins: {
      type: Number,
      default: 0,
    },

    losses: {
      type: Number,
      default: 0,
    },

    rank: {
      type: Number,
      default: null,
    },
  },
  { timestamps: true }
);

// Each user can have only one leaderboard entry per game
leaderboardSchema.index({ user: 1, game: 1 }, { unique: true });

// Index for sorting by rating within a game
leaderboardSchema.index({ game: 1, rating: -1 });

const Leaderboard = mongoose.model('Leaderboard', leaderboardSchema);

export default Leaderboard;
