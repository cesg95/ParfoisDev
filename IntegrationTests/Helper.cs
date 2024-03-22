namespace IntegrationTests
{
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public static class Helper
    {
        public static async Task<T?> GetRequestContent<T>(HttpResponseMessage httpResponseMessage)
        {
            JsonSerializerOptions jsonSettings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            return JsonSerializer.Deserialize<T>(
                await httpResponseMessage.Content.ReadAsStringAsync(),
                jsonSettings);
        }

        public static StringContent BuildRequestContent<T>(T content)
        {
            string serialized = JsonSerializer.Serialize(content);

            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }
    }
}
