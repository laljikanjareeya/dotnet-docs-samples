﻿// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Cloud.Redis.V1;
using System;
using Xunit;

[Collection(nameof(RedisFixture))]
public class CreateInstanceTest
{
    private readonly RedisFixture _fixture;
    public CreateInstanceTest(RedisFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void CreateInstance()
    {
        var instanceId = $"csharp-{Guid.NewGuid().ToString().Substring(0, 20)}";
        CreateInstanceSample createInstanceSample = new CreateInstanceSample();
        //run the sample code.
        Instance instance = createInstanceSample.CreateInstance(_fixture.ProjectId, _fixture.LocationId, instanceId);
        _fixture.MarkForDeletion(instanceId);
        Assert.Equal(Instance.Types.State.Ready, instance.State);
    }
}
