using Homework16.Abstraction;
using Homework16.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Homework16.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IFileOperations _fileOperations;
        public PersonController(IFileOperations fileOperations) 
        {
            _fileOperations = fileOperations;
        
        }
        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            var validator = new PersonValidator();
            var result = validator.Validate(person);
            if (!result.IsValid)
            {
                var msg = "";
                foreach (var err in result.Errors)
                {
                    msg += err.ErrorMessage + " ";
                }
                return BadRequest(msg);
            }
            _fileOperations.AddRecord(person);
            return Ok(person.PersonId);
        }

        [HttpGet("allrecords")]
        public IActionResult GetPersons()
        {
            
            var result = _fileOperations.GetAllRecords();
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }

        }

        [HttpGet("{personId}")]
        public IActionResult getPerson(string personId)
        {
            
            var result = _fileOperations.GetRecord(personId);
            if (result != null)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }

        [HttpGet("filteredRecords")]
        public IActionResult FilterPersonsByPosition(Jobposition position)
        {
            
            var result = _fileOperations.GetSpecificRecords(position);
            if (result != null)
            {
                return Ok(result);
            }else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult EditPerson(Person person)
        {

            if (person.CreateDate > DateTime.Now)
            {
                return BadRequest("date should not exceed current date");
            }
            else if (person.FirstName.Length > 50 || person.LastName.Length > 50)
            {
                return BadRequest("name should not exceed 50 characters");
            }else if (person.Salary > 10000 || person.Salary < 0)
            {
                return BadRequest("salary is out of legal range!");
            }
            
            var result = _fileOperations.UpdateRecord(person);
            if (result == person.PersonId)
            {
            return Ok(result);

            } else 
                return NotFound(result);  
        }

        [HttpDelete]
        public IActionResult DeletePerson(string personId)
        {
            
            var result = _fileOperations.DeleteRecord(personId);
            if (result == personId)
            {
                return Ok(result);
            }
            else
                return NotFound(result); ;
            
        }
    }
}
