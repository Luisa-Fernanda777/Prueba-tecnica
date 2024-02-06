using Application.OrganizationDto;
using Domain.Users;
using Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Prueba.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get(){
        var users = await _applicationDbContext.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id){
       
        var user = await _applicationDbContext.Users
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

        return Ok(user);        
    }

    private static User MapUserObject(UserDto payload){
        var result = new User
        {
            Email = payload.Email,
            Password = payload.Password,
            OrganizationId = payload.OrganizationId
        };

        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDto payloadUser ){
        var newUser = MapUserObject(payloadUser);
        _applicationDbContext.Users.Add(newUser);
        await _applicationDbContext.SaveChangesAsync();
        return Created($"/user/{newUser.Id}", newUser);
    }    

    [HttpPut]
    public async Task<IActionResult> Put(UserDto payloadUser){
        var updateUser = MapUserObject(payloadUser);
        //var newOrganization = _mapper.Map<Organization>(payloadUser);
        _applicationDbContext.Users.Update(updateUser);
        await _applicationDbContext.SaveChangesAsync();
        return Ok(updateUser);
    }    

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var userToDelete = await _applicationDbContext.Users
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

        if(userToDelete == null){
            return NotFound();
        }
        _applicationDbContext.Users.Remove(userToDelete);
        await _applicationDbContext.SaveChangesAsync();

        return NoContent();
    }    
}