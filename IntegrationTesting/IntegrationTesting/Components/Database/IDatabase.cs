using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace IntegrationTesting.Components.Database
{
    public interface IDatabase
    {
        Task<NpgsqlDataReader> Query(string query, Dictionary<string, string> parameters = null);
        Task Insert(string command, Dictionary<string, string> parameters = null);
    }
}