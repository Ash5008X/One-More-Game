import mongoose from 'mongoose';

const userAchievementSchema = new mongoose.Schema(
  {
    user: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'User',
      required: [true, 'User reference is required'],
    },

    achievement: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'Achievement',
      required: [true, 'Achievement reference is required'],
    },

    unlockedAt: {
      type: Date,
      default: Date.now,
    },
  },
  { timestamps: true }
);

// Each achievement can only be unlocked once per user
userAchievementSchema.index({ user: 1, achievement: 1 }, { unique: true });

const UserAchievement = mongoose.model('UserAchievement', userAchievementSchema);

export default UserAchievement;
