using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_GlobalMapManager : MonoBehaviour
{
    [SerializeField] int firstSpawnQuant = 10;
    [SerializeField] Map_PartManager partPrefab;

    //Distancia entre o centro de cada ilha, tanto no x quanto no y
    [SerializeField] float DistanceBeetweenCenters;
    [SerializeField] List<Map_PartManager> partsInGame = new List<Map_PartManager>();

    public void Start()
    {
        if(partsInGame.Count <= 1)
        {
            StartCoroutine(FirstSpawnIslandParts());
        }
    }

    public IEnumerator FirstSpawnIslandParts()
    {
        int[] coordsCal = new int[3];
        for(int i=0; i<firstSpawnQuant;)
        {
            coordsCal = CalculateEmptyCoordenate();
            if(coordsCal[2] == 1)
            {
                while(coordsCal[2] == 1)
                {
                    coordsCal = CalculateEmptyCoordenate();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            //Spawnando a parte e definindo suas coordenadas e posição 
            Map_PartManager newPart = Instantiate(partPrefab);
            newPart.SetMyPos(coordsCal[0], coordsCal[1], DistanceBeetweenCenters);

            //Adicionando a lista de partes
            partsInGame.Add(newPart);
            i++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public int[] CalculateEmptyCoordenate()
    {
        int tempCoordX = 0;
        int tempCoordY = 0;
        int randomEdge = Random.Range(0, partsInGame[partsInGame.Count - 1].Edges.Length);
        Debug.Log(randomEdge);
        if(randomEdge == 0) //Left
        {
            tempCoordX = 1;
            tempCoordY = 0;
        }
        else if(randomEdge == 1) //Up
        {
            tempCoordX = 0;
            tempCoordY = 1;
        }
        else if(randomEdge == 2) //Right
        {
            tempCoordX = -1;
            tempCoordY = 0;
        }
        else if(randomEdge == 3) //Down
        {
            tempCoordX = 0;
            tempCoordY = -1;
        }
        if(!VerifyIfHasPartWithThisCoordenates(partsInGame[partsInGame.Count - 1].MyCoordsX + tempCoordX, partsInGame[partsInGame.Count - 1].MyCoordsY + tempCoordY))
        {
            return new int[] { partsInGame[partsInGame.Count - 1].MyCoordsX + tempCoordX
            ,partsInGame[partsInGame.Count - 1].MyCoordsY + tempCoordY, 0 };
        }
        else
        {
            return new int[] { 0,0, 1 };
        }
    }

    public bool VerifyIfHasPartWithThisCoordenates(int corX, int corY)
    {
        foreach(var p in partsInGame)
        {
            if(p.MyCoordsX == corX && p.MyCoordsY == corY)
            {
                return true;
            }
        }

        return false;
    }
}
