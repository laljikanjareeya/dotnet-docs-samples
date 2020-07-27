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

// [START storage_disable_default_event_based_hold]

using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using System;

public class DisableDefaultEventBasedHoldSample
{
    /// <summary>
    /// Disables a bucket's default event based hold.
    /// </summary>
    /// <param name="bucketName">The name of the bucket.</param>
    public Bucket DisableDefaultEventBasedHold(string bucketName = "your-unique-bucket-name")
    {
        var storage = StorageClient.Create();
        var bucket = storage.GetBucket(bucketName);
        bucket.DefaultEventBasedHold = false;
        bucket = storage.UpdateBucket(bucket, new UpdateBucketOptions()
        {
            // Use IfMetagenerationMatch to avoid race conditions.
            IfMetagenerationMatch = bucket.Metageneration
        });
        Console.WriteLine($"Default event-based hold was disabled for {bucketName}");
        return bucket;
    }
}
// [END storage_disable_default_event_based_hold]
