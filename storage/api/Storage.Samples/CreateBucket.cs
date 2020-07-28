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

// [START storage_create_bucket]
// [START storage_quickstart]

using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using System;

public class CreateBucketSample
{
    /// <summary>
    /// Creates a storage bucket.
    /// </summary>
    /// <param name="projectId">The ID of the project to create the buckets in.</param>
    /// <param name="bucketName">The name of the bucket to create.</param>
    public Bucket CreateBucket(
        string projectId = "your-project-id",
        string bucketName = "your-unique-bucket-name")
    {
        var storage = StorageClient.Create();
        var bucket = storage.CreateBucket(projectId, bucketName);
        Console.WriteLine($"Created {bucketName}.");
        return bucket;
    }
}
// [END storage_quickstart]
// [END storage_create_bucket]
