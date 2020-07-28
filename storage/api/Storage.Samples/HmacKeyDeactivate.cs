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

// [START storage_deactivate_hmac_key]

using Google.Cloud.Storage.V1;
using System;

public class HmacKeyDeactivateSample
{
    /// <summary>
    /// Deactivates an HMAC key.
    /// </summary>
    /// <param name="projectId">The project containing the HMAC key.</param>
    /// <param name="accessId">The ID of the HMAC key.</param>
    public void DeactivateHmacKey(
        string projectId = "your-project-id",
        string accessId = "your-access-id")
    {
        var storage = StorageClient.Create();
        var metadata = storage.GetHmacKey(projectId, accessId);
        metadata.State = HmacKeyStates.Inactive;
        var updatedMetadata = storage.UpdateHmacKey(metadata);

        Console.WriteLine("The HMAC key is now inactive.");
        Console.WriteLine("The HMAC key metadata is:");
        Console.WriteLine($"ID: {updatedMetadata.Id}");
        Console.WriteLine($"Access ID: {updatedMetadata.AccessId}");
        Console.WriteLine($"Project ID: {updatedMetadata.ProjectId}");
        Console.WriteLine($"Service Account Email: {updatedMetadata.ServiceAccountEmail}");
        Console.WriteLine($"State: {updatedMetadata.State}");
        Console.WriteLine($"Time Created: {updatedMetadata.TimeCreated}");
        Console.WriteLine($"Time Updated: {updatedMetadata.Updated}");
        Console.WriteLine($"ETag: {updatedMetadata.ETag}");
    }
}
// [END storage_deactivate_hmac_key]
