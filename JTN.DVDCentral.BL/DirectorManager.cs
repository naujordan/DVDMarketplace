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
    public static class DirectorManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(Director director, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblDirector row = new tblDirector();
                    row.Id = dvd.tblDirectors.Any() ? dvd.tblDirectors.Max(s => s.Id) + 1 : 1;
                    row.FirstName = director.FirstName;
                    row.LastName = director.LastName;

                    director.Id = row.Id;
                    dvd.tblDirectors.Add(row);
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

        public static List<Director> Load()
        {
            try
            {
                List<Director> rows = new List<Director>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblDirectors
                        .ToList()
                        .ForEach(s => rows.Add(new Director
                        {
                            Id = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                        }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Director LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblDirector row = dvd.tblDirectors.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new Director
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
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

        public static int Update(Director director, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblDirector row = dvd.tblDirectors.Where(s => s.Id == director.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.FirstName = director.FirstName;
                        row.LastName = director.LastName;
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

                    tblDirector row = dvd.tblDirectors.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblDirectors.Remove(row);
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
