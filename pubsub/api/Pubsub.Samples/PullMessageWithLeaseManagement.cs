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

// [START pubsub_subscriber_sync_pull_with_lease_management]

using Google.Cloud.PubSub.V1;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class PullMessageWithLeaseManagementSample
{
    public int PullMessageWithLeaseManagement(string projectId, string subscriptionId, bool acknowledge)
    {
        SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(projectId, subscriptionId);
        SubscriberServiceApiClient subscriberClient = SubscriberServiceApiClient.Create();
        int messageCount = 0;
        try
        {
            // Pull messages from server,
            // allowing an immediate response if there are no messages.
            PullResponse response = subscriberClient.Pull(subscriptionName, returnImmediately: false, maxMessages: 20);

            var ackIds = new List<string>();
            // Print out each received message.
            foreach (ReceivedMessage msg in response.ReceivedMessages)
            {
                ackIds.Add(msg.AckId);
                string text = Encoding.UTF8.GetString(msg.Message.Data.ToArray());
                Console.WriteLine($"Message {msg.Message.MessageId}: {text}");
                Interlocked.Increment(ref messageCount);

                // Modify the ack deadline of each received message from the default 10 seconds to 30.
                // This prevents the server from redelivering the message after the default 10 seconds
                // have passed.
                subscriberClient.ModifyAckDeadline(subscriptionName, new List<string> { msg.AckId }, 30);
            }
            // If acknowledgement required, send to server.
            if (acknowledge && messageCount > 0)
            {
                subscriberClient.Acknowledge(subscriptionName, ackIds);
            }
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.Unavailable)
        {
            // UNAVAILABLE due to too many concurrent pull requests pending for the given subscription.
        }
        return messageCount;
    }
}
// [END pubsub_subscriber_sync_pull_with_lease_management]
