using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Commander.Controllers
{
    
    [Route("api/commands")]
    [ApiController]
    
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
       
        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommmands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(string id)
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult <Root> CreateCommand(Root commandRoot)
        {
            string temp="";
            CommandCreateDto commandCreateDto = new CommandCreateDto();
            commandCreateDto.task_id = commandRoot.tasks.task_id;
            commandCreateDto.completed = commandRoot.tasks.completed;
            commandCreateDto.result = commandRoot.tasks.payload.result;
            if (commandRoot.tasks.payload.periods != null)
            {
                foreach (Period period in commandRoot.tasks.payload.periods)
                {
                    temp = temp + period.key + ":" + period.value;
                }
                commandCreateDto.result = commandCreateDto.result + ";" + temp;
            }

            if (commandRoot.tasks.payload.locations != null)
            {
                temp = "";
                foreach (Location location in commandRoot.tasks.payload.locations)
                {
                    temp = temp + location.key + ":" + location.value;
                }
                commandCreateDto.result = commandCreateDto.result + ";" + temp;
            }
            if (commandRoot.tasks.payload.requests != null)
            {
                temp = "";
                foreach (Requests requests in commandRoot.tasks.payload.requests)
                {
                    temp = temp+ ";" +requests.key + ":" + requests.name + ":" + requests.status + ":" + requests.info;
                }
                commandCreateDto.result = commandCreateDto.result + "|" + temp;
            }

            if (commandRoot.tasks.payload.error_text != null)
            {
                commandCreateDto.result = commandCreateDto.result + " " + commandRoot.tasks.payload.error_text;
            }
            if (commandRoot.tasks.payload.response != null)
            {
                commandCreateDto.result = commandCreateDto.result + " " + commandRoot.tasks.payload.response;
            }

            if (commandRoot.tasks.payload.url != null)
            {
                commandCreateDto.result = commandCreateDto.result + " " + commandRoot.tasks.payload.url;
            }
            
            /*Payload payload = new Payload();
            int payloadSize = commandRoot.tasks.payload.Count();
            payload = commandRoot.tasks.payload[0];
            commandCreateDto.result = payload.result;*/
            /*
            commandCreateDto.doc_types = "NaN";
            if (commandRoot.tasks.payload.doc_types != null){
                commandCreateDto.doc_types = commandRoot.tasks.payload.doc_types;
            }
            commandCreateDto.error_code = "NaN";
            if (commandRoot.tasks.payload.error_code != null)
            {
                commandCreateDto.error_code = commandRoot.tasks.payload.error_code;
            }

            commandCreateDto.error_text = "NaN";
            if (commandRoot.tasks.payload.error_text != null)
            {
                commandCreateDto.error_text = commandRoot.tasks.payload.error_text;
            }*/


            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.task_id}, commandReadDto);      
        }
        /*
        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.task_id }, commandReadDto);
        }

        */

        //PUT api/commands/{id}
        [HttpPut("{task_id}")]
        public ActionResult UpdateCommand(string id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{task_id}")]
        public ActionResult PartialCommandUpdate(string id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(string id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        
    }
}