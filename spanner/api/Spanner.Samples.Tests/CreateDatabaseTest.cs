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

using System;
using Xunit;

[Collection(nameof(SpannerFixture))]
public class CreateDatabaseTest
{
    private readonly SpannerFixture _spannerFixture;

    public CreateDatabaseTest(SpannerFixture spannerFixture)
    {
        _spannerFixture = spannerFixture;
    }

    [Fact]
    public async void TestCreateDatabase()
    {
        var databaseId = $"my-db-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        CreateDatabaseAsyncSample sample = new CreateDatabaseAsyncSample();
        await sample.CreateDatabaseAsyncAsync(_spannerFixture.ProjectId, _spannerFixture.InstanceId, databaseId);
        var databases = _spannerFixture.GetDatabases();
        Assert.Contains(databases, d => d.DatabaseName.DatabaseId == databaseId);
        _spannerFixture.DatabasesToDelete.Add(databaseId);
    }
}
