using SocketIO;
using PublisherKit;

namespace SocketIOManager
{

    public class SocketManager : Publisher
    {

        SocketIOComponent socket;

        void Awake()
        {
            InitSocket();
        }

        void InitSocket()
        {
            socket = GetComponent<SocketIOComponent>();
            socket.On("open", OnConnectionOpen);
            socket.On("error", OnConnectionError);
            socket.On("close", OnConnectionClose);
        }

        public void Emit(string tag, JSONObject data = null)
        {
            if (data) socket.Emit(tag, data);
            else socket.Emit(tag);
        }

        public void On(string tag, System.Action<SocketIOEvent> method)
        {
            socket.On(tag, method);
        }

        public void OnConnectionOpen(SocketIOEvent e)
        {
            Broadcast("OnConnectionOpen");
        }

        public void OnConnectionClose(SocketIOEvent e)
        {
            Broadcast("OnConnectionClose");
        }

        public void OnConnectionError(SocketIOEvent e)
        {
            Broadcast("OnConnectionError");
        }

    }

}