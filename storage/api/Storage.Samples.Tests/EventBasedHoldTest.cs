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
public class EventBasedHoldTest
{
    private readonly BucketFixture _bucketFixture;

    public EventBasedHoldTest(BucketFixture bucketFixture)
    {
        _bucketFixture = bucketFixture;
    }

    [Fact]
    public void EventBasedHold()
    {
        ReleaseEventBasedHoldSample releaseEventBasedHoldSample = new ReleaseEventBasedHoldSample();
        SetEventBasedHoldSample setEventBasedHoldSample = new SetEventBasedHoldSample();
        UploadFileSample uploadFileSample = new UploadFileSample();
        GetMetadataSample getMetadataSample = new GetMetadataSample();
        uploadFileSample.UploadFile(_bucketFixture.BucketNameGeneric, _bucketFixture.FilePath, _bucketFixture.Collect("EventBasedHold.txt"));

        setEventBasedHoldSample.SetEventBasedHold(_bucketFixture.BucketNameGeneric, "EventBasedHold.txt");
        var metadata = getMetadataSample.GetMetadata(_bucketFixture.BucketNameGeneric, "EventBasedHold.txt");
        Assert.True(metadata.EventBasedHold);

        releaseEventBasedHoldSample.ReleaseEventBasedHold(_bucketFixture.BucketNameGeneric, "EventBasedHold.txt");
        metadata = getMetadataSample.GetMetadata(_bucketFixture.BucketNameGeneric, "EventBasedHold.txt");
        Assert.False(metadata.EventBasedHold);
    }
}
