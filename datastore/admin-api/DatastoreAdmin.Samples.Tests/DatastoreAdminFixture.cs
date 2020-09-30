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

using Google.Cloud.Datastore.V1;
using System;
using Xunit;

[CollectionDefinition(nameof(DatastoreAdminFixture))]
public class DatastoreAdminFixture : ICollectionFixture<DatastoreAdminFixture>, IDisposable
{
    public string ProjectId { get; } = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
    public string BucketName { get; } = "grass-clump-479-node-perf-test"; //Environment.GetEnvironmentVariable("CLOUD_STORAGE_BUCKET");
    public string Namespace { get; } = Guid.NewGuid().ToString();
    public string Kind { get; } = "datastore-admin-test-kind";

    public DatastoreAdminFixture()
    {
        CreateKeyFactory();
    }

    void CreateKeyFactory()
    {
        DatastoreDb datastoreDb = DatastoreDb.Create(ProjectId, Namespace);
        KeyFactory keyFactory = datastoreDb.CreateKeyFactory(Kind);
        Key key = keyFactory.CreateKey("sampletask");
        var task = new Entity
        {
            Key = key,
            ["description"] = "Buy milk"
        };
        datastoreDb.Insert(task);
    }

    public void Dispose()
    {
        DatastoreDb datastoreDb = DatastoreDb.Create(ProjectId, Namespace);
        var deadEntities = datastoreDb.RunQuery(new Query(Kind));
        datastoreDb.Delete(deadEntities.Entities);
    }
}