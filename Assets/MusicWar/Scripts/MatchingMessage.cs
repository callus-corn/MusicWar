using System.Collections.Generic;
using UnityEngine.Networking;
using UniRx;

public class MatchingMessage : MessageBase
{
    public static int playableCount = 2;
    public string[] users = new string[2] { "", "" };
    public int playerCount = 0;
}
