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

// [START spanner_query_with_int_parameter]

using Google.Cloud.Spanner.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class QueryWithIntAsyncSample
{
    public async Task<List<Venue>> QueryWithIntAsync(string projectId, string instanceId, string databaseId)
    {
        string connectionString = $"Data Source=projects/{projectId}/instances/{instanceId}/databases/{databaseId}";
        // Create a Int64 object to use for querying.
        Int64 exampleInt = 3000;
        // Create connection to Cloud Spanner.
        using (var connection = new SpannerConnection(connectionString))
        {
            var venues = new List<Venue>();
            var cmd = connection.CreateSelectCommand("SELECT VenueId, VenueName, Capacity FROM Venues WHERE Capacity >= @ExampleInt");
            cmd.Parameters.Add("ExampleInt", SpannerDbType.Int64, exampleInt);
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var venueId = reader.GetFieldValue<int>("VenueId");
                    var venueName = reader.GetFieldValue<string>("VenueName");
                    var capacity = reader.GetFieldValue<int>("Capacity");
                    Console.WriteLine($"VenueId: {venueId} VenueName: {venueName} Capacity:{capacity}");
                    venues.Add(new Venue
                    {
                        VenueId = venueId,
                        VenueName = venueName,
                        Capacity = capacity
                    });
                }
            }
            return venues;
        }
    }
}
// [END spanner_query_with_int_parameter]