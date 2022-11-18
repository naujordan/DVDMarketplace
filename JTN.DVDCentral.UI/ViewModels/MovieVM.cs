using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;

namespace JTN.DVDCentral.UI.ViewModels
{
    public class MovieVM
    {
        public Movie Movie { get; set; }
        public List<Genre> genres { get; set; }
        public List<Director> directors { get; set; }
        public List<Rating> ratings { get; set; }
        public List<Format> formats { get; set; }
        public IEnumerable<int> genreIds { get; set; }
        public IFormFile File { get; set; }

        public MovieVM(int id)
        {
            ratings = RatingManager.Load();
            formats = FormatManager.Load();
            directors = DirectorManager.Load();
            genres = GenreManager.Load();
            Movie = MovieManager.LoadById(id);
            genreIds = Movie.Genres.Select(g => g.Id);
        }
        public MovieVM()
        {
            ratings = RatingManager.Load();
            formats = FormatManager.Load();
            directors = DirectorManager.Load();
            genres = GenreManager.Load();
        }
    }
}
