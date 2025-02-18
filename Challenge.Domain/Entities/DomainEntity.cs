﻿namespace Challenge.Domain.Entities
{
	public abstract class DomainEntity<TId> : IDomainEntity
		where TId : struct
	{
		public virtual TId Id { get; protected set; }

		object IDomainEntity.Id => Id;
	}
}
