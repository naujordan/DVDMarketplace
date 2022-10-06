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

        public static List<Movie> Load()
        {
            try
            {
                List<Movie> rows = new List<Movie>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblMovies
                        .ToList()
                        .ForEach(s => rows.Add(new Movie
                        {
                            Id = s.Id,
                            Title = s.Title,
                            Description = s.Description,
                            Cost = s.Cost,
                            RatingId = s.RatingId,
                            FormatId = s.FormatId,
                            DirectorId = s.DirectorId,
                            Quantity = s.Quantity,
                            ImagePath = s.ImagePath,
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
                    tblMovie row = dvd.tblMovies.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new Movie
                        {
                            Id = row.Id,
                            Title = row.Title,
                            Description = row.Description,
                            Cost = row.Cost,
                            RatingId = row.RatingId,
                            FormatId = row.FormatId,
                            DirectorId = row.DirectorId,
                            Quantity = row.Quantity,
                            ImagePath = row.ImagePath,
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
