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

using Google.Cloud.Spanner.Admin.Database.V1;
using Google.Cloud.Spanner.Common.V1;
using Google.LongRunning;
using Google.Protobuf.WellKnownTypes;
using log4net;
using System;
using static GoogleCloudSamples.Spanner.Program;

namespace GoogleCloudSamples.Spanner
{
    public class CreateBackup
    {
        static readonly ILog s_logger = LogManager.GetLogger(typeof(CreateBackup));

        // [START spanner_create_backup]
        public static object SpannerCreateBackup(
            string projectId, string instanceId, string databaseId, string backupId)
        {
            // Create the DatabaseAdminClient instance.
            DatabaseAdminClient databaseAdminClient = DatabaseAdminClient.Create();

            // Initialize request parameters.
            Backup backup = new Backup
            {
                Database = DatabaseName.Format(projectId, instanceId, databaseId),
                ExpireTime = DateTime.UtcNow.AddDays(14).ToTimestamp()
            };
            string parent = InstanceName.Format(projectId, instanceId);

            // Make the CreateBackup request.
            Operation<Backup, CreateBackupMetadata> response =
                databaseAdminClient.CreateBackup(parent, backup, backupId);

            s_logger.Info("Waiting for the operation to finish.");

            // Poll until the returned long-running operation is complete.
            Operation<Backup, CreateBackupMetadata> completedResponse =
                response.PollUntilCompleted();

            if (completedResponse.IsFaulted)
            {
                s_logger.Error($"Error while creating backup: {completedResponse.Exception}");
                return ExitCode.InvalidParameter;
            }

            s_logger.Info($"Backup created successfully.");

            // GetBackup to get more information about the created backup.
            backup = databaseAdminClient.GetBackup(BackupName.Format(projectId, instanceId, backupId));
            s_logger.Info($"Backup {backup.Name} of size {backup.SizeBytes} bytes " +
                          $"was created at {backup.CreateTime} from {backup.Database} " +
                          $"and is in state {backup.State}");

            return ExitCode.Success;
        }
        // [END spanner_create_backup]
    }
}