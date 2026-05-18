using System.Text.Json;
using server.Models;

namespace server.Data
{
    public class Repo : IRepo
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        private readonly string _dataFilePath;

        public Repo()
        {
            _dataFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "data.json"
            );
        }

        public async Task<List<User>> GetUsers()
        {
            if (!File.Exists(_dataFilePath))
            {
                return [];
            }

            var existingJson = await File.ReadAllTextAsync(_dataFilePath);

            if (string.IsNullOrWhiteSpace(existingJson))
            {
                return [];
            }

            return JsonSerializer.Deserialize<List<User>>(existingJson) ?? [];
        }

        public async Task AddUser(User user)
        {
            var users = await GetUsers();
            users.Add(user);

            var updatedJson = JsonSerializer.Serialize(users, JsonOptions);
            await File.WriteAllTextAsync(_dataFilePath, updatedJson);
        }
    }
}
