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

    public void SpawnAIsland(int cordX, int cordY, int biomeId)
    {
        //Spawnando a parte e definindo suas coordenadas e posição 
        Map_PartManager newPart = Instantiate(partPrefab);
        newPart.SetPart(this);
        newPart.SetMyPos(cordX, cordY, DistanceBeetweenCenters);
        partsInGame.Add(newPart);
        newPart.name = "MapPart(" + (partsInGame.Count - 2) + ")";
    }

    public IEnumerator FirstSpawnIslandParts()
    {
        int[] coordsCal = new int[3];

        partsInGame[0].SetPart(this);
        partsInGame[0].SetMyPos(0, 0, DistanceBeetweenCenters);

        yield return new WaitForSeconds(0.01f);

        for(int i=0; i<firstSpawnQuant;)
        {
            coordsCal = CalculateEmptyCoordenate();
            if(coordsCal[2] == 1)
            {
                while(coordsCal[2] == 1)
                {
                    coordsCal = CalculateEmptyCoordenate();
                    yield return new WaitForSeconds(0.01f);
                }
            }
            //Spawnando a parte e definindo suas coordenadas e posição 
            Map_PartManager newPart = Instantiate(partPrefab);
            newPart.SetPart(this);
            newPart.SetMyPos(coordsCal[0], coordsCal[1], DistanceBeetweenCenters);

            //Adicionando a lista de partes
            partsInGame.Add(newPart);
            newPart.name = "MapPart(" + (partsInGame.Count - 2) + ")";
            i++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public int[] CalculateEmptyCoordenate()
    {
        int tempCoordX = 0;
        int tempCoordY = 0;
        int randomEdge = Random.Range(0, partsInGame[partsInGame.Count - 1].Edges.Length);
        //Debug.Log(randomEdge);
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

    //Retorna o valor que será alterado na coordenada baseado no id do expand edge
    public int[] ReturnTheCordChangeBasedOnTheEdgeId(int edgeValue)
    {
        if(edgeValue == 0) //Left
        {
            return new int[] { 1, 0 };
        }
        else if(edgeValue == 1) //Up
        {
            return new int[] { 0, 1 };
        }
        else if(edgeValue == 2) //Right
        {
            return new int[] { -1, 0 };
        }
        else if(edgeValue == 3) //Down
        {
            return new int[] { 0, -1 };
        }

        return new int[] { 0, 0 };
    }
}
