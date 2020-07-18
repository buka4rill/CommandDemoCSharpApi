using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Data;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        // Constructor Dependency Injection
        // Injecting the Commander Db context
        private readonly CommanderContext _context;
        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            // throw new System.NotImplementedException();

            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            // throw new NotImplementedException();
            
            // Check to see if there's command 
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            // throw new System.NotImplementedException();
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            // throw new System.NotImplementedException();
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            // Save changes in the database by calling 
            // this method
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            // Because of the way Db Context works,
            // We are doing nothing!
        }
    }
}