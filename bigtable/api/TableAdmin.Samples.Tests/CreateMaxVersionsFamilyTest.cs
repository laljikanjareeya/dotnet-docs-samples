﻿// Copyright (c) 2020 Google LLC.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.

using Google.Cloud.Bigtable.Admin.V2;
using Xunit;

[Collection(nameof(BigtableTableAdminFixture))]
public class CreateMaxVersionsFamilyTest
{
    private readonly BigtableTableAdminFixture _fixture;

    public CreateMaxVersionsFamilyTest(BigtableTableAdminFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestCreateMaxVersionsFamily()
    {
        CreateMaxVersionsFamilySample createMaxVersionsFamilySample = new CreateMaxVersionsFamilySample();
        DeleteFamilySample deleteFamilySample = new DeleteFamilySample();
        var table = createMaxVersionsFamilySample.CreateMaxVersionsFamily(_fixture.ProjectId, _fixture.InstanceId, _fixture.TableId);

        Assert.Contains(table.ColumnFamilies, c => c.Value.GcRule.RuleCase == GcRule.RuleOneofCase.MaxNumVersions && c.Value.GcRule.MaxNumVersions != 0);
        deleteFamilySample.DeleteFamily(_fixture.ProjectId, _fixture.InstanceId, _fixture.TableId, "cf2");
    }
}