using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL
{
    public static class MovieManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblMovie row = new tblMovie();
                    row.Id = dvd.tblMovies.Any() ? dvd.tblMovies.Max(s => s.Id) + 1 : 1;
                    row.Title = movie.Title;
                    row.Description = movie.Description;
                    row.Cost = movie.Cost;
                    row.RatingId = movie.RatingId;
                    row.FormatId = movie.FormatId;
                    row.DirectorId = movie.DirectorId;
                    row.Quantity = movie.Quantity;
                    row.ImagePath = movie.ImagePath;



                    movie.Id = row.Id;
                    dvd.tblMovies.Add(row);
                    results = dvd.SaveChanges();

                    if (rollback) dbContextTransaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Movie> Load(int? genreId = null)
        {
            try
            {
                List<Movie> rows = new List<Movie>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    var movies = (from m in dvd.tblMovies
                                  join r in dvd.tblRatings
                                  on m.RatingId equals r.Id
                                  join f in dvd.tblFormats
                                  on m.FormatId equals f.Id
                                  join d in dvd.tblDirectors
                                  on m.DirectorId equals d.Id
                                  join mg in dvd.tblMovieGenres
                                  on m.Id equals mg.MovieId
                                  join g in dvd.tblGenres
                                  on mg.GenreId equals g.Id
                                  where mg.GenreId == genreId || genreId == null
                                  select new
                                  {
                                      m.Id,
                                      m.Title,
                                      m.Description,
                                      m.Cost,
                                      RatingId = r.Id,
                                      RatingDesc = r.Description,
                                      FormatId = f.Id,
                                      FormatDesc = f.Description,
                                      DirectorId = d.Id,
                                      d.FirstName,
                                      d.LastName,
                                      m.Quantity,
                                      m.ImagePath,

                                  }).Distinct()
                                  .ToList();

                    movies.ForEach(movie => rows.Add(new Movie
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Description = movie.Description,
                        Cost = movie.Cost,
                        RatingId = movie.RatingId,  
                        RatingDesc= movie.RatingDesc,
                        FormatId = movie.FormatId,
                        FormatDesc = movie.FormatDesc,
                        Quantity = movie.Quantity,
                        ImagePath = movie.ImagePath,
                        DirectorId= movie.DirectorId,
                        DirectorName = movie.FirstName + " " + movie.LastName

                    }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        public static Movie LoadById(int id)
        {
            try
            {

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    var row = (from m in dvd.tblMovies
                                  join r in dvd.tblRatings
                                  on m.RatingId equals r.Id
                                  join f in dvd.tblFormats
                                  on m.FormatId equals f.Id
                                  join d in dvd.tblDirectors
                                  on m.DirectorId equals d.Id
                                  join mg in dvd.tblMovieGenres
                                  on m.Id equals mg.MovieId
                                  join g in dvd.tblGenres
                                  on mg.GenreId equals g.Id
                                  where m.Id == id
                                  select new
                                  {
                                      m.Id,
                                      m.Title,
                                      m.Description,
                                      m.Cost,
                                      RatingId = r.Id,
                                      RatingDesc = r.Description,
                                      FormatId = f.Id,
                                      FormatDesc = f.Description,
                                      DirectorId = d.Id,
                                      d.FirstName,
                                      d.LastName,
                                      m.Quantity,
                                      m.ImagePath,

                                  }).FirstOrDefault();
                    if (row != null)
                    {
                        return new Movie
                        {
                            Id = row.Id,
                            Title = row.Title,
                            Description = row.Description,
                            Cost = row.Cost,
                            RatingId = row.RatingId,
                            RatingDesc = row.RatingDesc,
                            FormatId = row.FormatId,
                            FormatDesc = row.FormatDesc,
                            Quantity = row.Quantity,
                            ImagePath = row.ImagePath,
                            DirectorId = row.DirectorId,
                            DirectorName = row.FirstName + " " + row.LastName,
                            Genres = GenreManager.Load(id)
                        };
                    }
                    else
                    {
                        throw new Exception(Message);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Movie> LoadByGenreId(int genreId)
        {
            return MovieManager.Load(genreId);
        }

        public static int Update(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblMovie row = dvd.tblMovies.Where(s => s.Id == movie.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.Title = movie.Title;
                        row.Description = movie.Description;
                        row.Cost = movie.Cost;
                        row.RatingId = movie.RatingId;
                        row.FormatId = movie.FormatId;
                        row.DirectorId = movie.DirectorId;
                        row.Quantity = movie.Quantity;
                        row.ImagePath = movie.ImagePath;
                        results = dvd.SaveChanges();

                        if (rollback) dbContextTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception(Message);
                    }

                }

                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblMovie row = dvd.tblMovies.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblMovies.Remove(row);
                        results = dvd.SaveChanges();

                        if (rollback) dbContextTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception(Message);
                    }

                }

                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
