using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

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
    [SerializeField] Collider2D myCollider;
    bool died;

    [Header("Verifications")]
    [SerializeField] Vector2 isInMap_offset;
    [SerializeField] Vector2 isInMap_size;
    [SerializeField] LayerMask mapLayer;
    [SerializeField] Vector2 touchingOtherBobj_offset;
    [SerializeField] Vector2 touchingOtherBobj_size;
    [SerializeField] LayerMask breakableLayer;
    [SerializeField] Vector2 touchingStructure_offset;
    [SerializeField] Vector2 touchingStructure_size;
    [SerializeField] LayerMask structureLayer;

    [Header("Events")]
    [SerializeField] UnityEvent whenSpawn;
    [SerializeField] UnityEvent whenTakeDamage;
    [SerializeField] UnityEvent whenDie;
    [SerializeField] UnityEvent whenRespawn;

    public UnityEvent WhenSpawn { get { return whenSpawn; } set { whenSpawn = value; }}
    public UnityEvent WhenTakeDamage { get { return whenTakeDamage; } set { whenTakeDamage = value; }}
    public UnityEvent WhenDie { get { return whenDie; } set { whenDie = value; }}
    public UnityEvent WhenRespawn { get { return whenRespawn; } set { whenRespawn = value; }}

    [Header("LifeBar")]
    [SerializeField] GameObject lifeBar;
    public GameObject LifeBar { get { return lifeBar; }}
    [SerializeField] Image lifeBar_bar;
    public Image LifeBar_bar { get { return lifeBar_bar; }}
    [SerializeField] Gradient lifeBar_gradient;
    public Gradient LifeBar_gradient { get { return lifeBar_gradient; }}

    [Header("Drop")]
    [SerializeField] DropSystem myDropSystem;
    public DropSystem MyDropSystem { get { return myDropSystem; } set { }}

    [Header("TextInScreen")]
    [SerializeField] UI_TextInScreen textInScreenPrefab;

    public void Awake()
    {
        maxLife = life;
    }

    public void Start()
    {
        if(whenSpawn != null) { whenSpawn.Invoke(); }
    }

    public void Break(Hec_Stats hec_stats)
    {
        float globalChance = Random.Range(0f,1f);
        if (!died)
        {
            if(life > 0)
            {
                float realDamage = hec_stats.GetACharacterMultValue(0, hec_stats.characterSelectedId) * hec_stats.All_damageMult;
                realDamage += PassiveDamage(hec_stats);
                if(globalChance <= hec_stats.GetACharacterMultValue(3, hec_stats.characterSelectedId) * hec_stats.All_criticalChanceMult) 
                {
                    float criticalDamage = 2 * hec_stats.All_criticalDamageMult * hec_stats.GetACharacterMultValue(2, hec_stats.characterSelectedId);
                    realDamage = hec_stats.GetACharacterMultValue(0, hec_stats.characterSelectedId) * hec_stats.All_damageMult * criticalDamage;
                    
                    UI_TextInScreen criticalShow = Instantiate(textInScreenPrefab);
                    criticalShow.transform.position = transform.position;
                    criticalShow.SetTxt("Critical!", Color.red);
                    criticalShow.MyAnim.SetTrigger("CriticalHit");
                }
                UI_TextInScreen realDamageShow = Instantiate(textInScreenPrefab);
                realDamageShow.transform.position = transform.position;
                realDamageShow.SetTxt(EventsManager.eventM.UpdateVariables(realDamage), Color.white);
                realDamageShow.MyAnim.SetTrigger("NormalHit");

                life -= realDamage;

                

                if(whenTakeDamage != null) { whenTakeDamage.Invoke(); }

                if(life <= 0)
                {
                    if(myDropSystem.DropSettings.Length > 0)
                    {
                        CallDrop(hec_stats);
                    }
                    Died();
                }
            }
        }
    }

    public virtual float PassiveDamage(Hec_Stats stats)
    {
        return 0;
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
        myCollider.enabled = false;
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
        myDropSystem.Drop(hec_stats, 0, 1);
    }

    public virtual void UpdateMyLifeBar()
    {
        if(Life >= maxLife) { lifeBar.SetActive(false); }
        else if(Life <= 0) { lifeBar.SetActive(false); }
        else { lifeBar.SetActive(true); }
        EventsManager.eventM.UpdateBar(Life, maxLife, lifeBar_bar, lifeBar_gradient);
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

    public bool AmIinMap()
    {
        Collider2D verify = Physics2D.OverlapBox
        (new Vector2(transform.position.x + isInMap_offset.x, transform.position.y + isInMap_offset.y), 
        isInMap_size * transform.localScale.x, 0, mapLayer);

        return verify;
    }

    public bool TouchingAnotherBreakableObject()
    {
        Collider2D verify = Physics2D.OverlapBox
        (new Vector2(transform.position.x + touchingOtherBobj_offset.x, transform.position.y + touchingOtherBobj_offset.y), 
        touchingOtherBobj_size * transform.localScale.x, 0, breakableLayer);

        return verify;
    }
    public bool TouchingStructure()
    {
        Collider2D verify = Physics2D.OverlapBox
        (new Vector2(transform.position.x + touchingStructure_offset.x, transform.position.y + touchingStructure_offset.y), 
        touchingStructure_size * transform.localScale.x, 0, structureLayer);

        return verify;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + isInMap_offset.x, transform.position.y + isInMap_offset.y), isInMap_size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + touchingOtherBobj_offset.x, transform.position.y + touchingOtherBobj_offset.y), touchingOtherBobj_size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + touchingStructure_offset.x, transform.position.y + touchingStructure_offset.y), touchingStructure_size);
    }
}