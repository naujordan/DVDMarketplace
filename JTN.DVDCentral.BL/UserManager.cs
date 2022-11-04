using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base("Cannot login iwth these credentials.")
        {
        }
        public LoginFailureException(string message) : base(message)
        {
        }

    }
    public static class UserManager
    {
        private static string GetHash(string password)
        {
            using (var hasher = SHA1.Create())
            {
                var hashbytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }
        public static int Insert(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dvd.Database.BeginTransaction();
                    tblUser row = new tblUser();
                    row.Id = dvd.tblUsers.Any() ? dvd.tblUsers.Max(u => u.Id) + 1 : 1;
                    row.FirstName = user.FirstName;
                    row.LastName = user.LastName;
                    row.UserId = user.UserId;
                    row.Password = GetHash(user.Password);

                    dvd.tblUsers.Add(row);
                    results = dvd.SaveChanges();
                    if (rollback) transaction.Rollback();
                    user.Id = row.Id;
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static int Update(User user, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dvd.Database.BeginTransaction();

                    tblUser row = dvd.tblUsers.Where(s => s.Id == user.Id).FirstOrDefault();
                    if (row != null)
                    {
                        row.FirstName = user.FirstName;
                        row.LastName = user.LastName;
                        row.UserId = user.UserId;
                        row.Password = user.Password;
                        results = dvd.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception();
                    }

                }

                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int DeleteAll()
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblUsers.RemoveRange(dvd.tblUsers.ToList());
                    return dvd.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static bool Login(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.UserId))
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        using (DVDCentralEntities dvd = new DVDCentralEntities())
                        {
                            tblUser tblUser = dvd.tblUsers.FirstOrDefault(u => u.UserId == user.UserId);

                            if (tblUser != null)
                            {
                                //valid user
                                if (tblUser.Password == GetHash(user.Password))
                                {
                                    user.Id = tblUser.Id;
                                    user.FirstName = tblUser.FirstName;
                                    user.LastName = tblUser.LastName;
                                    return true;
                                }
                                else
                                {
                                    throw new LoginFailureException();
                                    //or throw new LoginFailureException("Custom Message");
                                }
                            }
                            else
                            {
                                //Invalid user
                                throw new Exception("UserId could not be found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Password was not set.");
                    }
                }
                else
                {
                    throw new Exception("UserID was not set.");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static void Seed()
        {
            User user = new User
            {
                UserId = "jnau",
                FirstName = "Jordan",
                LastName = "Nau",
                Password = "password"
            };
            Insert(user);

            user = new User
            {
                UserId = "bfoote",
                FirstName = "Brian",
                LastName = "Foote",
                Password = "maple"
            };
            Insert(user);
        }

        public static User LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    tblUser row = dvd.tblUsers.FirstOrDefault(s => s.Id == id);

                    if (row != null)
                    {
                        return new User
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            UserId = row.UserId,
                            Password = row.Password

                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<User> Load()
        {
            try
            {
                List<User> rows = new List<User>();

                using (DVDCentralEntities dvd = new DVDCentralEntities())
                {
                    dvd.tblUsers
                        .ToList()
                        .ForEach(s => rows.Add(new User
                        {
                            Id = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            UserId = s.UserId,
                            Password = s.Password
                        }));
                    return rows;
                }

            }
            catch (Exception)
            {

                throw;
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

                    tblUser row = dvd.tblUsers.Where(s => s.Id == id).FirstOrDefault();
                    if (row != null)
                    {
                        dvd.tblUsers.Remove(row);
                        results = dvd.SaveChanges();

                        if (rollback) dbContextTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception();
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
