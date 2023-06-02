using habitTracker;
using Microsoft.Data.Sqlite;
using System;

namespace HabitTracker
{
    internal class RecordsDB
    {
        private string? _connectionString = @"Data Source=habit-Tracker.db";
        private RunningRecord? _record;
        private List<RunningRecord>? _runningRecords;


        internal RecordsDB()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS RunningRecords
                                    (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Date TEXT,
                                        Duration INTEGER,
                                        Distance INTEGER
                                    )";
            command.ExecuteNonQuery();
        }

        internal void DeleteRecord(int id)
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM RunningRecords
                                    WHERE Id = $Id";
            command.Parameters.AddWithValue("$Id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal List<RunningRecord> GetAllRecords()
        {
            _runningRecords = new List<RunningRecord>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT Id, Date, Duration, Distance
                                    FROM RunningRecords";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                _record = new RunningRecord();
                _record.Id = reader.GetInt32(0);
                _record.Date = reader.GetString(1);
                _record.Duration = reader.GetInt32(2);
                _record.Distance = reader.GetInt32(3);
                _runningRecords.Add(_record);
            }
            connection.Close();
            return _runningRecords;
        }
        internal void AddRecord(RunningRecord record)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO RunningRecords
                                    (
                                        Date,
                                        Duration,
                                        Distance
                                    )
                                    VALUES
                                    (
                                        $Date,
                                        $Duration,
                                        $Distance
                                    )";
            command.Parameters.AddWithValue("$Date", record.Date);
            command.Parameters.AddWithValue("$Duration", record.Duration);
            command.Parameters.AddWithValue("$Distance", record.Distance);
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal RunningRecord GetRecordById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $@"SELECT Id, Date, Duration, Distance
                                    FROM RunningRecords
                                    WHERE Id = {id}";

            var reader = command.ExecuteReader();
            var record = new RunningRecord();
            while (reader.Read())
            {
                record.Id = reader.GetInt32(0);
                record.Date = reader.GetString(1);
                record.Duration = reader.GetInt32(2);
                record.Distance = reader.GetInt32(3);
            }
            connection.Close();
            return record;
        }

        public void UpdateRecord(RunningRecord record)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE RunningRecords
                                    SET
                                        Date = $Date,
                                        Duration = $Duration,
                                        Distance = $Distance
                                    WHERE Id = $Id";
            command.Parameters.AddWithValue("$Id", record.Id);
            command.Parameters.AddWithValue("$Date", record.Date);
            command.Parameters.AddWithValue("$Duration", record.Duration);
            command.Parameters.AddWithValue("$Distance", record.Distance);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
