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

// [START storage_download_encrypted_file]

using Google.Cloud.Storage.V1;
using System;
using System.IO;

public class DownloadEncryptedFileSample
{
    /// <summary>
    /// Downloads file with encryption.
    /// </summary>
    /// <param name="key">The encryption key to use for this operation. 
    /// If this property is null, the Google.Cloud.Storage.V1.StorageClient.EncryptionKey will be used instead</param>
    /// <param name="bucketName">The name of the bucket.</param>
    /// <param name="objectName">The name of the object within the bucket.</param>
    /// <param name="localPath">Local path to write the data into.</param>
    public void DownloadEncryptedFile(string key = "3eFsTXPvqi3BpT2ipFCGirslh1Jgc6ikjoAu2oQ5JcI=", string bucketName = "your-unique-bucket-name", string objectName = "my-file-name", string localPath = null)
    {
        var storage = StorageClient.Create();
        localPath = localPath ?? Path.GetFileName(objectName);
        using (var outputFile = File.OpenWrite(localPath))
        {
            storage.DownloadObject(bucketName, objectName, outputFile, new DownloadObjectOptions()
            {
                EncryptionKey = EncryptionKey.Create(Convert.FromBase64String(key))
            });
        }
        Console.WriteLine($"Downloaded {objectName} to {localPath}.");
    }
}
// [END storage_download_encrypted_file]
