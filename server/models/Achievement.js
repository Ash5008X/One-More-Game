import mongoose from 'mongoose';

const achievementSchema = new mongoose.Schema(
  {
    title: {
      type: String,
      required: [true, 'Achievement title is required'],
      trim: true,
      unique: true,
    },

    description: {
      type: String,
      default: '',
    },

    icon: {
      type: String,
      default: '',
    },

    xp: {
      type: Number,
      default: 0,
    },

    condition: {
      type: String,
      default: '',
    },
  },
  { timestamps: true }
);

const Achievement = mongoose.model('Achievement', achievementSchema);

export default Achievement;
