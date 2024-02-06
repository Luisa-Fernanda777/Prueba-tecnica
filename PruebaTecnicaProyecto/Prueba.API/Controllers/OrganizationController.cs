using Application.OrganizationDto;
using Domain.Organizations;
using Domain.Products;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastucture.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Prueba.API.Controllers;

[Route("api/organization")]
[ApiController]
public class OrganizationController : ControllerBase{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ProductDbContext _productDbContext;

    public OrganizationController(ApplicationDbContext applicationDbContext, ProductDbContext productDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _productDbContext = productDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get(){
        var organizations = await _applicationDbContext.Organizations
        .Include(_ => _.Products)
        .Include(_ => _.Users).ToListAsync();

        return Ok(organizations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id){
       
        var organizations = await _applicationDbContext.Organizations
        .Include(_ => _.Products)
        .Include(_ => _.Users)
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

        return Ok(organizations);        
    }

    private static Organization MapOrganizationObject(OrganizationDTO payload){
        var result = new Organization();
        result.Name = payload.Name;
        result.SlugTenant = payload.SlugTenant;
        result.Products = new List<Product>();
        payload.Products.ForEach(_ => {
            var newProduct = new Product();
            newProduct.Name = _.Name;
            newProduct.Price = _.Price;
            result.Products.Add(newProduct);
            });
        result.Users = new List<User>();
        payload.Users.ForEach(_ => {
            var newUser = new User();
            newUser.Email = _.Email;
            newUser.Password = _.Password;
            result.Users.Add(newUser);
        });
        return result;
    }


    [HttpPost]
    public async Task<IActionResult> Post(OrganizationDTO payloadOrg ){
        var newOrganization = MapOrganizationObject(payloadOrg);
        //var newOrganization = _mapper.Map<Organization>(payloadOrg);
        _applicationDbContext.Organizations.Add(newOrganization);
        await _applicationDbContext.SaveChangesAsync();
        MigrationHelper.ApplyMigrations(_productDbContext);
        return Created($"/organization/{newOrganization.Id}", newOrganization);
    }

    [HttpPut]
    public async Task<IActionResult> Put(OrganizationDTO payloadOrg){
        var updateOrganization = MapOrganizationObject(payloadOrg);
        //var newOrganization = _mapper.Map<Organization>(payloadOrg);
        _applicationDbContext.Organizations.Update(updateOrganization);
        await _applicationDbContext.SaveChangesAsync();
        return Ok(updateOrganization);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var organizationToDelete = await _applicationDbContext.Organizations
        .Include(_ => _.Products)
        .Include(_ => _.Users).Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

        if(organizationToDelete == null){
            return NotFound();
        }
        _applicationDbContext.Organizations.Remove(organizationToDelete);
        await _applicationDbContext.SaveChangesAsync();

        return NoContent();
    }
}