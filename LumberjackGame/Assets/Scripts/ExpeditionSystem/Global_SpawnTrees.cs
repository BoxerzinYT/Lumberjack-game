using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Global_SpawnTrees : MonoBehaviour
{
    [SerializeField] UnityEvent modifySomethingInTrees;
    [SerializeField] List<Breakable_Tree> treesSaved = new List<Breakable_Tree>();
    public List<Breakable_Tree> TreesSaved { get { return treesSaved; }}

    Sappling _mySappling;
    Rank _myRank;

    public IEnumerator SpawnTrees(int quant, ChopDownTrees_TreesVariationsSettings[] trees, ChopDownTrees_TreesRankVariations[] treeRankVariation, string whereSpawn, Transform[] spawnSquares)
    {
        treesSaved.Clear();
        for(int i=0; i<quant; i++)
        {
            float treeVariationChance = Random.Range(0f,1f);
            foreach(var vc in trees)
            {
                if(treeVariationChance <= vc.chance)
                {
                    _mySappling = vc.tree;
                    float rankVariationChance = Random.Range(0f,1f);
                    foreach(var rv in treeRankVariation)
                    {
                        if(rankVariationChance <= rv.chance)
                        {
                            _myRank = rv.treeRank;
                            break;
                        }
                    }
                    break;
                }
            }

            Breakable_Tree newTree = Instantiate(_mySappling.myExpeditionTree);
            newTree.AddSappling(new InventoryItem(_mySappling, _myRank, 1));

            bool isInMap = false;
            bool touchingOtherBobj = true;
            bool touchingStructure = true;
            while (!isInMap || touchingOtherBobj || touchingStructure)
            {
                 newTree.transform.position = new Vector2
                (Random.Range(spawnSquares[0].position.x, spawnSquares[1].position.x),
                Random.Range(spawnSquares[0].position.y, spawnSquares[1].position.y));
                isInMap = newTree.AmIinMap();
                touchingOtherBobj = newTree.TouchingAnotherBreakableObject();
                touchingStructure = newTree.TouchingStructure();
                yield return new WaitForSeconds(0.01f);
            }
            newTree.ChangePhase(Random.Range(1, 2 + 1));
            SceneManager.MoveGameObjectToScene(newTree.gameObject, SceneManager.GetSceneByName(whereSpawn));
            treesSaved.Add(newTree);
            newTree.WhenDie.AddListener(() => treesSaved.Remove(newTree));
        }

        modifySomethingInTrees?.Invoke();

        StopCoroutine("SpawnTrees");
    }

    public IEnumerator ConfigureTheRestOfTheTree(Breakable_Tree newTree, string whereSpawn, Transform[] spawnSquares)
    {
        bool isInMap = false;
        bool touchingOtherBobj = true;
        bool touchingStructure = true;
        while (!isInMap || touchingOtherBobj || touchingStructure)
        {
            newTree.transform.position = new Vector2
            (Random.Range(spawnSquares[0].position.x, spawnSquares[1].position.x),
            Random.Range(spawnSquares[0].position.y, spawnSquares[1].position.y));
            isInMap = newTree.AmIinMap();
            touchingOtherBobj = newTree.TouchingAnotherBreakableObject();
            touchingStructure = newTree.TouchingStructure();
            yield return new WaitForSeconds(0.01f);
        }
        newTree.ChangePhase(Random.Range(1, 2 + 1));
        SceneManager.MoveGameObjectToScene(newTree.gameObject, SceneManager.GetSceneByName(whereSpawn));
        treesSaved.Add(newTree);
        newTree.WhenDie.AddListener(() => treesSaved.Remove(newTree));

        StopCoroutine("ConfigureTheRestOfTheTree");
    }

    public void ClearAllTreesSpawned()
    {
        foreach(var t in treesSaved)
        {
            Destroy(t.gameObject);
        }

        treesSaved.Clear();
    }
}
