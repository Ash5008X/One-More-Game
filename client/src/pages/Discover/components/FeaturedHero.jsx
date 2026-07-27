import { useState, useEffect, useCallback } from 'react';

// Hero image imports
import heroImg1 from '../../../assets/images/discover_hero_1.png';
import heroImg2 from '../../../assets/images/discover_hero_2.png';
import heroImg3 from '../../../assets/images/discover_hero_3.png';
import heroImg4 from '../../../assets/images/discover_hero_4.png';
import heroImg5 from '../../../assets/images/discover_hero_5.png';

const SLIDE_DURATION = 10000; // 10 seconds per slide

const slides = [
  {
    id: 1,
    title: 'NEON OVERDRIVE: 2099',
    description: 'Race through the neon-soaked streets of a dystopian future. High-octane multiplayer action awaits.',
    image: heroImg1,
  },
  {
    id: 2,
    title: 'DRAGON\'S KEEP',
    description: 'Forge your legend in a world of dark fantasy. Epic dragon battles and ancient mysteries.',
    image: heroImg2,
  },
  {
    id: 3,
    title: 'GALACTIC FRONTIER',
    description: 'Explore uncharted galaxies. Build your fleet and conquer the cosmos in this space odyssey.',
    image: heroImg3,
  },
  {
    id: 4,
    title: 'VELOCITY RUSH',
    description: 'Push the limits of speed through rain-soaked cyberpunk circuits. Feel every drift.',
    image: heroImg4,
  },
  {
    id: 5,
    title: 'WASTELAND ECHO',
    description: 'Survive the aftermath. Scavenge, build, and fight in a post-apocalyptic open world.',
    image: heroImg5,
  },
];

const FeaturedHero = () => {
  const [activeIndex, setActiveIndex] = useState(0);
  const [prevIndex, setPrevIndex] = useState(null);

  const goToSlide = useCallback((nextIdx) => {
    if (nextIdx === activeIndex) return;

    setPrevIndex(activeIndex);
    setActiveIndex(nextIdx);

    const timer = setTimeout(() => {
      setPrevIndex(null);
    }, 800);
    return () => clearTimeout(timer);
  }, [activeIndex]);

  const advanceSlide = useCallback(() => {
    const nextIdx = (activeIndex + 1) % slides.length;
    goToSlide(nextIdx);
  }, [activeIndex, goToSlide]);

  // Auto-advance timer (triggers after 10 seconds)
  useEffect(() => {
    const timer = setTimeout(advanceSlide, SLIDE_DURATION);
    return () => clearTimeout(timer);
  }, [activeIndex, advanceSlide]);

  const handleDotClick = (idx) => {
    if (idx === activeIndex) return;
    goToSlide(idx);
  };

  return (
    <section className="discover-hero brutal-card" aria-label="Featured Games">
      {/* Previous slide (fading out) */}
      {prevIndex !== null && (
        <div
          className="discover-hero__bg discover-hero__bg--exiting"
          style={{ backgroundImage: `url(${slides[prevIndex].image})` }}
          aria-hidden="true"
        />
      )}

      {/* Active slide (fading in with Ken Burns) */}
      <div
        key={activeIndex}
        className="discover-hero__bg discover-hero__bg--active"
        style={{ backgroundImage: `url(${slides[activeIndex].image})` }}
        aria-hidden="true"
      />

      {/* Gradient overlay for text readability */}
      <div className="discover-hero__overlay" aria-hidden="true" />

      {/* Content overlaid on artwork — movie poster style */}
      <div
        key={`content-${activeIndex}`}
        className="discover-hero__content"
      >
        <div className="discover-hero__info">
          <h1 className="discover-hero__title">{slides[activeIndex].title}</h1>
          <p className="discover-hero__desc">{slides[activeIndex].description}</p>
          <div className="discover-hero__actions">
            <button className="discover-hero__play-btn">
              <span className="material-symbols-outlined" style={{ fontVariationSettings: "'FILL' 1" }}>play_arrow</span>
              PLAY NOW
            </button>
            <button className="discover-hero__details-btn">
              VIEW DETAILS
            </button>
          </div>
        </div>
      </div>

      {/* Slide indicators — bottom right */}
      <div className="discover-hero__indicators">
        {slides.map((slide, idx) => (
          <button
            key={slide.id}
            className={`discover-hero__dot${idx === activeIndex ? ' discover-hero__dot--active' : ''}`}
            onClick={() => handleDotClick(idx)}
            aria-label={`Go to slide ${idx + 1}`}
          >
            {idx === activeIndex && (
              <span key={activeIndex} className="discover-hero__dot-fill" />
            )}
          </button>
        ))}
      </div>
    </section>
  );
};

export default FeaturedHero;
