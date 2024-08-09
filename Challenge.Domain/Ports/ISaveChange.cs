
namespace Challenge.Domain.Ports
{
	public interface ISaveChange
	{
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}