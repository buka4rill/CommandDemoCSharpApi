using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    /**
        The class that implements the ICommander Interface
    
        This is a mock repository. That means we are not 
        connecting to a real database. We are hard coding data
        which can be useful for testing.
    **/
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            // throw new System.NotImplementedException();

            // Return a list of commands
            var commands = new List<Command>
            {
                new Command
                {
                    Id = 1,
                    HowTo = "Make Soup",
                    Line = "Cut some onions",
                    Platform = "A pot"
                },
                new Command
                {
                    Id = 2,
                    HowTo = "Make bread",
                    Line = "Get some flour",
                    Platform = "Baking pan"
                },
                new Command
                {
                    Id = 3,
                    HowTo = "Make a cup of tea",
                    Line = "Place tea bag in cup",
                    Platform = "Kettle & Cup"
                },
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            // throw new System.NotImplementedException();

            // Return a single command by ID
            return new Command
            {
                Id = 0,
                HowTo = "Boil an egg",
                Line = "Boil water",
                Platform = "Kettle & Pan"
            };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}