using System.Text.Json;

#pragma warning disable CS8618
namespace Challenge.Domain.Entities.OutBoxes
{
	public class Outbox : DomainEntity<Guid>, IAuditableEntity
	{
		public EventType EventType { get; private set; }
		public string EventData { get; private set; }
		public bool Processed { get; private set; }
		public DateTime EventDate { get; private set; }

		public Outbox(EventType eventType, object eventData, DateTime eventDate)
			: this(Guid.Empty, eventType, eventData, false, eventDate, OutboxValidator.ValidateToCreate)
		{
		}

		private Outbox(Guid id, EventType eventType, object eventData, bool processed, DateTime eventDate, Func<Outbox, Validations.ValidationSet>? validateAction)
		{
			Id = id;
			EventType = eventType;
			SerializeAndSetEventData(eventData);
			Processed = processed;
			EventDate = eventDate;
			validateAction?.Invoke(this).ValidateAndThrow();
		}

		private Outbox()
		{
		}

		public void MarkAsProcessed()
		{
			Processed = true;
		}

		public void SerializeAndSetEventData(object eventData)
		{
			EventData = JsonSerializer.Serialize(eventData);
			_deserializedData = null;
		}

		public T DeserializedData<T>()
		{
			if (_deserializedData == null)
			{
				_deserializedData = JsonSerializer.Deserialize<T>(EventData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}
			return (T)_deserializedData!;
		}

		private object? _deserializedData;
	}
}