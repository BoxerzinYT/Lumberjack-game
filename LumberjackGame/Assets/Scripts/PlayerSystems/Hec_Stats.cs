using UnityEngine;

public class Hec_Stats : MonoBehaviour
{
    Hec_UIManager myUiManager;
    void Start() { myUiManager = GetComponent<Hec_UIManager>(); }

    [Header("CoinsStats")]
    public float hec_coins;
    public int hec_level;
    public float hec_xp;
    public float hec_xpForNextLevel;
    
    int mapleTree_saplings_prot;
    public int MapleTree_saplings_prot { get { return mapleTree_saplings_prot; } set
        {
            mapleTree_saplings_prot = value;
            if(mapleTree_saplings_prot < 0)
            {
                mapleTree_saplings_prot = 0;
            }

            myUiManager.CallSaplingsChange();
        }
    }
    int mapleBiome_expansionScrolls_prot;
    public int MapleBiome_expansionScrolls_prot { get { return mapleBiome_expansionScrolls_prot; } set
        {
            mapleBiome_expansionScrolls_prot = value;
            if(mapleBiome_expansionScrolls_prot < 0)
            {
                mapleBiome_expansionScrolls_prot = 0;
            }

            myUiManager.CallSaplingsChange();
        }
    }

    [Header("AttributeStats")]
    public float hec_damage;
    public float hec_range;
    [SerializeField] int hec_capacity;
    public int Hec_Capacity { get { return hec_capacity;} set {
        hec_capacity = value; 
        if(hec_capacity > hec_MaxCapacity)
        {
            hec_capacity = hec_MaxCapacity;
        }
        else if(hec_capacity < 0)
        {
            hec_capacity = 0;
        }
        myUiManager.CallUpdateBar();
        }}
    [SerializeField] int hec_MaxCapacity;
    public int Hec_MaxCapacity { get { return hec_MaxCapacity; } set { hec_MaxCapacity = value; }}
    public float hec_criticalChance;
    public float hec_bonusChance;
    public float hec_speed;
    public float hec_luck;
    public int hec_dropPoints;
}
