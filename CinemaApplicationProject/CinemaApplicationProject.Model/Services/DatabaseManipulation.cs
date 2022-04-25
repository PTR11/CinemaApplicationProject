using CinemaApplicationProject.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace CinemaApplicationProject.Model.Services
{
    public static class DatabaseManipulation
    {
        public static DatabaseContext context;



        public static T AddElement<T>(T element) where T : class
        {
            try
            {
                context.Add(element);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return element;
        }

        public static bool DeleteElement<T>(T element) where T : class
        {
            try
            {
                context.Remove(element);
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public static bool UpdateElementAsync<T>(T element) where T : class
        {
            try
            {
                context.Update(element);
                context.Entry(element).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;

        }
    }
}
