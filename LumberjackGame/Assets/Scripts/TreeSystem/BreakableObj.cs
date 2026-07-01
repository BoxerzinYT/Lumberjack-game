using System.Collections;
using UnityEngine;

public class BreakableObj : MonoBehaviour, IBreakable
{
    [SerializeField] float life;
    public float Life { get { return life; } set { life = value; }}
    float maxLife;

    [SerializeField] bool hasRespawn;
    [SerializeField] float timeToRespawn;
    bool died;

    public void Start()
    {
        maxLife = life;
    }

    public void Break(Breakable_Interactions inter)
    {
        float globalChance = Random.Range(0f,1f);
        if (!died)
        {
            if(life > 0)
            {
                float realDamage = (globalChance <= inter.criticalChance) ? inter.damage * 2 : inter.damage;
                life -= realDamage;
                Debug.Log(realDamage);
            }
            if(life <= 0)
            {
                Debug.Log("died");
                Died();
            }
        }
    }

    public void Died()
    {
        died = true;
        if (!hasRespawn)
        {
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(CooldownForReturn());
        }
    }
    IEnumerator CooldownForReturn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        died = false;
        Debug.Log("respawned!");
        StopCoroutine("CooldownForReturn");
    }
}