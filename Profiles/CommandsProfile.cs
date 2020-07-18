using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // What are we mapping from and to?
            // Source -> Target
            CreateMap<Command, CommandReadDto>(); // Dto for Get

            CreateMap<CommandCreateDto, Command>(); // Dto for Post

            CreateMap<CommandUpdateDto, Command>(); // Dto for Put

            CreateMap<Command, CommandUpdateDto>(); // Dto for Patch
        }
    }
}