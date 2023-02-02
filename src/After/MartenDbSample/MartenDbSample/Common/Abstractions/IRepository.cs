namespace MartenDbSample.Common.Abstractions
{
    public interface IRepository<T> where T : Aggregate
    {
        public Task CreateAsync(T aggregate, CancellationToken ctx = default);

        public Task UpdateAsync(T aggregate, CancellationToken ctx = default);

        public Task<T?> GetByIdAsync(Guid streamId, CancellationToken ctx = default);

        public Task<T?> GetSelfAggregateProjectionAsync<T>(Guid streamId, CancellationToken ctx = default);
    }
}
