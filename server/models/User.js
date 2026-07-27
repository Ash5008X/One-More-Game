import mongoose from 'mongoose';
import bcrypt from 'bcryptjs';

const userSchema = new mongoose.Schema(
  {
    fullName: {
      type: String,
      required: [true, 'Full name is required'],
      trim: true,
    },

    username: {
      type: String,
      unique: true,
      sparse: true,
      default: null,
      trim: true,
      lowercase: true,
    },

    email: {
      type: String,
      required: [true, 'Email is required'],
      unique: true,
      trim: true,
      lowercase: true,
    },

    password: {
      type: String,
      required: [true, 'Password is required'],
      minlength: [6, 'Password must be at least 6 characters'],
      select: false,
    },

    avatar: {
      type: String,
      default: '/assets/images/default-avatar.png',
    },

    bio: {
      type: String,
      default: '',
    },

    settings: {
      showStatus: {
        type: String,
        enum: ['online', 'away', 'dnd', 'invisible'],
        default: 'online',
      },
      theme: {
        type: String,
        default: 'dark',
      },
      emailNotifications: {
        type: Boolean,
        default: true,
      },
    },

    lastSeen: {
      type: Date,
    },
  },
  { timestamps: true }
);

// Hash password before saving
userSchema.pre('save', async function () {
  if (!this.isModified('password')) return;
  const salt = await bcrypt.genSalt(12);
  this.password = await bcrypt.hash(this.password, salt);
});

// Compare a plain-text password against the stored hash
userSchema.methods.matchPassword = async function (plainPassword) {
  return bcrypt.compare(plainPassword, this.password);
};

const User = mongoose.model('User', userSchema);

export default User;
