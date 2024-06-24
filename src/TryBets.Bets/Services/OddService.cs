namespace TryBets.Bets.Services;

public class OddService : IOddService
{
    private readonly HttpClient _client;
    public OddService(HttpClient client)
    {
        _client = client;
    }

    public async Task<object> UpdateOdd(int MatchId, int TeamId, decimal BetValue)
    {
        /* var response = await _client.PatchAsync($"http://localhost:5504/Odd/{MatchId}/{TeamId}/{BetValue}", null);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(); */
        throw new NotImplementedException();
    }
}