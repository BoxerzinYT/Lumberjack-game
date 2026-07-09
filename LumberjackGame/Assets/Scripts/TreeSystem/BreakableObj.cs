using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DropSystem))]
public class BreakableObj : MonoBehaviour, IBreakable
{
    [Header("MainSettings")]
    [SerializeField] float life;
    public float Life { get { return life; } set { life = value; }}
    float maxLife;

    [SerializeField] bool hasRespawn;
    [SerializeField] float timeToDestroy;
    [SerializeField] float timeToRespawn;
    bool died;

    [Header("Events")]
    [SerializeField] UnityEvent whenDie;
    [SerializeField] UnityEvent whenTakeDamage;
    [SerializeField] UnityEvent whenRespawn;

    public UnityEvent WhenDie { get { return whenDie; } set { whenDie = value; }}
    public UnityEvent WhenTakeDamage { get { return whenTakeDamage; } set { whenTakeDamage = value; }}
    public UnityEvent WhenRespawn { get { return whenRespawn; } set { whenRespawn = value; }}

    [Header("Drop")]
    [SerializeField] DropSystem myDropSystem;
    public DropSystem MyDropSystem { get { return myDropSystem; } set { }}

    public void Awake()
    {
        maxLife = life;
    }

    public void Break(Hec_Stats hec_stats)
    {
        float globalChance = Random.Range(0f,1f);
        if (!died)
        {
            if(life > 0)
            {
                float realDamage = hec_stats.hec_damage;
                if(globalChance <= hec_stats.hec_criticalChance) { realDamage = hec_stats.hec_damage * 2; }
                life -= realDamage;

                if(whenTakeDamage != null) { whenTakeDamage.Invoke(); }

                if(life <= 0)
                {
                    CallDrop(hec_stats);
                    Died();
                }
            }
        }
    }

    public void Died()
    {
        if(whenDie != null) { whenDie.Invoke(); }
        died = true;
        if (!hasRespawn)
        {
            StartCoroutine(CooldownForDie());
        }
        else
        {
            StartCoroutine(CooldownForReturn());
        }
    }
    IEnumerator CooldownForDie()
    {
        died = false;
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
        StopCoroutine("CooldownForDie");
    }
    IEnumerator CooldownForReturn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        life = maxLife;
        died = false;
        //whenDie.RemoveAllListeners();
        if(whenRespawn != null) { whenRespawn.Invoke(); }
        StopCoroutine("CooldownForReturn");
    }

    public virtual void CallDrop(Hec_Stats hec_stats)
    {
        myDropSystem.Drop(hec_stats, 0);
    }

    public void ChangeLife(float newValue)
    {
        life = newValue;
        if(life >= maxLife)
        {
            life = maxLife;
        }
    }
    public void ChangeMaxLife(float newValue, bool canOverHealth)
    {
        maxLife = newValue;
        if (canOverHealth)
        {
            life = maxLife;
        }
    }
}