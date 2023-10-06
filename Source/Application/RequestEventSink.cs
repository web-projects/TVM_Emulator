using System;

namespace TVMEmulator
{
    public delegate void OnRequestIdGenerated(string generatedRequestId);

    public class RequestEventSink
    {
        private static readonly RequestEventSink instance = new RequestEventSink();

        public static RequestEventSink Instance { get => instance; }

        public event OnRequestIdGenerated RequestIdGenerated;

        public string LastRequestId { get; private set; }

        private RequestEventSink()
        {
        }

        public string GenerateRequestId()
        {
            LastRequestId = Guid.NewGuid().ToString();
            RaiseOnGeneratedRequestId(LastRequestId);
            return LastRequestId;
        }

        private void RaiseOnGeneratedRequestId(string generatedRequestId)
        {
            RequestIdGenerated?.Invoke(generatedRequestId);
        }
    }
}
