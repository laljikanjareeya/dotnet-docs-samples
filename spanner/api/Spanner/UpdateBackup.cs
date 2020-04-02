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
using Google.Protobuf.WellKnownTypes;
using log4net;
using System;
using static GoogleCloudSamples.Spanner.Program;

namespace GoogleCloudSamples.Spanner
{
    public class UpdateBackup
    {
        static readonly ILog s_logger = LogManager.GetLogger(typeof(UpdateBackup));

        // [START spanner_update_backup]
        public static object SpannerUpdateBackup(string projectId, string instanceId, string backupId)
        {
            // Create the DatabaseAdminClient instance.
            DatabaseAdminClient databaseAdminClient = DatabaseAdminClient.Create();

            // Retrieve existing backup.
            string backupName = BackupName.Format(projectId, instanceId, backupId);
            Backup backup = databaseAdminClient.GetBackup(backupName);

            // Add 30 days to the existing ExpireTime.
            backup.ExpireTime = backup.ExpireTime.ToDateTime().AddDays(30).ToTimestamp();

            UpdateBackupRequest backupUpdateRequest = new UpdateBackupRequest
            {
                UpdateMask = new FieldMask()
                {
                    Paths =
                    {
                        "expire_time"
                    }
                },
                Backup = backup
            };

            // Make the UpdateBackup requests.
            var updatedBackup = databaseAdminClient.UpdateBackup(backupUpdateRequest);

            s_logger.Info("Backup Updated successfully.");
            s_logger.Info($"Updated Backup ExireTime: {updatedBackup.ExpireTime}");

            return ExitCode.Success;
        }
        // [END spanner_update_backup]
    }
}