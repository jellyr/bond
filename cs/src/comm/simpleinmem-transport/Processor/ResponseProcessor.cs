﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Bond.Comm.SimpleInMem.Processor
{
    using System;
    using System.Threading.Tasks;

    internal class ResponseProcessor : QueueProcessor
    {
        private InMemFrameQueue m_clientreqresqueue;
        private SimpleInMemConnection m_connection;

        internal ResponseProcessor(SimpleInMemConnection connection, InMemFrameQueue queue)
        {
            if (queue == null) throw new ArgumentNullException(nameof(queue));
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            m_connection = connection;
            m_clientreqresqueue = queue;
        }

        override internal void Process()
        {
            const PayloadType payloadType = PayloadType.Response;
            
            if (m_clientreqresqueue.Count(payloadType) == 0)
            {
                return;
            }

            var frame = m_clientreqresqueue.Dequeue(payloadType);
            var headers = frame.m_headers;
            var message = frame.m_message;
            var taskSource = frame.m_outstandingRequest;

            DispatchResponse(headers, message, taskSource);
        }

        private void DispatchResponse(SimpleInMemHeaders headers, IMessage message, TaskCompletionSource<IMessage> responseCompletionSource)
        {
            responseCompletionSource.SetResult(message);
        }
    }
}