using System.Collections;
using UnityEngine;

public class Map_PartManager : MonoBehaviour
{
    [SerializeField] bool centerIsland;
    Map_GlobalMapManager mapGlobalMan;
    [SerializeField] LayerMask mapLayer;

    [Header("SpawnSystem")]
    //Right, Up, Left, Down
    [SerializeField] ExpandEdges[] expandEdges;
    public ExpandEdges[] Edges { get { return expandEdges; }}
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
        if (centerIsland)
        {
            mapGlobalMan = FindFirstObjectByType<Map_GlobalMapManager>();
            SetMyExpandEdges();
        }
    }

    public void SetPart(Map_GlobalMapManager _mapGlobalMan)
    {
        mapGlobalMan = _mapGlobalMan;
    }

    public void SetMyPos(int _coords_x, int _coords_y, float _distanceConstant)
    {
        my_Coords_x = _coords_x;
        my_Coords_y = _coords_y;
        my_distanceConstant = _distanceConstant;
        transform.position = new Vector2(my_distanceConstant * my_Coords_x, my_distanceConstant * my_Coords_y);
        StartCoroutine(CallOtherPartsForUpdateTheirEdges());
    }

    public void SetMyExpandEdges()
    {
        for(int i=0; i < expandEdges.Length; i++)
        {
            expandEdges[i].expandEdge.MapGlobalMan = mapGlobalMan;
            if(i == 0) //Right
            {
                expandEdges[i].expandEdge.MyExpandCordX = MyCoordsX + 1;
                expandEdges[i].expandEdge.MyExpandCordY = MyCoordsY;
            }
            if(i == 1) //Up
            {
                expandEdges[i].expandEdge.MyExpandCordX = MyCoordsX;
                expandEdges[i].expandEdge.MyExpandCordY = MyCoordsY + 1;
            }
            if(i == 2) //Left
            {
                expandEdges[i].expandEdge.MyExpandCordX = MyCoordsX - 1;
                expandEdges[i].expandEdge.MyExpandCordY = MyCoordsY;
            }
            if(i == 3) //Down
            {
                expandEdges[i].expandEdge.MyExpandCordX = MyCoordsX;
                expandEdges[i].expandEdge.MyExpandCordY = MyCoordsY - 1;
            }
        }
    }
    
    public void UpdateEdge(int edgeId)
    {
        //if 0 == desable 2
        //if 1 == desable 3
        //if 2 == desable 0
        //if 3 == desable 1

        if(edgeId == 0)
        {
            expandEdges[2].expandEdge.gameObject.SetActive(false);
            expandEdges[2].expanded = true;
        }
        else if(edgeId == 1)
        {
            expandEdges[3].expandEdge.gameObject.SetActive(false);
            expandEdges[3].expanded = true;
        }
        else if(edgeId == 2)
        {
            expandEdges[0].expandEdge.gameObject.SetActive(false);
            expandEdges[0].expanded = true;
        }
        else if(edgeId == 3)
        {
            expandEdges[1].expandEdge.gameObject.SetActive(false);
            expandEdges[1].expanded = true;
        }
    }
    public IEnumerator CallOtherPartsForUpdateTheirEdges()
    {
        yield return new WaitForSeconds(0.01f);
        for(int i=0; i<4; i++)
        {
            Vector2 directionOfTheRay = new Vector2();
            Vector2 originOfTheRay = new Vector2();
            if(i == 0) { directionOfTheRay = Vector2.right;
            originOfTheRay = new Vector2(transform.position.x + 3, transform.position.y); } //Right
            else if(i == 1) { directionOfTheRay = Vector2.up;
            originOfTheRay = new Vector2(transform.position.x, transform.position.y + 3); } //Up
            else if(i == 2) { directionOfTheRay = Vector2.left;
            originOfTheRay = new Vector2(transform.position.x - 3, transform.position.y); } //Left
            else if(i == 3) { directionOfTheRay = Vector2.down;
            originOfTheRay = new Vector2(transform.position.x, transform.position.y - 3); } //Down
            RaycastHit2D hit = Physics2D.Raycast(originOfTheRay, directionOfTheRay, my_distanceConstant - 3, mapLayer);
            //Debug.DrawRay(originOfTheRay, directionOfTheRay * (my_distanceConstant - 3), Color.red);
            if (!hit)
            {
                continue;
            }
            else if(hit)
            {
                //Debug.Log("Hitted: " + hit.transform.gameObject.name + " from: " + gameObject.name + " with id of: " + i);
                expandEdges[i].expandEdge.gameObject.SetActive(false);
                expandEdges[i].expanded = true;
                hit.transform.GetComponent<Map_PartManager>().UpdateEdge(i);
            }
            yield return new WaitForSeconds(0.01f);
        }

        SetMyExpandEdges();
        StopCoroutine("CallOtherPartsForUpdateTheirEdges");
    }

    public void SpawnTrees()
    {
        for(int i=0; i<Random.Range(1, quantOfTreesThatSpawnWithThePart + 1); i++)
        {
            int randomSlot = Random.Range(0, mySaplingsSlot.Length);
            Breakable_Tree newTree = Instantiate(myTree);
            newTree.transform.position = mySaplingsSlot[randomSlot].transform.position;
            Destroy(mySaplingsSlot[randomSlot].gameObject);
            newTree.ChangePhase(1);
        }
    }
}
[System.Serializable]
public sealed class ExpandEdges
{
    public Acb_ExpandMap expandEdge;
    [Tooltip("0=Right, 1=Up, 2=Left, 3=Down")]
    public int whereIsTheExpand;
    public bool expanded;
    //Variavel responsavel por definir aonde que será a expansão que esse edge fará
}