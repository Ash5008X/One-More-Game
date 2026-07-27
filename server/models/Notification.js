import mongoose from 'mongoose';

const notificationSchema = new mongoose.Schema(
  {
    receiver: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'User',
      required: [true, 'Receiver is required'],
    },

    type: {
      type: String,
      enum: [
        'friend_request',
        'friend_accepted',
        'achievement_unlocked',
        'game_invite',
        'match_result',
        'system',
      ],
      required: [true, 'Notification type is required'],
    },

    message: {
      type: String,
      required: [true, 'Notification message is required'],
    },

    isRead: {
      type: Boolean,
      default: false,
    },

    createdAt: {
      type: Date,
      default: Date.now,
    },
  },
  { timestamps: true }
);

// Index for fetching a user's unread notifications efficiently
notificationSchema.index({ receiver: 1, isRead: 1 });

const Notification = mongoose.model('Notification', notificationSchema);

export default Notification;
