using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NetworkSetting.asset", menuName = "Network Setting")]
public class NetworkSetting : ScriptableObject
{
    public string FetchLeaderboardUrl;
    public string PostScoreUrl;
}
