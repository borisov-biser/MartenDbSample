namespace MartenDbSample.Common.Abstractions
{
    public abstract class Aggregate : IAggregate
    {
        public Guid Id { get; init; }
        public long Version { get; protected set; }

        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        protected Aggregate()
        {
        }

        protected Aggregate(Guid streamId)
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
