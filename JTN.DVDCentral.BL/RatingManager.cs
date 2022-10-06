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
    public static class RatingManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(Rating rating, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblRating row = new tblRating();
                    row.Id = dvd.tblRatings.Any() ? dvd.tblRatings.Max(s => s.Id) + 1 : 1;
                    row.Description = rating.Description;

                    rating.Id = row.Id;
                    dvd.tblRatings.Add(row);
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

        public static List<Rating> Load()
        {
            try
            {
                List<Rating> rows = new List<Rating>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblRatings
                        .ToList()
                        .ForEach(s => rows.Add(new Rating
                        {
                            Id = s.Id,
                            Description = s.Description,
                        }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Rating LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblRating row = dvd.tblRatings.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new Rating
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

        public static int Update(Rating rating, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblRating row = dvd.tblRatings.Where(s => s.Id == rating.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.Description = rating.Description;
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

                    tblRating row = dvd.tblRatings.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblRatings.Remove(row);
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
