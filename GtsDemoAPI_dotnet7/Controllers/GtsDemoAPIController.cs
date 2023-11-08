//using Microsoft.AspNetCore.Http;
using GtsDemoAPI_dotnet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace GtsDemoAPI_dotnet7.Controllers
{
    [Route("api/GtsDemoAPI")]
    [ApiController]
    public class GtsDemoAPIController : ControllerBase
    {

        private readonly ApiDemoDbContext _apiDemoDbContext;
        public GtsDemoAPIController(ApiDemoDbContext apiDemoDbContext)
        {
            _apiDemoDbContext = apiDemoDbContext;
        }

        [HttpGet]
        [Route("get-users-list")]
        public async Task<IActionResult> GetAsyncUser()
        {
            var users = await _apiDemoDbContext.users.ToListAsync();
            return Ok(users);
        }
        

        // This method returns the User information by ID search
        [HttpGet("[action]/{id:int}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)] //OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
        public ActionResult<Users> GetUserById(int id)
        {
            // ID value is not valid
            if (id <= 0) 
            {
                Console.WriteLine("Warning! ID value is not valid!");
                return BadRequest();
            } 

            // There is no user with such ID
            var userResult = _apiDemoDbContext.users.FirstOrDefault(u => u.ID == id);
            if (userResult == null) 
            {
                Console.WriteLine("Warning! There is no such User with this ID!");
                return NotFound();
            }
            // No problem, user information will be printed
            return Ok(userResult);

        }


        // This method returns the Member information by ID search
        [HttpGet("[action]/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Unauthorized
        public ActionResult<Members> GetMemberById(int id)
        {
            // ID value is not valid
            if (id <= 0)
            {
                Console.WriteLine("Warning! ID value is not valid!");
                return BadRequest();
            }

            // There is no member with such ID
            var memberResult = _apiDemoDbContext.members.FirstOrDefault(u => u.ID == id);
            if (memberResult == null)
            {
                Console.WriteLine("Warning! There is no such User with this ID!");
                return NotFound();
            }
            // No problem, user information will be printed
            return Ok(memberResult);

        }

        
        [HttpPost("[action]/{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Not Found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Server Error
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] // Unauthorized
        [ProducesResponseType(StatusCodes.Status204NoContent)] // No Content
        public async Task<ActionResult<Members>> UpdateMember(int id, [FromBody]Members updateReq)
        {

            if(id != updateReq.ID)
            {
                Console.WriteLine("Warning! There is no such Member with this ID!");
                return NotFound();
            }

            // Database commands work, however does not update
            _apiDemoDbContext.members.Update(updateReq);
            await _apiDemoDbContext.SaveChangesAsync();
            // No problem, member information will be printed
            Console.WriteLine("Member with ID: " + updateReq.ID + " with name: " + updateReq.Name + " called request");
            return NoContent();

        }

    }
}
