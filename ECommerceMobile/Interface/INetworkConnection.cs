namespace ECommerceMobile.Interface
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();

    }
}
