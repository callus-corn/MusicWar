using System.Collections.Generic;
using UnityEngine.Networking;
using UniRx;

public class MatchingMessage : MessageBase
{
    public static int playableCount = 4;
    public string[] users = new string[4] { "", "","","" };
    public int playerCount = 0;
}
