namespace MartenDbSample.Common.Abstractions
{
    public interface IAggregate
    {
        public Guid Id { get; }
        public long Version { get; }
    }
}
