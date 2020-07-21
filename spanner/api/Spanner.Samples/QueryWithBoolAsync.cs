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

// [START spanner_query_with_bool_parameter]

using Google.Cloud.Spanner.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class QueryWithBoolAsyncSample
{
    public async Task<List<Venue>> QueryWithBoolAsync(string projectId, string instanceId, string databaseId)
    {
        string connectionString = $"Data Source=projects/{projectId}/instances/{instanceId}/databases/{databaseId}";
        var venues = new List<Venue>();
        // Create a Boolean to use for querying.
        bool exampleBool = true;
        // Create connection to Cloud Spanner.
        using (var connection = new SpannerConnection(connectionString))
        {
            var cmd = connection.CreateSelectCommand(
                "SELECT VenueId, VenueName, OutdoorVenue FROM Venues "
                + "WHERE OutdoorVenue = @ExampleBool");
            cmd.Parameters.Add("ExampleBool", SpannerDbType.Bool, exampleBool);
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var venueId = reader.GetFieldValue<int>("VenueId");
                    var venueName = reader.GetFieldValue<string>("VenueName");
                    var outdoorVenue = reader.GetFieldValue<bool>("OutdoorVenue");
                    Console.WriteLine($"VenueId: {venueId} VenueName:{venueName } OutdoorVenue:{outdoorVenue}");
                    venues.Add(new Venue
                    {
                        VenueId = venueId,
                        VenueName = venueName,
                        OutdoorVenue = outdoorVenue
                    });
                }
            }
        }
        return venues;
    }
}
// [END spanner_query_with_bool_parameter]