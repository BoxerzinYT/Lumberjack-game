using System.Collections;
using UnityEngine;

public class Hec_passive_dash : Power_PassiveManager
{
    [SerializeField] Hec_Stats hecStatus;
    [SerializeField] Characters_LevelManager levelMan;
    [SerializeField] float dashDistance;
    [SerializeField] RectTransform dashDistancePivot;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    //[SerializeField] int dashPoints;
    //[SerializeField] int maxDashPoints;
    /*
    public int DashPoints {
        set
        {
            dashPoints = value;
            if(dashPoints > maxDashPoints)
            {
                dashPoints = maxDashPoints;
            }
        }
        get { return dashPoints; }
    }
    */
    //[SerializeField] GameObject[] dashPointsUI;
    bool isDashing;
    bool CanDashAfterCooldown = true;
    bool globalCanDash = true;
    public bool IsDashing { get { return isDashing; }}
    public bool GlobalCanDash { get { return globalCanDash; } set { globalCanDash = value; }}
    Rigidbody2D rb;
    Animator anim;

    [Header("DashSettings")]
    [SerializeField] Transform dashPoint;
    [SerializeField] Transform dashAreaWrapper;
    [SerializeField] GameObject dashAreaObj;
    bool preparingDash;
    Vector2 mouseWorldPos;
    Vector2 whereDash;

    [Header("OrbSettings")]
    [SerializeField] float powerPointsToSpawn;
    [SerializeField] Transform[] spawnSquares;
    [SerializeField] BreakableObj speedOrb;
    [SerializeField] float orbDuration;

    [Header("MultSettings")]
    [SerializeField] MultiplicatorSettings[] passiveBuffs;
    Char_Hector myCharacterData;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashAreaObj.SetActive(false);

        LoadMyValues();
    }

    public void LoadMyValues()
    {
        if(hecStatus.charactersData[0].GetType() == typeof(Char_Hector))
        {
            myCharacterData = (Char_Hector)hecStatus.charactersData[0];
        }
        else { return; }

        dashDistance = myCharacterData.dash_distance + myCharacterData.passiveIncrementsPerLevel[0] * levelMan.hector_level;
        dashCooldown = myCharacterData.dash_Cooldown + myCharacterData.passiveIncrementsPerLevel[1] * levelMan.hector_level;
        powerPointsToSpawn = myCharacterData.dash_PowerToUsePassive + myCharacterData.passiveIncrementsPerLevel[2] * levelMan.hector_level;
        orbDuration = myCharacterData.dash_OrbDuration + myCharacterData.passiveIncrementsPerLevel[3] * levelMan.hector_level;
        passiveBuffs[0].add = myCharacterData.dash_damageUpMult + myCharacterData.passiveIncrementsPerLevel[4] * levelMan.hector_level;
        passiveBuffs[0].duration = myCharacterData.dash_damageUpDuration + myCharacterData.passiveIncrementsPerLevel[5] * levelMan.hector_level;
        passiveBuffs[1].add = myCharacterData.dash_speedUpMult + myCharacterData.passiveIncrementsPerLevel[6] * levelMan.hector_level;
        passiveBuffs[1].duration = myCharacterData.dash_speedUpDuration + myCharacterData.passiveIncrementsPerLevel[7] * levelMan.hector_level;

        Vector2 dashDistanceVector = new Vector2(dashDistance, dashDistancePivot.rect.height);
        dashDistancePivot.sizeDelta = dashDistanceVector;
    }

    public void Update()
    {
        if(!CanDashAfterCooldown || !globalCanDash) { return; }

        if(Input.GetKey(KeyCode.LeftShift) && !preparingDash)
        {
            preparingDash = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) && preparingDash && !isDashing)
        {
            whereDash = dashPoint.transform.position;
            dashAreaObj.SetActive(false);

            CanDashAfterCooldown = false;
            preparingDash = false;
            anim.SetTrigger("StartDash");
            anim.SetBool("Dash", true);

            if(PowerPoints >= powerPointsToSpawn)
            {
                CreateOrb();
                PowerPoints -= powerPointsToSpawn;
            }

            isDashing = true;
        }
        if (preparingDash)
        {
            dashAreaObj.SetActive(true);
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LookAtMouse();
        }
    }
    public void FixedUpdate()
    {
        if (isDashing)
        {
            Vector2 dir = whereDash - (Vector2)transform.position;
            rb.linearVelocity = dir * dashSpeed;
            if(((Vector2)transform.position - whereDash).magnitude <= 1f)
            {
                isDashing = false;
                rb.linearVelocity = new Vector2(0,0);
                EventsManager.eventM.playerCanInteract = true;
                anim.SetBool("Dash", false);
                whereDash = new Vector2(0,0);
                StartCoroutine(DashCooldown());
            }
        }
    }

    public void CreateOrb()
    {
        BreakableObj newOrb = Instantiate(speedOrb);
        bool isInMap = false;
        while (!isInMap)
        {
            newOrb.transform.position = new Vector2
            (UnityEngine.Random.Range(spawnSquares[0].position.x, spawnSquares[1].position.x),
            UnityEngine.Random.Range(spawnSquares[0].position.y, spawnSquares[1].position.y));
            isInMap = newOrb.AmIinMap();
        }
        newOrb.WhenDie.AddListener(() => ApplyBuffs());
        EventsManager.eventM.CreateTimer(orbDuration, () => DestroyOrb(newOrb) , true);
    }
    public void DestroyOrb(BreakableObj newOrb) { if(newOrb != null) { Destroy(newOrb.gameObject); } }

    public void ApplyBuffs()
    {
        foreach(var m in passiveBuffs)
        {
            HecStats.AddOrRemoveGeneralMult(1, m, m.add);
            EventsManager.eventM.CreateTimer(m.duration, () => HecStats.AddOrRemoveGeneralMult(-1, m, m.add), true);
        }
    }

    void LookAtMouse()
    {
        var dir = (mouseWorldPos - (Vector2)dashAreaWrapper.position).normalized;
        dashAreaWrapper.right = (Vector3)(dir * Mathf.Sign(transform.localScale.x));
        var eulerDir = dashAreaWrapper.localEulerAngles;
    }
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        CanDashAfterCooldown = true;

        StopCoroutine("DashCooldown");
    }

    /*
    IEnumerator Dashing()
    {
        dashPoints--;
        UpdateDashPointsUI();

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if(hor == 0 && ver == 0) { yield return null; StopCoroutine("Dashing"); }

        CanDashAfterCooldown = false;
        isDashing = true;
        EventsManager.eventM.playerCanInteract = false;

        Vector2 dir = dashPoint.position;
        
        rb.linearVelocity = dir * dashSpeed;
        anim.SetTrigger("StartDash");
        anim.SetBool("Dash", true);

        //yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.linearVelocity = new Vector2(0,0);
        EventsManager.eventM.playerCanInteract = true;
        anim.SetBool("Dash", false);

        yield return new WaitForSeconds(dashCooldown);

        CanDashAfterCooldown = true;

        StopCoroutine("Dashing");
    }

    public void AddDashPoints(int quant)
    {
        if(DashPoints < maxDashPoints)
        {
            DashPoints += quant;
        }
        UpdateDashPointsUI();
    }

    public void UpdateDashPointsUI()
    {
        foreach(var d in dashPointsUI) { d.SetActive(false); }
        for(int i=0; i<dashPoints; i++)
        {
            dashPointsUI[i].SetActive(true);
        }
    }
    */

    
}
