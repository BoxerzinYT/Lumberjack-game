using UnityEngine;

public class Map_PartManager : MonoBehaviour
{
    [Header("SpawnSystem")]
    //Right, Up, Left, Down
    [SerializeField] GameObject[] edges;
    public GameObject[] Edges { get { return edges; }}
    int my_Coords_x;
    public int MyCoordsX { get { return my_Coords_x; }}
    int my_Coords_y;
    public int MyCoordsY { get { return my_Coords_y; }}
    float my_distanceConstant; //Guarda a distancia entre o centro de cada ilha (tanto no x quanto no y)

    [Header("SpawnTreesSystem")]
    [SerializeField] int quantOfTreesThatSpawnWithThePart;
    [SerializeField] Struc_SaplingSlot[] mySaplingsSlot;
    [SerializeField] Breakable_Tree myTree;

    public void Start()
    {
        SpawnTrees();
    }

    public void SetMyPos(int _coords_x, int _coords_y, float _distanceConstant)
    {
        my_Coords_x = _coords_x;
        my_Coords_y = _coords_y;
        my_distanceConstant = _distanceConstant;
        transform.position = new Vector2(my_distanceConstant * my_Coords_x, my_distanceConstant * my_Coords_y);
    }

    public void SpawnTrees()
    {
        for(int i=0; i<Random.Range(1, quantOfTreesThatSpawnWithThePart); i++)
        {
            int randomSlot = Random.Range(0, mySaplingsSlot.Length);
            Breakable_Tree newTree = Instantiate(myTree);
            newTree.transform.position = mySaplingsSlot[randomSlot].transform.position;
            Destroy(mySaplingsSlot[randomSlot].gameObject);
            newTree.ChangePhase(1);
        }
    }
}
