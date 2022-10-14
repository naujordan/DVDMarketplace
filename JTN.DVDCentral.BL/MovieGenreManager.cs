using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL
{
    public static class MovieGenreManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(int id, int movieID, int genreID, bool rollback = false)
        {
            try
            {
                Movie movie = new Movie();
                Genre genre = new Genre();

                movie.Id = movieID;
                genre.Id = genreID;

                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblMovieGenre row = new tblMovieGenre();
                    row.Id = dvd.tblMovieGenres.Any() ? dvd.tblMovieGenres.Max(s => s.Id) + 1 : 1;
                    row.MovieId = movie.Id;
                    row.GenreId = genre.Id;

                    id = row.Id;
                    dvd.tblMovieGenres.Add(row);
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

        public static int Update(int id, int movieID, int genreID, bool rollback = false)
        {
            try
            {
                Movie movie = new Movie();
                Genre genre = new Genre();

                movie.Id = movieID;
                genre.Id = genreID;

                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblMovieGenre row = dvd.tblMovieGenres.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        row.MovieId = movie.Id;
                        row.GenreId = genre.Id;
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

                    tblMovieGenre row = dvd.tblMovieGenres.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblMovieGenres.Remove(row);
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
