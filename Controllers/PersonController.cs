using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PersonSkill.Models;

namespace PersonSkill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        readonly IPSRepository PSRepository;

        public PersonController(IPSRepository psRepository)
        {
            PSRepository = psRepository;
        }

        [HttpGet(Name = "GetAllItems")]
        public IEnumerable<Person> Get()
        {
            return PSRepository.Get();
        }

        [HttpGet("{id}", Name = "GetPSItem")]
        public IActionResult Get(int Id)
        {
            Person personItem = PSRepository.Get(Id);

            if (personItem == null)
            {
                return NotFound();
            }

            return new ObjectResult(personItem);
        }

        [HttpPost]
        public IActionResult PostPerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            foreach(var skillchar in person.skills)
            {
                if(skillchar.skillName == "" || double.IsNaN(skillchar.level))
                {
                    return BadRequest();
                }
            }
            PSRepository.PostPerson(person);
            return Ok(person);
        }

        [HttpPut]
        public IActionResult PutPerson([FromBody] Person updatedPersonItem)
        {
            if (updatedPersonItem == null)
            {
                return BadRequest();
            }
            PSRepository.PutPerson(updatedPersonItem);
            return Ok(updatedPersonItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedPersonItem = PSRepository.Delete(id);

            if (deletedPersonItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedPersonItem);
        }
    }
}
