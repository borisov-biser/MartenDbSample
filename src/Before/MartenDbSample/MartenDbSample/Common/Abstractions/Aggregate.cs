namespace MartenDbSample.Common.Abstractions
{
    public abstract class Aggregate : IAggregate
    {
        public Guid Id { get; }
        public long Version { get; }

        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        public Aggregate(Guid streamId)
        {
            Id = streamId;
        }

        public virtual void When(IEvent @event)
        {

        }

        public IEnumerable<IEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        protected void AddUncommittedEvent(IEvent @event)
        {
            _uncommittedEvents.Add(@event);
        }
    }
}
