using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Reflection;

namespace Gtest.Common.DataForTest;

public class VirtualDatabase : IDisposable
{
	private readonly SqliteConnection _connection;

	public VirtualDatabase(string csvResourceName = "Gtest.Common.DataForTest.Data.gwpByCountry.csv", string tableName = "gwpByCountry")
	{
		Batteries_V2.Init();
		_connection = new SqliteConnection("Data Source=:memory:");
		_connection.Open();

		Assembly assembly = Assembly.GetExecutingAssembly(); // Load CSV from embedded resource
		using Stream? stream = assembly.GetManifestResourceStream(csvResourceName);
		if (stream == null)
			throw new FileNotFoundException($"Resource '{csvResourceName}' not found.");

		using StreamReader reader = new(stream);
		string? headerLine = reader.ReadLine();
		if (headerLine == null)
			throw new InvalidDataException("CSV file is empty.");

		string[] columns = headerLine.Split(',');
		string createTableSql = $"CREATE TABLE [{tableName}] ({string.Join(", ", columns.Select(c => $"[{c}] TEXT"))});";
		using (SqliteCommand cmd = _connection.CreateCommand())
		{
			cmd.CommandText = createTableSql;
			cmd.ExecuteNonQuery();
		}

		string? line;
		while ((line = reader.ReadLine()) != null)
		{
			string[] values = line.Split(',');
			string insertSql = $"INSERT INTO [{tableName}] ({string.Join(", ", columns.Select(c => $"[{c}]"))}) VALUES ({string.Join(", ", columns.Select((_, i) => $"@p{i}"))});";
			using SqliteCommand insertCmd = _connection.CreateCommand();
			insertCmd.CommandText = insertSql;
			for (int i = 0; i < columns.Length; i++)
			{
				insertCmd.Parameters.AddWithValue($"@p{i}", values.Length > i ? values[i] : DBNull.Value);
			}
			insertCmd.ExecuteNonQuery();
		}
	}

	public SqliteConnection Connection => _connection;

	public void Dispose()
	{
		_connection?.Dispose();
	}
}