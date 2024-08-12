using AutoMapper;
using Challenge.Application.Features.Reservations.Commands;
using Challenge.Domain.Entities.Reservations;
using Challenge.Domain.Validations;

namespace Challenge.Application.Features.Reservations
{
	public class ReservationMapProfile : Profile
	{
		public ReservationMapProfile()
		{
			CreateMap<CreateReservationCommand, Reservation>()
				.ConstructUsing(reservationDto => new Reservation(reservationDto.UserName, reservationDto.PassportNumber, reservationDto.Email, reservationDto.FlightId, reservationDto.SeatNumber))
				.ForAllMembers(config => config.Ignore());

			CreateMap<UpdateReservationCommand, Reservation>()
				.ConstructUsing(reservationDto => new Reservation(reservationDto.Id, reservationDto.UserName, reservationDto.PassportNumber, reservationDto.Email, reservationDto.FlightId, reservationDto.SeatNumber))
				.ForAllMembers(config => config.Ignore());

			CreateMap<Reservation, ReservationDto>()
				.ConstructUsing(reservation => new ReservationDto
				{
					Id = reservation.Id,
					UserName = reservation.UserName,
					PassportNumber = reservation.PassportNumber,
					Email = reservation.Email,
					FlightId = reservation.FlightId,
					FlightCode = reservation.Flight.IsNotNull() ? reservation.Flight!.FlightCode : string.Empty,
					SeatNumber = reservation.SeatNumber,
				})
				.ForAllMembers(config => config.Ignore());
		}
	}
}
