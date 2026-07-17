using UnityEngine;

public class Expedition_PowersManager : MonoBehaviour
{
    GlobalExpeditionManager myExpeditionManager;
    [SerializeField] Expedition_PowerDifficultSettings[] powers;
    Global_SpawnTrees global_spawnTrees;

    public void Start()
    {
        myExpeditionManager = GetComponent<GlobalExpeditionManager>();
        global_spawnTrees = GetComponent<Global_SpawnTrees>();

        EnableAllInteractionsWithPowers();
    }

    public void EnableAllInteractionsWithPowers()
    {
        TreePower_bombArea.explodedPlayer += () => myExpeditionManager.stunnedTimes++;
    }

    public void DisableAllInteractionsWithPowers()
    {
        TreePower_bombArea.explodedPlayer -= () => myExpeditionManager.stunnedTimes++;
    }

    public void SpawnBomb(BreakableObj obj)
    {
        float chance = Random.Range(0f,1f);
        foreach(var p in powers[myExpeditionManager.getExpeditionInfo.DifficultId].powers)
        {
            if(chance <= p.chanceToSpawn)
            {
                myExpeditionManager.allStunsSituations++;
                TreePower_Bomb newBomb = Instantiate(p.powerPrefab);
                newBomb.transform.position = obj.transform.position;
            }
        }
    }

    public void FinishSettingsOnTheTrees()
    {
        foreach(var t in global_spawnTrees.TreesSaved)
        {
            t.WhenDie.AddListener(() => SpawnBomb(t));
        }
    }
}
[System.Serializable]
public sealed class ExpeditionPowersSettings
{
    public TreePower_Bomb powerPrefab;
    public float chanceToSpawn;
}
[System.Serializable]
public sealed class Expedition_PowerDifficultSettings
{
    public string difficultName;
    public ExpeditionPowersSettings[] powers;
}
