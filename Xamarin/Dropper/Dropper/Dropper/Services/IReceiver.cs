using System;

namespace Dropper.Services
{
    public interface IReceiver : IDisposable
    {
        void Initialize(Credentials credentials);
        void StartListener();
        void StopListener();
    }
}
