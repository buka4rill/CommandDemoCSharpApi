using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        /** 
            An interface is basically like a contract
            that is provided to a class which would consume 
            this data, which shouldn't be broken.

            We don't implement in an interface, we just define commands
            that would be used.

            This interface is implemented in MockCommanderRepo for test.

            This interface is also implemented in SqlCommanderRepo
        **/

        bool SaveChanges();

        // Give All Command Objects/Resources
        IEnumerable<Command> GetAllCommands();

        // Return a single command based by Id
        Command GetCommandById(int id);

        // Create a Command
        void CreateCommand(Command cmd);

        // Update a command
        void UpdateCommand(Command cmd);

    }
}