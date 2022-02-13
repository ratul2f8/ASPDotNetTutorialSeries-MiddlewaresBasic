using middlewares_tut.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TestingMiddleware>();
// Add services to the container.
builder.Services.AddControllers();
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Map("/Hello", applicationBuilder =>
{
	Console.WriteLine("Using mapper for /Hello route");
	applicationBuilder.Run(async context =>
	{
		await context.Response.WriteAsync("Hello world");
	});
});
//use UseWhen instead of MapWhen to merge back to the main pipeline
app.MapWhen(context => context.Request.Query.ContainsKey("q"), applicationBuilder =>
{
	Console.WriteLine("Using mapper for /Hello route");
	applicationBuilder.Run(async context =>
	{
		await context.Response.WriteAsync("The queries of the request contains the letter 'q' ");
	});
});
app.UseMiddleware<TestingMiddleware>();
;
app.Run();
