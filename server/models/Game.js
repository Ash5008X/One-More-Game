import mongoose from 'mongoose';

const gameSchema = new mongoose.Schema(
  {
    slug: {
      type: String,
      required: [true, 'Game slug is required'],
      unique: true,
      trim: true,
      lowercase: true,
    },

    title: {
      type: String,
      required: [true, 'Game title is required'],
      trim: true,
    },

    description: {
      type: String,
      default: '',
    },

    thumbnail: {
      type: String,
      default: '',
    },

    category: {
      type: String,
      required: [true, 'Game category is required'],
      trim: true,
    },

    multiplayer: {
      type: Boolean,
      default: false,
    },

    unityBuild: {
      loaderUrl: { type: String, default: '' },
      dataUrl: { type: String, default: '' },
      frameworkUrl: { type: String, default: '' },
      codeUrl: { type: String, default: '' },
    },

    difficulty: {
      type: String,
      enum: ['easy', 'medium', 'hard'],
      default: 'medium',
    },

    active: {
      type: Boolean,
      default: true,
    },
  },
  { timestamps: true }
);

const Game = mongoose.model('Game', gameSchema);

export default Game;
