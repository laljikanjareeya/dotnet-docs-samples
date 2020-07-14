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

// [START spanner_dml_getting_started_update]

using Google.Cloud.Spanner.Data;
using System;
using System.Threading.Tasks;

public class WriteWithTransactionUsingDmlCoreAsyncSample
{
    public async Task WriteWithTransactionUsingDmlCoreAsync(string projectId, string instanceId, string databaseId)
    {
        // This sample transfers 200,000 from the MarketingBudget
        // field of the second Album to the first Album. Make sure to run
        // the addColumn and writeDataToNewColumn samples first,
        // in that order.
        string connectionString = $"Data Source=projects/{projectId}/instances/{instanceId}/databases/{databaseId}";

        decimal transferAmount = 200000;
        decimal secondBudget = 0;
        decimal firstBudget = 0;

        // Create connection to Cloud Spanner.
        using (var connection = new SpannerConnection(connectionString))
        {
            await connection.OpenAsync();

            // Create a readwrite transaction that we'll assign
            // to each SpannerCommand.
            using (var transaction = await connection.BeginTransactionAsync())
            {
                // Create statement to select the second album's data.
                var cmdLookup = connection.CreateSelectCommand("SELECT * FROM Albums WHERE SingerId = 2 AND AlbumId = 2");
                cmdLookup.Transaction = transaction;
                // Execute the select query.
                using (var reader = await cmdLookup.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Read the second album's budget.
                        secondBudget = reader.GetFieldValue<decimal>("MarketingBudget");
                        // Confirm second Album's budget is sufficient and
                        // if not raise an exception. Raising an exception
                        // will automatically roll back the transaction.
                        if (secondBudget < transferAmount)
                        {
                            throw new Exception($"The first album's budget {secondBudget} is less than the amount to transfer.");
                        }
                    }
                }
                // Read the first album's budget.
                cmdLookup = connection.CreateSelectCommand("SELECT * FROM Albums WHERE SingerId = 1 and AlbumId = 1");
                cmdLookup.Transaction = transaction;
                using (var reader = await cmdLookup.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        firstBudget = reader.GetFieldValue<decimal>("MarketingBudget");
                    }
                }

                // Update second album to remove the transfer amount.
                secondBudget -= transferAmount;
                SpannerCommand cmd = connection.CreateDmlCommand("UPDATE Albums SET MarketingBudget = @MarketingBudget  WHERE SingerId = 2 and AlbumId = 2");
                cmd.Parameters.Add("MarketingBudget", SpannerDbType.Int64, secondBudget);
                cmd.Transaction = transaction;
                await cmd.ExecuteNonQueryAsync();

                // Update first album to add the transfer amount.
                firstBudget += transferAmount;
                cmd = connection.CreateDmlCommand("UPDATE Albums SET MarketingBudget = @MarketingBudget WHERE SingerId = 1 and AlbumId = 1");
                cmd.Parameters.Add("MarketingBudget", SpannerDbType.Int64, firstBudget);
                cmd.Transaction = transaction;
                await cmd.ExecuteNonQueryAsync();

                await transaction.CommitAsync();
            }
            Console.WriteLine("Transaction complete.");
        }
    }
}
// [END spanner_dml_getting_started_update]
