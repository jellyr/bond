// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Bond.Comm
{
    /// <summary>
    /// Interface for layer stacks. The layer stack encapsulates
    /// a list of layers which work together over the same layer data.
    /// </summary>
    public interface ILayerStack
    {
        /// <summary>
        /// Take action on sending of a message, by invoking a stack of layers (in forward order).
        /// </summary>
        /// <param name="messageType">The type of message</param>
        /// <param name="context">The send context. May not be null.</param>
        /// <param name="layerData">The layer data for this layer, provided as an instance in an <see cref="IBonded"/> ready to be serialized</param>
        /// <returns>An error, or null if there is no error.</returns>
        /// <remarks>
        /// In general, the layer is not expected to interact with the message payload itself.
        /// </remarks>
        Error OnSend(MessageType messageType, SendContext context, out IBonded layerData);

        /// <summary>
        /// Take action on receipt of a message, by invoking a stack of layers (in reverse order).
        /// </summary>
        /// <param name="messageType">The type of message</param>
        /// <param name="context">The receive context. May not be null.</param>
        /// <param name="layerData">The layer data for this layer, provided as an <see cref="IBonded"/> to be deserialized</param>
        /// <returns>An error, or null if there is no error.</returns>
        /// <remarks>
        /// In general, the layer is not expected to interact with the message payload itself.
        /// </remarks>
        Error OnReceive(MessageType messageType, ReceiveContext context, IBonded layerData);
    }
}
