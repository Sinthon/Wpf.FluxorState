using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wpf.FluxorState.API.Hubs;
using Wpf.FluxorState.API.Weathers;
using Microsoft.AspNetCore.SignalR;

namespace Wpf.FluxorState.API.Apis;

public static class WeatherForecastApi
{
    public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/WeatherForecast").WithTags(nameof(WeatherForecast));

        group.MapGet("/", GetAllWeatherForecasts)
            .WithName("GetAllWeatherForecasts")
            .WithOpenApi();

        group.MapGet("/{id}", GetWeatherForecastById)
            .WithName("GetWeatherForecastById")
            .WithOpenApi();

        group.MapGet("/by", GetWeatherForecastByParams)
            .WithName("GetWeatherForecastByParams")
            .WithOpenApi();

        group.MapPut("/{id}", UpdateWeatherForecast)
            .WithName("UpdateWeatherForecast")
            .WithOpenApi();

        group.MapPost("/", CreateWeatherForecast)
            .WithName("CreateWeatherForecast")
            .WithOpenApi();

        group.MapDelete("/{id}", DeleteWeatherForecast)
            .WithName("DeleteWeatherForecast")
            .WithOpenApi();
    }

    private static async Task<List<WeatherForecast>> GetAllWeatherForecasts([AsParameters] WeatherService service)
    {
        return await service.Context
            .WeatherForecasts
            .AsNoTracking()
            .ToListAsync(service.Ctn);
    }

    private static async Task<Results<Ok<WeatherForecast>, NotFound>> GetWeatherForecastById([AsParameters] WeatherService service, Guid id)
    {
        return await service.Context.WeatherForecasts
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id, service.Ctn)
            is WeatherForecast forecast
                ? TypedResults.Ok(forecast)
                : TypedResults.NotFound();
    }

    private static async Task<List<WeatherForecast>> GetWeatherForecastByParams([AsParameters] WeatherService service, Guid? id, DateOnly? date)
    {
        var query = service.Context.WeatherForecasts.AsQueryable();

        if (id.HasValue)
            query = query.Where(w => w.Id == id);

        if (date.HasValue)
            query = query.Where(w => w.Date == date);

        return await query.AsNoTracking().ToListAsync(service.Ctn);
    }

    private static async Task<Results<Ok, NotFound>> UpdateWeatherForecast([AsParameters] WeatherService service, Guid id, WeatherForecast updatedForecast)
    {
        var existingForecast = await service.Context.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id, service.Ctn);

        if (existingForecast == null)
            return TypedResults.NotFound();

        existingForecast.Date = updatedForecast.Date;
        existingForecast.TemperatureC = updatedForecast.TemperatureC;
        existingForecast.Summary = updatedForecast.Summary;

        service.Context.WeatherForecasts.Update(existingForecast);

        existingForecast.RecordEvent(new WeatherForecastUpdated(existingForecast));

        await service.Context.SaveChangesAsync(service.Ctn);

        return TypedResults.Ok();
    }

    private static async Task<Results<Created<WeatherForecast>, BadRequest<string>>> CreateWeatherForecast([AsParameters] WeatherService service, WeatherForecast newForecast)
    {
        if (newForecast == null)
            return TypedResults.BadRequest("Weather forecast data is required.");

        newForecast.Id = Guid.NewGuid();

        service.Context.WeatherForecasts.Add(newForecast);

        newForecast.RecordEvent(new WeatherForecastCreated(newForecast));

        await service.Context.SaveChangesAsync(service.Ctn);

        return TypedResults.Created($"/api/WeatherForecast/{newForecast.Id}", newForecast);
    }

    private static async Task<Results<Ok, NotFound>> DeleteWeatherForecast([AsParameters] WeatherService service, Guid id)
    {
        var forecastToDelete = await service.Context.WeatherForecasts
            .FirstOrDefaultAsync(w => w.Id == id, service.Ctn);

        if (forecastToDelete == null)
            return TypedResults.NotFound();

        forecastToDelete.RecordEvent(new WeatherForecastDeleted(forecastToDelete));

        service.Context.WeatherForecasts.Remove(forecastToDelete);

        await service.Context.SaveChangesAsync(service.Ctn);

        return TypedResults.Ok();
    }
}

