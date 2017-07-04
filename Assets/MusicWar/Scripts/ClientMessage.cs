using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientMessage : MessageBase
{
    public string id = "";
    public string modelName = "";
    public string weponName = "";
    public int team = -1;
}
