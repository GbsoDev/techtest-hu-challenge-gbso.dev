using Challenge.Domain.Dtos;

namespace Challenge.Domain.Ports
{
	public interface IEmailService
	{
		Task SendEmailAsync(EmailDto emailRecipient, CancellationToken cancellationToken);
	}
}