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
    public class OrderManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblOrder row = new tblOrder();
                    row.Id = dvd.tblOrders.Any() ? dvd.tblOrders.Max(s => s.Id) + 1 : 1;
                    row.CustomerId = order.CustomerId;
                    row.OrderDate = order.OrderDate;
                    row.UserId = order.UserId;
                    row.ShipDate = order.ShipDate;

                    order.Id = row.Id;
                    dvd.tblOrders.Add(row);
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

        public static List<Order> Load()
        {
            try
            {
                List<Order> rows = new List<Order>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblOrders
                        .ToList()
                        .ForEach(s => rows.Add(new Order
                        {
                            Id = s.Id,
                            CustomerId = s.CustomerId,
                            OrderDate = s.OrderDate,
                            UserId = s.UserId,
                            ShipDate = s.ShipDate,
                        }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Order LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblOrder row = dvd.tblOrders.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new Order
                        {
                            Id = row.Id,
                            CustomerId = row.CustomerId,
                            OrderDate = row.OrderDate,
                            UserId = row.UserId,
                            ShipDate= row.ShipDate,
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

        public static int Update(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblOrder row = dvd.tblOrders.Where(s => s.Id == order.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.CustomerId = order.CustomerId;
                        row.OrderDate = order.OrderDate;
                        row.UserId = order.UserId;
                        row.ShipDate = order.ShipDate;
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

                    tblOrder row = dvd.tblOrders.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblOrders.Remove(row);
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

