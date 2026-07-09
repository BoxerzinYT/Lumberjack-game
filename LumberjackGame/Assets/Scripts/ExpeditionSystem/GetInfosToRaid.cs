using UnityEngine;

public class GetInfosToRaid : MonoBehaviour
{
    [SerializeField] int mapId;
    [SerializeField] int difficultId;
    [SerializeField] Vector2 comissionNpc;
    public int DifficultId { get { return difficultId; } set { difficultId = value; } }
    public int MapId { get { return mapId; } set { mapId = value; } }
    public Vector2 ComissionNpc { get { return comissionNpc; } set { comissionNpc = value; } }

    public GetInfosToRaid(int _mapId, int _difficultId, Transform _comissionNpc)
    {
        //comissionNpc = _comissionNpc.position;
        mapId = _mapId;
        difficultId = _difficultId;
    }
}
