using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationHouseExtensions{
    public static void MapHouseExtensionEndPoints(this WebApplication app)
    {
        app.MapGet("/houses", async (IHouseRepository repo) => await repo.GetAll())
            .Produces<HouseDto[]>(StatusCodes.Status200OK);

        app.MapGet("/house/{houseId:int}", async (int houseId, IHouseRepository repo) => {
            var house = await repo.Get(houseId);
            
            if (house == null)
                return Results.Problem($"House with id {houseId} is not found.", statusCode: 404);
            
            return Results.Ok(house);
        }).ProducesProblem(404).Produces<HouseDetailDto>(statusCode: StatusCodes.Status200OK);

        app.MapPost("/houses", async ([FromBody]HouseDetailDto dto, IHouseRepository repo) => {
            if (!MiniValidator.TryValidate(dto, out var errors))
                return Results.ValidationProblem(errors);
            
            if (await repo.Get(dto.Id) == null)
                return Results.Problem($"House with id {dto.Id} not found.", statusCode: 404);

            var newHouse = await repo.Add(dto);
            return Results.Created($"/house/{newHouse.Id}", newHouse);
        }).ProducesValidationProblem().ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status201Created);

        app.MapPut("/houses/{houseId:int}", async (int houseId, [FromBody]HouseDetailDto dto, IHouseRepository repo) => {
            if (!MiniValidator.TryValidate(dto, out var errors)) 
                return Results.ValidationProblem(errors);

            if (await repo.Get(dto.Id) == null)
                return Results.Problem($"House with id {dto.Id} not found.", statusCode: 404);
            
            var updatedHouse = await repo.Update(dto);
            return Results.Ok(updatedHouse);
        }).ProducesValidationProblem().ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

        app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository repo) => 
        {
            if (await repo.Get(houseId) == null)
                return Results.Problem($"House with id {houseId} does not exist.", statusCode:404);

            await repo.Delete(houseId);
            return Results.Ok();
        }).ProducesValidationProblem().ProducesProblem(404).Produces(StatusCodes.Status202Accepted);
    }
}