import DashboardNavbar from '../../components/DashboardNavbar/DashboardNavbar';
import SearchBar from './components/SearchBar';
import FeaturedHero from './components/FeaturedHero';
import TrendingGames from './components/TrendingGames';
import BrowseCategories from './components/BrowseCategories';
import AllGames from './components/AllGames';

import './Discover.css';

const Discover = () => {
  return (
    <div className="discover-page">
      <DashboardNavbar />

      <main className="discover-main">
        {/* Search Bar */}
        <SearchBar />

        {/* Featured Games Hero Carousel */}
        <FeaturedHero />

        {/* Trending Games */}
        <TrendingGames />

        {/* Browse by Category */}
        <BrowseCategories />

        {/* All Games */}
        <AllGames />
      </main>
    </div>
  );
};

export default Discover;
