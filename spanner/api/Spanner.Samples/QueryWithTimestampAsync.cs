﻿// Copyright 2020 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START spanner_query_with_timestamp_parameter]

using Google.Cloud.Spanner.Data;
using System;
using System.Threading.Tasks;

public class QueryWithTimestampAsyncSample
{
    public async Task QueryWithTimestampAsync(string projectId, string instanceId, string databaseId)
    {
        string connectionString = $"Data Source=projects/{projectId}/instances/{instanceId}/databases/{databaseId}";
        // Create a DateTime timestamp object to use for querying.
        DateTime exampleTimestamp = DateTime.Now;
        // Create connection to Cloud Spanner.
        using (var connection = new SpannerConnection(connectionString))
        {
            var cmd = connection.CreateSelectCommand("SELECT VenueId, VenueName, LastUpdateTime FROM Venues WHERE LastUpdateTime < @ExampleTimestamp");
            cmd.Parameters.Add("ExampleTimestamp", SpannerDbType.Timestamp, exampleTimestamp);
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    string timestamp = string.Empty;
                    if (reader["LastUpdateTime"] != DBNull.Value)
                    {
                        timestamp = reader.GetFieldValue<string>("LastUpdateTime");
                    }
                    Console.WriteLine("VenueId : " + reader.GetFieldValue<string>("VenueId")
                    + " VenueName : " + reader.GetFieldValue<string>("VenueName")
                    + $" LastUpdateTime : {timestamp}");
                }
            }
        }
    }
}
// [END spanner_query_with_timestamp_parameter]
