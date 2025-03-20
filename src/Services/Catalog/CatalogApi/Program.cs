var builder = WebApplication.CreateBuilder(args);

//Add Services to container-DI.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    //opts.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate; // Set the desired option
}).UseLightweightSessions();

var app = builder.Build();

//configure the http request pipeline.
app.MapCarter();

app.Run();
