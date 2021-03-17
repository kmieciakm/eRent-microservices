using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.DatabaseContext
{
    class RentDbContextCleaner
    {
        public void ClearDatabase(RentDbContext dbContext)
        {
            // IMPROVEMENT: Check if async calls does not impact removing connected entities

            List<Task> clearSetTasks = new List<Task>() {
                ClearSet(dbContext.Clients),
                ClearSet(dbContext.Rents)
            };
            Task.WhenAll(clearSetTasks);
        }

        private async Task ClearSet<T>(DbSet<T> dbSet) where T : class
        {
            await dbSet.ForEachAsync(entity => dbSet.Remove(entity));
        }
    }
}
