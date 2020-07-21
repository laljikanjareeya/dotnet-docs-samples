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

// [START spanner_query_data]

using Google.Cloud.Spanner.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class QuerySampleDataAsyncSample
{
    public async Task<List<Album>> QuerySampleDataAsync(string projectId, string instanceId, string databaseId)
    {
        string connectionString = $"Data Source=projects/{projectId}/instances/{instanceId}/databases/{databaseId}";
        // Create connection to Cloud Spanner.
        var albums = new List<Album>();
        using (var connection = new SpannerConnection(connectionString))
        {
            var cmd = connection.CreateSelectCommand("SELECT SingerId, AlbumId, AlbumTitle FROM Albums");
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var singerId = reader.GetFieldValue<int>("SingerId");
                    var albumId = reader.GetFieldValue<int>("AlbumId");
                    var albumTitle = reader.GetFieldValue<string>("AlbumTitle");
                    Console.WriteLine($"SingerId : { singerId} AlbumId : {albumId} AlbumTitle : {albumTitle}");
                    albums.Add(new Album
                    {
                        AlbumId = albumId,
                        SingerId = singerId,
                        AlbumTitle = albumTitle
                    });
                }
            }
            return albums;
        }
    }
}
// [END spanner_query_data]