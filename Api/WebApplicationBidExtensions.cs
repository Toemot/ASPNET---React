    using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationBidExtensions
{
    public static void MapBidEndpoints(this WebApplication app)
    {
        app.MapGet("/house/{houseId:int}/bids", async (int houseId, IHouseRepository houseRepo, BidRepository bidRepo) => {
            if (await houseRepo.Get(houseId) == null) 
                return Results.Problem($"This house id {houseId} does not exist", statusCode: 404);

            var bid = await bidRepo.Get(houseId);
            return Results.Ok(bid);
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        app.MapPost("/house/{houseId:int}/bids", async (int houseId, [FromBody]BidDto dto, IBidRepository repo) =>{
            if (dto.HouseId != houseId)
                return Results.Problem($"House id of the dto {dto.HouseId} is not the same for the house {houseId}", 
                    statusCode:StatusCodes.Status400BadRequest);
            if (!MiniValidator.TryValidate(dto, out var errors))
            return Results.ValidationProblem(errors);

            var newBid = await repo.Add(dto);
            return Results.Created($"/houses/{newBid.HouseId}/bids", newBid);
        }).ProducesValidationProblem().ProducesProblem(400).Produces<BidDto>(StatusCodes.Status201Created);
    }


}