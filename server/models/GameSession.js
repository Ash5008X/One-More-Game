import mongoose from 'mongoose';

const playerSchema = new mongoose.Schema(
  {
    user: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'User',
      required: true,
    },
    score: {
      type: Number,
      default: 0,
    },
    rank: {
      type: Number,
      default: null,
    },
    result: {
      type: String,
      enum: ['win', 'loss', 'draw', null],
      default: null,
    },
  },
  { _id: false }
);

const gameSessionSchema = new mongoose.Schema(
  {
    game: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'Game',
      required: [true, 'Game reference is required'],
    },

    players: {
      type: [playerSchema],
      default: [],
    },

    mode: {
      type: String,
      enum: ['solo', 'multiplayer'],
      default: 'solo',
    },

    duration: {
      type: Number, // seconds
      default: 0,
    },

    winner: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'User',
      default: null,
    },

    matchStatus: {
      type: String,
      enum: ['ongoing', 'finished'],
      default: 'ongoing',
    },

    startedAt: {
      type: Date,
      default: Date.now,
    },

    endedAt: {
      type: Date,
      default: null,
    },
  },
  { timestamps: true }
);

// Index for querying sessions by game and status
gameSessionSchema.index({ game: 1, matchStatus: 1 });

const GameSession = mongoose.model('GameSession', gameSessionSchema);

export default GameSession;
