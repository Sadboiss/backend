using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middleware
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketServerConnectionManager _manager;

        public WebSocketServerMiddleware(RequestDelegate next, WebSocketServerConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();
                Console.WriteLine("WebSocket connected");
                
                string connId = _manager.AddSocket(socket);
                
                await SendConnIdAsync(socket, connId);
                
                await ReceiveMessage(socket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                        Console.WriteLine($"Message: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");

                    if (result.MessageType == WebSocketMessageType.Close)
                        Console.WriteLine("Received Closed message");
                });
            }
            else
            {
                Console.WriteLine("Hello from the 2nd request delegate");
                await _next(context);
            }
        }

        private async Task SendConnIdAsync(WebSocket socket, string connId)
        {
            var buffer = Encoding.UTF8.GetBytes("ConnID: " + connId);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        
        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new Byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                    cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        } 
    }
}