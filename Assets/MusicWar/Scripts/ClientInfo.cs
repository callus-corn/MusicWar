using UnityEngine;
using UnityEngine.Networking;

public class ClientInfo
{
    public ClientMessage clientMessage;
    public NetworkConnection connection;

    public ClientInfo(ClientMessage c, NetworkConnection nc)
    {
        clientMessage = c;
        connection = nc;
    }
}
