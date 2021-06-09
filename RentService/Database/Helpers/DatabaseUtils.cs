using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Helpers
{
    static class DatabaseUtils
    {
        /// <summary>
        /// Saves all changes made in the database context and clear all tracked entries.
        /// </summary>
        /// <param name="dbContext">Database context to save changes.</param>
        /// <returns>The number of state entries written to database.</returns>
        public static int CommitChanges(DbContext dbContext)
        {
            int saveResult = dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
            return saveResult;
        }
    }
}
