﻿/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Google.Cloud.Compute.V1;
using System.Threading.Tasks;
using Xunit;

namespace Compute.Samples.Tests
{
    [Collection(nameof(ComputeFixture))]
    public class DisableUsageExportBucketAsyncTest
    {
        private readonly ComputeFixture _fixture;
        private readonly DisableUsageExportBucketAsyncSample _sample = new DisableUsageExportBucketAsyncSample();

        public DisableUsageExportBucketAsyncTest(ComputeFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task DisablesUsageExportBucket()
        {
            var project = await _fixture.ProjectsClient.GetAsync(_fixture.ProjectId);
            var oldUsageExportLocation = project.UsageExportLocation;

            await _sample.DisableUsageExportBucketAsync(_fixture.ProjectId);

            project = await _fixture.ProjectsClient.GetAsync(_fixture.ProjectId);
            var newUsageExportLocation = project.UsageExportLocation;

            Assert.Null(newUsageExportLocation);

            // As cleanup we just restore whatever config we had before.
            // Strictly speaking we should use a separate project for this test, etc. But let's leave this
            // as is for now, until we really run into issues.
            var restoreRequest = new SetUsageExportBucketProjectRequest
            {
                Project = _fixture.ProjectId,
                UsageExportLocationResource = oldUsageExportLocation ?? new UsageExportLocation(),
            };

            // We don't poll, not much we can do if this fails.
            await _fixture.ProjectsClient.SetUsageExportBucketAsync(restoreRequest);
        }
    }
}
