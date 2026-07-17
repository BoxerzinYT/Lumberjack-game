using System;
using UnityEngine;

public class Struc_GetInfoToExpedition : MonoBehaviour
{
    [SerializeField] int biomeId;
    [SerializeField] int difficultId;
    [SerializeField] string expeditionSceneName;
    [SerializeField] Hec_Stats playerInfos;
    public int DifficultId { get { return difficultId; } set { difficultId = value; } }
    public int BiomeId { get { return biomeId; } set { biomeId = value; } }
    public string ExpeditionSceneName { get { return expeditionSceneName; } set { expeditionSceneName = value; } }
    public Hec_Stats PlayerInfos { get { return playerInfos; } set { playerInfos = value; } }
}
