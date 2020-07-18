using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    // api/commands
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        /**
            Constructor Dependency Injection
            This constructor also injects Automapper services
        **/
        private readonly ICommanderRepo _repository; // Readonly field
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            // What ever is injected (repository), assign to
            // _repository
            _repository = repository;

            // Mapper injected
            _mapper = mapper;
        }

        // Inefficient way of doing the above
        // private readonly MockCommanderRepo _repository = new MockCommanderRepo(); 

        // Property returns list of commands 
        // [GET] api/commands
        // [HttpGet]
        // public ActionResult <IEnumerable<Command>> GetAllCommands()
        // {
        //     // Use Repository in this property
        //     // A variable to hold results
        //     var commandItems = _repository.GetAllCommands();

        //     // Return Success status 200 OK
        //     return Ok(commandItems);
        // }
        
        // Property returns list of commands 
        // [GET] api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            // Use Repository in this property
            // A variable to hold results
            var commandItems = _repository.GetAllCommands();

            // Return Success status 200 OK
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // Property returns Command by Id 
        // [GET] api/commands/{id}
        // [HttpGet("{id}")]
        // public ActionResult <Command> GetCommandById(int id)
        // {
        //     // Single command item variable
        //     var commandItem = _repository.GetCommandById(id);

        //     // Check for null command item
        //     if (commandItem != null)
        //     {
        //         // Return Success status 200 OK
        //         return Ok(commandItem);
        //     }

        //     // Else return 404
        //     return NotFound();
        // }
        
        // Property returns Command by Id (mapped object)
        // [GET] api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            // Single command item variable
            var commandItem = _repository.GetCommandById(id);

            // Check for null command item
            if (commandItem != null)
            {
                // Return Success status 200 OK
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            // Else return 404
            return NotFound();
        }

        // Create Commands (mapped object)
        // [POST] api/commands
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            // Make use of Automapper to create a command model
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);

            // Save Changes in DB
            _repository.SaveChanges();

            // We want to pass back a CommandReadDto
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            // return Ok(commandReadDto);

            // Return 201 Created
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
        } 

        // Update Commands (mapped object)
        // [PUT] api/commands/{ID}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            // Check if command exists
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            // Here, we have CommandUpdateDto that has data and 
            // CommandModel from repo that has data
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            // In the interest of maintainig a separate interface from 
            // implementation, this is good practice...
            // but it isn't really needed because of the way Entity Framework
            // works
            _repository.UpdateCommand(commandModelFromRepo);

            // Save changes
            _repository.SaveChanges();

            // Return 204 Status
            return NoContent();
        }

        // Update Commands (mapped object)
        // [PATCH] api/commands/{ID}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            // Check if command exists
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            // We are receiving from the client, the patch document and we
            // can't apply it directly to our Command model
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            
            // Validation Check
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // Update Model Data in the repostiory
            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            // Save Changes
            _repository.SaveChanges();

            return NoContent();
        }

        // Delete a command
        // [DELETE] api/commands/{ID}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            // Check if command exists
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModelFromRepo);

            // Save 
            _repository.SaveChanges();

            // Return 204 Status
            return NoContent();
        }
    }
}