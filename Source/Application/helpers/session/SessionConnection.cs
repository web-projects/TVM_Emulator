﻿using System.Threading.Tasks;
using TVMEmulator.emulation;

namespace TVMEmulator.helpers.session
{
    public delegate void SSEEventDelegate(string contents);
    public delegate void SSEDisconnectDelegate(string url);

    public class SessionConnection
    {
        private SSEEventDelegate _SSEEventFunction;
        private SSEDisconnectDelegate _SSEDisconnectFunction;
        private SSESubscriber _subscriber;

        private string _serverURL;

        public SessionConnection(string url, SSEEventDelegate sseEventFunction, SSEDisconnectDelegate sseDisconnectFunction)
        {
            _serverURL = url;
            _SSEEventFunction = sseEventFunction;
            _SSEDisconnectFunction = sseDisconnectFunction;
        }

        public string OnStart(string sessionID)
        {
            _subscriber = new SSESubscriber(_serverURL);
            _subscriber.OnDisconnect += () => _SSEDisconnectFunction?.Invoke(_serverURL);
            _subscriber.OnMessageReceived += (string serializedMessage) => _SSEEventFunction?.Invoke(serializedMessage);

            _subscriber.Initialize(sessionID);
            var task = Task.Run(async () =>
            {
                await _subscriber.Subscribe();
            });

            return _subscriber.SessionId;
        }
    }
}
