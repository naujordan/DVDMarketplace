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
    public static class GenreManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(Genre genre, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblGenre row = new tblGenre();
                    row.Id = dvd.tblGenres.Any() ? dvd.tblGenres.Max(s => s.Id) + 1 : 1;
                    row.Description = genre.Description;

                    genre.Id = row.Id;
                    dvd.tblGenres.Add(row);
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

        public static List<Genre> Load(int? movieId = null)
        {
            try
            {
                List<Genre> rows = new List<Genre>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    (from g in dvd.tblGenres
                     join mg in dvd.tblMovieGenres
                     on g.Id equals mg.GenreId
                     where mg.MovieId == movieId || movieId == null
                     select new
                     {
                         g.Id,
                         g.Description
                     }).Distinct().ToList().ForEach(g => rows.Add(new Genre
                     {
                         Id = g.Id,
                         Description = g.Description
                     }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<Genre> Load()
        {
            try
            {
                List<Genre> rows = new List<Genre>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    (from g in dvd.tblGenres
                     select new
                     {
                         g.Id,
                         g.Description
                     }).Distinct().ToList().ForEach(g => rows.Add(new Genre
                     {
                         Id = g.Id,
                         Description = g.Description
                     }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Genre LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblGenre row =dvd.tblGenres.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new Genre
                        {
                            Id = row.Id,
                            Description = row.Description,
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

        public static int Update(Genre genre, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblGenre row = dvd.tblGenres.Where(s => s.Id == genre.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.Description = genre.Description;
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

                    tblGenre row = dvd.tblGenres.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblGenres.Remove(row);
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
