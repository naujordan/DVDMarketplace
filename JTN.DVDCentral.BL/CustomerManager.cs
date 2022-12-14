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
    public static class CustomerManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblCustomer row = new tblCustomer();
                    row.Id = dvd.tblCustomers.Any() ? dvd.tblCustomers.Max(s => s.Id) + 1 : 1;
                    row.FirstName = customer.FirstName;
                    row.LastName = customer.LastName;
                    row.Address = customer.Address;
                    row.City = customer.City;
                    row.State = customer.State;
                    row.Zip = customer.Zip;
                    row.Phone = customer.Phone;
                    row.UserId = customer.UserId;

                    customer.Id = row.Id;
                    dvd.tblCustomers.Add(row);
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

        public static List<Customer> Load()
        {
            try
            {
                List<Customer> rows = new List<Customer>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblCustomers
                        .ToList()
                        .ForEach(s => rows.Add(new Customer
                        {
                            Id = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            Address = s.Address,
                            City = s.City,
                            State = s.State,   
                            Zip = s.Zip,
                            Phone = s.Phone,
                            UserId = s.UserId
                        }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Customer LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblCustomer row = dvd.tblCustomers.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new Customer
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            Address = row.Address,
                            City = row.City,
                            State = row.State,
                            Zip = row.Zip,
                            Phone= row.Phone,
                            UserId= row.UserId
                        
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

        public static List<Customer> LoadByUserId(int id)
        {
            try
            {
                List<Customer> rows = new List<Customer>();
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    (from c in dvd.tblCustomers
                     where c.UserId == id
                     select new
                     {
                         c.Id,
                         c.FirstName,
                         c.LastName,
                         c.Address,
                         c.City,
                         c.State,
                         c.Zip,
                         c.Phone,
                         c.UserId
                     }).Distinct().ToList().ForEach(c => rows.Add(new Customer
                     {
                         Id = c.Id,
                         FirstName = c.FirstName,
                         LastName = c.LastName,
                         Address = c.Address,
                         City = c.City,
                         State = c.State,
                         Zip = c.Zip,
                         Phone = c.Phone,
                         UserId = c.UserId
                     }));
                    return rows;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblCustomer row = dvd.tblCustomers.Where(s => s.Id == customer.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.FirstName = customer.FirstName;
                        row.LastName = customer.LastName;
                        row.Address = customer.Address;
                        row.City = customer.City;
                        row.State = customer.State;
                        row.Zip = customer.Zip;
                        row.Phone = customer.Phone;
                        row.UserId = customer.UserId;
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

                    tblCustomer row = dvd.tblCustomers.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblCustomers.Remove(row);
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
