﻿using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace WebApi.Middleware
{
    public class WebSocketServerConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public ConcurrentDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

        public string AddSocket(WebSocket socket)
        {
            string connId = Guid.NewGuid().ToString();
            _sockets.TryAdd(connId, socket);
            Console.WriteLine("Connection added: " + connId);
            return connId;
        }
    }
}