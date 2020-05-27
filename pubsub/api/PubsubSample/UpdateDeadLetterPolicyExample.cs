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

// [START pubsub_dead_letter_update_subscription]

using Google.Cloud.PubSub.V1;
using Google.Protobuf.WellKnownTypes;

public class UpdateDeadLetterPolicySample
{
    public Subscription UpdateDeadLetterPolicy(string projectId, string topicId,
        string subscriptionId, string deadLetterTopicId)
    {
        SubscriberServiceApiClient subscriber = SubscriberServiceApiClient.Create();
        TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
        SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(projectId,
            subscriptionId);

        var subscription = new Subscription()
        {
            SubscriptionName = subscriptionName,
            TopicAsTopicName = topicName,
            DeadLetterPolicy = new DeadLetterPolicy
            {
                DeadLetterTopic = TopicName.FromProjectTopic(projectId, deadLetterTopicId).ToString(),
                MaxDeliveryAttempts = 15
            }
        };

        var request = new UpdateSubscriptionRequest
        {
            Subscription = subscription,
            UpdateMask = new FieldMask { Paths = { "deadLetterPolicy" } }
        };
        var updatedSubscription = subscriber.UpdateSubscription(request);
        return updatedSubscription;
    }
}
// [END pubsub_dead_letter_update_subscription]
