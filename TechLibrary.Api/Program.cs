using TechLibrary.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddConfigureServices();
builder.AddDataContext();
builder.AddDocumentation();
builder.AddSecurity();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();