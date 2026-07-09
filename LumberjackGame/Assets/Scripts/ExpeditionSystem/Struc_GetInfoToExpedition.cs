using UnityEngine;

public class Struc_GetInfoToExpedition : MonoBehaviour
{
    [SerializeField] int biomeId;
    [SerializeField] int difficultId;
    public int DifficultId { get { return difficultId; } set { difficultId = value; } }
    public int BiomeId { get { return biomeId; } set { biomeId = value; } }
}
