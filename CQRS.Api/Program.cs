

using CQRS.Api;
using CQRS.Application;
using CQRS.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

{

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration); 

}

var app = builder.Build();

{
        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();

}
