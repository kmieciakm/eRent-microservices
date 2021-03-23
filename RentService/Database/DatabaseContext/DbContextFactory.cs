using System.Collections.Generic;
using System.Text;

namespace Database.DatabaseContext
{
    static class DbContextFactory
    {
        public static InMemoryRentDatabase CreateInMemoryRentDatabase() => new InMemoryRentDatabase();
    }
}
