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

using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using System;
using static Google.Apis.Storage.v1.Data.Bucket;

namespace Storage
{
    public class SetRetentionPolicy
    {
        // [START storage_set_retention_policy]
        public static RetentionPolicyData SetBucketRetentionPolicy(string bucketName,
            long retentionPeriod)
        {
            var storage = StorageClient.Create();
            var bucket = storage.GetBucket(bucketName);
            bucket.RetentionPolicy = new Bucket.RetentionPolicyData();
            bucket.RetentionPolicy.RetentionPeriod = retentionPeriod;
            bucket = storage.UpdateBucket(bucket, new UpdateBucketOptions()
            {
                IfMetagenerationMatch = bucket.Metageneration
            });

            Console.WriteLine($"Retention policy for {bucketName} was set to {retentionPeriod}");
            return bucket.RetentionPolicy;
        }
        // [END storage_set_retention_policy]
    }
}
