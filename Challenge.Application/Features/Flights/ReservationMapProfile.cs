using AutoMapper;
using Challenge.Domain.Entities.Flights;
using Challenge.Domain.Validations;

namespace Challenge.Application.Features.Flights
{
	public class FlightMapProfile : Profile
	{
		public FlightMapProfile()
		{

			CreateMap<Flight, FlightDto>()
				.ConstructUsing(flight => new FlightDto
				{
					Id = flight.Id,
					OriginCityId = flight.OriginCity.IsNotNull() ? flight.OriginCity!.Id : Guid.Empty,
					OriginCityName = flight.OriginCity.IsNotNull() ? flight.OriginCity!.CityName : string.Empty,
					OriginAirportName = flight.OriginCity.IsNotNull() ? flight.OriginCity!.AirportName : string.Empty,
					DestinationCityId = flight.DestinationCity.IsNotNull() ? flight.DestinationCity!.Id : Guid.Empty,
					DestinationCityName = flight.DestinationCity.IsNotNull() ? flight.DestinationCity!.CityName : string.Empty,
					DestinationAirportName = flight.DestinationCity.IsNotNull() ? flight.DestinationCity!.AirportName : string.Empty,
					FlightNumber = flight.FlightNumber,
					DepartureTime = flight.DepartureTime,
					ArrivalTime = flight.ArrivalTime,
					FlightCode = flight.FlightCode,
				})
				.ForAllMembers(config => config.Ignore());
		}
	}
}
