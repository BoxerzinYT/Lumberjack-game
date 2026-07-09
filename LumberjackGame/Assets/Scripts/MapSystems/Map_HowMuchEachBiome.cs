using UnityEngine;

public class Map_HowMuchEachBiome : MonoBehaviour
{
    [SerializeField] int quantOf_maple;
    public int QuantOf_maple { get { return quantOf_maple; } }
    [SerializeField] int quantOf_forest;
    public int QuantOf_forest { get { return quantOf_forest; } }
    [SerializeField] int quantOf_snow;
    public int QuantOf_snow { get { return quantOf_snow; } }

    public void SetQuantOf(int id, int quant)
    {
        if(id == 0)
        {
            quantOf_maple += quant;
        }
        else if(id == 1)
        {
            quantOf_forest += quant;
        }
        else if(id == 2)
        {
            quantOf_snow += quant;
        }
    }

    public int GetBiomeId(int id)
    {
        if(id == 0)
        {
            return quantOf_maple;
        }
        else if(id == 1)
        {
            return quantOf_forest;
        }
        else if(id == 2)
        {
            return quantOf_snow;
        }

        return -1;
    }
}
