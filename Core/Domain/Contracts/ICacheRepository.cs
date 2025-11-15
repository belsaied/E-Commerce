namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        //Get ==> Already cached [return data] ==> response caching
        Task<string?> GetAsync(string key);

        //Set ==> No caching happen [first time to call end point] ==> Apply caching
        Task SetAsync(string key, object value, TimeSpan duration);
    }
}
