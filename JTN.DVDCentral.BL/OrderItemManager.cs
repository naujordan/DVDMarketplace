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
    public static class OrderItemManager
    {
        private const string Message = "Row does not exist";
        public static int Insert(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblOrderItem row = new tblOrderItem();
                    row.Id = dvd.tblOrderItems.Any() ? dvd.tblOrderItems.Max(s => s.Id) + 1 : 1;
                    row.OrderId = orderItem.OrderId;
                    row.MovieId = orderItem.MovieId;
                    row.Quantity = orderItem.Quantity;
                    row.Cost = orderItem.Cost;

                    orderItem.Id = row.Id;
                    dvd.tblOrderItems.Add(row);
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

        public static List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> rows = new List<OrderItem>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblOrderItems
                        .ToList()
                        .ForEach(s => rows.Add(new OrderItem
                        {
                            Id = s.Id,
                            OrderId = s.OrderId,
                            MovieId = s.MovieId,
                            Quantity = s.Quantity,
                            Cost = s.Cost,
                        }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static OrderItem LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblOrderItem row = dvd.tblOrderItems.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new OrderItem
                        {
                            Id = row.Id,
                            OrderId = row.OrderId,
                            MovieId = row.MovieId,
                            Quantity = row.Quantity,
                            Cost = row.Cost,
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

        public static int Update(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dvd.Database.BeginTransaction();

                    tblOrderItem row = dvd.tblOrderItems.Where(s => s.Id == orderItem.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.OrderId = orderItem.OrderId;
                        row.MovieId = orderItem.MovieId;
                        row.Quantity = orderItem.Quantity;
                        row.Cost = orderItem.Cost;
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

                    tblOrderItem row = dvd.tblOrderItems.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblOrderItems.Remove(row);
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
   
