using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public class BaseEntity
    {
        private List<IDomainEvent> _domainEvents= new List<IDomainEvent>();

        public Guid Id { get; private set; }
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }


        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()=> _domainEvents.Clear();


    }


}
