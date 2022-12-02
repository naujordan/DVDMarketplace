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
                    row.OrderDate = DateTime.Now;
                    row.UserId = order.UserId;
                    row.ShipDate = row.OrderDate.AddDays(3);
                    row.SubTotal = order.SubTotal;
                    row.Tax = order.SubTotal;
                    row.Total = order.Total;

                    

                    order.Id = row.Id;
                    dvd.tblOrders.Add(row);
                    results = dvd.SaveChanges();

                    foreach (OrderItem orderItem in order.OrderItems)
                    {
                        tblOrderItem newRow = new tblOrderItem();

                        newRow.Id = dvd.tblOrderItems.Any() ? dvd.tblOrderItems.Max(s => s.Id) + 1 : 1;
                        newRow.Cost = orderItem.Cost;
                        newRow.MovieId = orderItem.MovieId;
                        newRow.OrderId = order.Id;
                        newRow.Quantity = orderItem.Quantity;

                        dvd.tblOrderItems.Add(newRow);
                        dvd.SaveChanges();
                    }

                    if (rollback) dbContextTransaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Order> Load(int? customerId = null)
        {
            try
            {
                List<Order> rows = new List<Order>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    var orders = (from o in dvd.tblOrders
                                  join c in dvd.tblCustomers on o.CustomerId equals c.Id
                                  join u in dvd.tblUsers on o.UserId equals u.Id
                                  where o.CustomerId == customerId || customerId == null
                                  select new
                                  {
                                      o.Id,
                                      o.CustomerId,
                                      o.OrderDate,
                                      o.UserId,
                                      o.ShipDate, 
                                      c.FirstName,
                                      c.LastName,
                                      UserName = u.UserId,
                                      o.SubTotal,
                                      o.Tax,
                                      o.Total
                                  }).ToList();
                    
                    orders.ForEach(s => rows.Add(new Order
                        {
                            Id = s.Id,
                            CustomerId = s.CustomerId,
                            OrderDate = s.OrderDate,
                            UserId = s.UserId,
                            ShipDate = s.ShipDate,
                            CustomerName = s.LastName + ", " + s.FirstName,
                            UserName = s.UserName,
                            SubTotal = s.SubTotal,
                            Tax = s.Tax,
                            Total = s.Total
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
                    var row = (from o in dvd.tblOrders
                                  join c in dvd.tblCustomers on o.CustomerId equals c.Id
                                  join u in dvd.tblUsers on o.UserId equals u.Id
                                  where o.Id == id
                                  select new
                                  {
                                      o.Id,
                                      o.CustomerId,
                                      o.OrderDate,
                                      o.UserId,
                                      o.ShipDate,
                                      c.FirstName,
                                      c.LastName,
                                      UserName = u.UserId,
                                      o.SubTotal,
                                      o.Tax,
                                      o.Total
                                  }).FirstOrDefault();


                    if (row != null)
                    {
                        return new Order
                        {
                            Id = row.Id,
                            CustomerId = row.CustomerId,
                            OrderDate = row.OrderDate,
                            UserId = row.UserId,
                            ShipDate = row.ShipDate,
                            CustomerName = row.LastName + ", " + row.FirstName,
                            UserName = row.UserName,
                            SubTotal = row.SubTotal,
                            Tax = row.Tax,
                            Total = row.Total,
                            OrderItems = OrderItemManager.LoadByOrderId(id)
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

