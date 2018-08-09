﻿using System;
using System.Collections.Generic;
using System.Threading;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Contexts;
using ExecutionContext = FunctionMonkey.Abstractions.Contexts.ExecutionContext;

namespace FunctionMonkey.Infrastructure
{
    class ContextManager : IContextSetter, IContextProvider
    {
        private static readonly AsyncLocal<ServiceBusContext> ServiceBusContextLocal = new AsyncLocal<ServiceBusContext>();

        private static readonly AsyncLocal<StorageQueueContext> StorageQueueContextLocal = new AsyncLocal<StorageQueueContext>();

        private static readonly AsyncLocal<BlobContext> BlobContextLocal = new AsyncLocal<BlobContext>();

        private static readonly AsyncLocal<EventHubContext> EventHubContextLocal = new AsyncLocal<EventHubContext>();

        private static readonly AsyncLocal<ExecutionContext> ExecutionContextLocal = new AsyncLocal<ExecutionContext>();

        void IContextSetter.SetServiceBusContext(int deliveryCount, DateTime enqueuedTimeUtc, string messageId)
        {
            ServiceBusContextLocal.Value = new ServiceBusContext
            {
                DeliveryCount = deliveryCount,
                EnqueuedTimeUTc = enqueuedTimeUtc,
                MessageId = messageId
            };
        }

        void IContextSetter.SetStorageQueueContext(DateTimeOffset expirationTime, DateTimeOffset insertionTime, DateTimeOffset nextVisibleTime,
            string queueTrigger, string id, string popReceipt, int dequeueCount)
        {
            StorageQueueContextLocal.Value = new StorageQueueContext
            {
                DequeueCount = dequeueCount,
                ExpirationTime = expirationTime,
                Id = id,
                InsertionTime = insertionTime,
                NextVisibleTime = nextVisibleTime,
                PopReceipt = popReceipt,
                QueueTrigger = queueTrigger
            };
        }

        void IContextSetter.SetBlobContext(string blobTrigger, Uri uri, IDictionary<string, string> metadata)
        {
            BlobContextLocal.Value = new BlobContext
            {
                BlobTrigger = blobTrigger,
                Uri = uri,
                Metadata = metadata
            };
        }

        void IContextSetter.SetEventHubContext(DateTime enqueuedTimeUtc, long sequenceNumber, string offset)
        {
            EventHubContextLocal.Value = new EventHubContext
            {
                EnqueuedTimeUtc = enqueuedTimeUtc,
                Offset = offset,
                SequenceNumber = sequenceNumber
            };
        }

        void IContextSetter.SetExecutionContext(string functionAppDirectory, string functionDirectory, string functionName, Guid invocationId)
        {
            ExecutionContextLocal.Value = new ExecutionContext
            {
                FunctionDirectory = functionDirectory,
                FunctionAppDirectory = functionAppDirectory,
                FunctionName = functionName,
                InvocationId = invocationId
            };
        }

        public ServiceBusContext ServiceBusContext => ServiceBusContextLocal.Value;
        public StorageQueueContext StorageQueueContext => StorageQueueContextLocal.Value;
        public BlobContext BlobContext => BlobContextLocal.Value;
        public EventHubContext EventHubContext => EventHubContextLocal.Value;
        public ExecutionContext ExecutionContext => ExecutionContextLocal.Value;
    }
}