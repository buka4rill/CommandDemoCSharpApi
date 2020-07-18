using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        // Constructor 
        public CommanderContext(DbContextOptions<CommanderContext> options) : base(options)
        {
            
        }

        // Representation of Models in the Database
        public DbSet<Command> Commands { get; set; }
    }
}
