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

// [START storage_list_files]

using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;

public class ListFilesSample
{
    /// <summary>
    /// Lists the objects in a given bucket.
    /// </summary>
    /// <param name="bucketName">The bucket to list the objects from.</param>
    public IEnumerable<Google.Apis.Storage.v1.Data.Object> ListFiles(
        string bucketName = "your-unique-bucket-name")
    {
        var storage = StorageClient.Create();
        var storageObjects = storage.ListObjects(bucketName, "");
        Console.WriteLine($"Files in bucket {bucketName}:");
        foreach (var storageObject in storageObjects)
        {
            Console.WriteLine(storageObject.Name);
        }

        return storageObjects;
    }
}
// [END storage_list_files]
