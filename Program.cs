

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();


app.MapGet("/", () =>
{
    return Results.Content("<h1>Green University provides shuttle bus services</h1>","text/html");
});

app.MapGet("/home", () =>
{
    var response = new
    {
        Title = "Green University Transport Management System",
        Developer = "MD. Rajib Hossain",
        Tools = "ASP .Net Core Web API"
    };


    return Results.Content("<h1>Green University Transport Management System</h1>","text/html");
});


List<Category> categories = new List<Category>();


// Read = Read a Category => Get :/api/categories


//app.MapGet("/api/categories", ([FromQuery]string searchValue) =>
//{
//    var foundSerachValue = categories.FirstOrDefault(value => value.Name == searchValue);

//    if (foundSerachValue == null)
//    {
//        return Results.Ok("categories Not Found ");
//    }
//    else
//    {        
//        return Results.Ok(categories);
//    }


//});


app.MapGet("/api/categories/test", () =>
{
    
        return Results.Ok(categories);
 


});



// Read = Read a Category => Get :/api/categories


app.MapGet("/api/categories", ([FromQuery]string searchValue) =>
{
    //var foundSerachValue = categories.FirstOrDefault(value => value.Name == searchValue);

    if (!string.IsNullOrEmpty(searchValue))
    {

        var foundCategory = categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

        return Results.Ok(foundCategory);
    }
    else
    {        
        return Results.Ok("categories Not Found");
    }

    
});


app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{

    var NewCategory = new Category
    {

        CategoryID = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description, //"A smartphone is a handheld mobile device that combines the features of a traditional cell phone with the advanced computing capabilities of a personal computer. The category is defined by its powerful mobile operating system, internet connectivity, and the ability to run downloadable applications. \r\n",
        CreatedAt = DateTime.Now

    };
   
    categories.Add(NewCategory);

    return Results.Created($"/api/catgories/{NewCategory.CategoryID}",NewCategory);
});





// Update = Update a Category => PUT :/api/categories


app.MapPut("/api/categories/{categID}", (Guid categID , [FromBody] Category categoryData) =>
{

    var foundCategory = categories.FirstOrDefault(category => category.CategoryID == categID);

    if (foundCategory == null)
    {
        return Results.NotFound("Category with this ID does not Exist");
    }
    else
    {

        foundCategory.Name = categoryData.Name;
        foundCategory.Description = categoryData.Description;

        return Results.NoContent();
    }


});


// Delete = Delete a Category => Delete :/api/categories


//app.MapDelete("/api/categories", ([FromBody] Category categoryData) =>
//{
//    var foundCategory = categories.FirstOrDefault(category => category.CategoryID == categoryData.CategoryID);

//    if (foundCategory == null)
//    {
//        return Results.NotFound("Category with this ID does not Exist");
//    }
//    else
//    {
//        categories.Remove(foundCategory);
//        return Results.NoContent();
//    }
//});

// Delete = Delete a Category => Delete :/api/categories


app.MapDelete("/api/categories/{categID}", (Guid categID) =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryID == categID);

    if (foundCategory == null)
    {
        return Results.NotFound("Category with this ID does not Exist");
    }
    else
    {
        categories.Remove(foundCategory);
        return Results.NoContent();
    }
});







app.Run();



public record Category
{
    public Guid CategoryID { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

};



// CRUD

// Create = Create a Category => POST :/api/categories

// Read = Read a Category => Get :/api/categories


// Update = Update a Category => PUT :/api/categories

// Delete = Delete a Category => Delete :/api/categories


// complit CRUD Operation with FromQuery, FromBody, CategoryName, Guid and etc. 