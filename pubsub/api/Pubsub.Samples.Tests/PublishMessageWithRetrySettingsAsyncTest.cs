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

[Collection(nameof(PubsubFixture))]
public class PublishMessageWithRetrySettingsAsyncTest
{
    private readonly PubsubFixture _pubsubFixture;
    private readonly PublishMessageWithRetrySettingsAsyncSample _publishMessageWithRetrySettingsAsyncSample;
    private readonly PullMessagesAsyncSample _pullMessagesAsyncSample;

    public PublishMessageWithRetrySettingsAsyncTest(PubsubFixture pubsubFixture)
    {
        _pubsubFixture = pubsubFixture;
        _publishMessageWithRetrySettingsAsyncSample = new PublishMessageWithRetrySettingsAsyncSample();
        _pullMessagesAsyncSample = new PullMessagesAsyncSample();
    }

    [Fact]
    public async void PublishMessageWithRetrySettingsAsync()
    {
        string randomName = _pubsubFixture.RandomName();
        string topicId = $"testTopicForMessageWithRetrySettingsAsync{randomName}";
        string subscriptionId = $"testSubscriptionForMessageWithRetrySettingsAsync{randomName}";

        _pubsubFixture.CreateTopic(topicId);
        _pubsubFixture.CreateSubscription(topicId, subscriptionId);

        var output = await _publishMessageWithRetrySettingsAsyncSample
            .PublishMessageWithRetrySettingsAsync(_pubsubFixture.ProjectId, topicId, "Hello World!");

        Assert.Equal(1, output);

        // Pull the Message to confirm it is valid
        var result = await _pullMessagesAsyncSample.PullMessagesAsync(_pubsubFixture.ProjectId, subscriptionId, false);
        Assert.Equal(1, result);
    }
}
