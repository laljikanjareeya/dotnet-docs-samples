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

using Xunit;

[Collection(nameof(BucketFixture))]
public class AddBucketDefaultOwnerTest
{
    private readonly BucketFixture _bucketFixture;

    public AddBucketDefaultOwnerTest(BucketFixture bucketFixture)
    {
        _bucketFixture = bucketFixture;
    }

    [Fact]
    public void TestAddBucketDefaultOwner()
    {
        var addBucketDefaultOwnerSample = new AddBucketDefaultOwnerSample();
        RemoveBucketDefaultOwnerSample removeBucketDefaultOwnerSample = new RemoveBucketDefaultOwnerSample();

        // Add bucket default owner.
        var updatedBucket = addBucketDefaultOwnerSample.AddBucketDefaultOwner(_bucketFixture.BucketNameGeneric, _bucketFixture.ServiceAccountEmail);
        Assert.Contains(updatedBucket.DefaultObjectAcl, acl => acl.Role == "OWNER" && acl.Email == _bucketFixture.ServiceAccountEmail);

        // Remove bucket default owner.
        removeBucketDefaultOwnerSample.RemoveBucketDefaultOwner(_bucketFixture.BucketNameGeneric, _bucketFixture.ServiceAccountEmail);
    }
}