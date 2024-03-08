
using EmployeeDashboard;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
     policy =>
     {
         policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
     });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

//how to create a get method using http get request
app.MapGet("/employee", async (DataContext context) =>
 await context.Employees.ToListAsync());
 


app.MapGet("/employee/{id}", async (DataContext context, int id) =>
    await context.Employees.FindAsync(id) is Employee employee ?
        Results.Ok(employee):
        Results.NotFound("Sorry, employee doesn't exist."));

app.MapPost("/employee/add", async (DataContext context, Employee employee) =>
{
    context.Employees.Add(employee);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Employees.ToListAsync());
});

//updating 
app.MapPut("/employee/{id}", async (DataContext context, Employee updatedEmploye, int id) =>
{
    var employee = await context.Employees.FindAsync(id);
    if (employee is null)
        return Results.NotFound("Sorry, employee doesn't exist. ");
    employee.EmployeeId = updatedEmploye.EmployeeId;
    employee.Name = updatedEmploye.Name;
    employee.Project = updatedEmploye.Project;
    await context.SaveChangesAsync();
    return Results.Ok(await context.Employees.ToListAsync());
});
app.MapDelete("/employee/{id}", async (DataContext context, int id) =>
{
    var employee = await context.Employees.FindAsync(id);
    if (employee is null)
        return Results.NotFound("Sorry, employee doesn't exist. ");

    context.Employees.Remove(employee);
    await context.SaveChangesAsync();
    return Results.Ok(employee);
});

app.Run();

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int EmployeeId { get; set; }

    public string Project { get; set; }


}
















/*using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;*/

/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CRUDAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// get endpoint
app.MapGet("/empdata", async (CRUDAppDbContext crudAppDbContext) =>
{
    var response = await crudAppDbContext.empData.ToListAsync();
    return Results.Ok(response);
});
// post endpoint
app.MapPost("/empdata/create", async (empData empData, CRUDAppDbContext crudAppDbContext) =>
{
    crudAppDbContext.empData.Add(empData);
    await crudAppDbContext.SaveChangesAsync();
    return Results.Ok();
});

// update endpoint
app.MapPut("/empdata/update", async (empData empData, CRUDAppDbContext crudAppDbContext) =>
{
    var response = await crudAppDbContext.empData.FindAsync(empData.Id);
    if (response == null)
    {
        return Results.NotFound();
    }
    response.Name = empData.Name;
    response.Code = empData.Code;
    response.Project_Name = empData.Project_Name;
    await crudAppDbContext.SaveChangesAsync();
    return Results.NoContent();
});

// delete endpoint.
app.MapDelete("/empdata/delete/{id}", async (int id, CRUDAppDbContext crudAppDbContext) =>
{
    var dbGadgets = await crudAppDbContext.empData.FindAsync(id);
    if (dbGadgets == null)
    {
        return Results.NoContent();
    }
    crudAppDbContext.empData.Remove(dbGadgets);
    await crudAppDbContext.SaveChangesAsync();
    return Results.Ok();
});

app.RunAsync();
*/
//DbContext
/*public class CRUDAppDbContext : DbContext
{
    public CRUDAppDbContext(DbContextOptions<CRUDAppDbContext> options) : base(options)
    {

    }
    public DbSet<Employee> Employee { get; set; }
}
*/
