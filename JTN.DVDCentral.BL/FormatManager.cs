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
    public static class FormatManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(Format format, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblFormat row = new tblFormat();
                    row.Id = dvd.tblFormats.Any() ? dvd.tblFormats.Max(s => s.Id) + 1 : 1;
                    row.Description = format.Description;

                    format.Id = row.Id;
                    dvd.tblFormats.Add(row);
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

        public static List<Format> Load()
        {
            try
            {
                List<Format> rows = new List<Format>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblFormats
                        .ToList()
                        .ForEach(s => rows.Add(new Format
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

        public static Format LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblFormat row = dvd.tblFormats.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new Format
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

        public static int Update(Format format, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblFormat row = dvd.tblFormats.Where(s => s.Id == format.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.Description = format.Description;
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

                    tblFormat row = dvd.tblFormats.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblFormats.Remove(row);
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
