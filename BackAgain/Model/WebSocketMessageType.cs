using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Model
{
    public enum WebSocketMessageType
    {
        ConnectionID,

        NewTerminalConnection,

        TerminalDisconnected,

        PosReconnected,

        POSNotConnected,

        SettingsUpdated,

        MenuUpdated,
        
        NewOrderPlaced,

        NewItemAddedToOrder,

        CommentAdded,

        OrderConfirmed,

        OrderComplete,

        OrderCancelled,
    }
}
