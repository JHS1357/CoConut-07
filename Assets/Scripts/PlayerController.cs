using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private WeaponInvenCtrl index;

    MinimapIcon myMinimapicon = null;
    [Header("Move")]
    public float moveSpeed = 4.0f;
    public float SmoothMoveSpeed = 5.0f;

    public AudioClip clip;

    public GameObject[] weapons;
    public bool[] hasWeapons;
    public GameObject[] grenades;
    public int hasGrenades;
    public GameObject grenadeObject;

    public int maxHasGrenades;

    public Weapon equipWeapon;
    public int equipWeaponIndexNum;
    float fireDelay;


    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    bool gDown;
    bool isSwap;
    bool fDown;
    bool isFireReady;

    Rigidbody rigid;
    void Update()
    {
        GetInput();
        Grenade();

        //switch (MyWeapon)
        //{
        //    case WeaponType.Melee:
        //        JoyStick_Double.SetActive(false);
        //        JoyStick_Single.SetActive(true);
        //        break;

        //    case WeaponType.Range:
        //        JoyStick_Double.SetActive(true);
        //        JoyStick_Single.SetActive(false);
        //        break;
        //}

    }
    void GetInput()
    {
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        sDown4 = Input.GetButtonDown("Swap4");
        gDown = Input.GetButtonDown("Fire2");

    }

    public enum WeaponType { Melee, Range };

    [Header("Weapon")]
    public WeaponType MyWeapon;
    //public bool joyStickType = true;    //기본 무기에 따라?
    [Tooltip("양손 조작")]
    public GameObject JoyStick_Double;
    [Tooltip("한손 조작")]
    public GameObject JoyStick_Single;

    Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        GameObject obj = Instantiate(Resources.Load("Prefabs/MiniMapIcon") as GameObject);
        myMinimapicon = obj.GetComponent<MinimapIcon>();
        myMinimapicon.Initalize(this.transform);
        myMinimapicon.myColor = Color.blue;

        Init();
    }
    //void Start()
    //{


    //}

    void Init()
    {
        MyWeapon = WeaponType.Melee;
        JoyStick_Double.SetActive(false);
        JoyStick_Single.SetActive(true);
    }
    // Update is called once per frame

    void Grenade()
    {
        if (hasGrenades == 0)
            return;

        if (gDown)
        {
            Vector3 nextVec = Vector3.forward;
            nextVec.y = 5;
            nextVec.z = 15;

            GameObject instantGrenade = Instantiate(grenadeObject, transform.position, transform.rotation);
            Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
            rigidGrenade.AddForce(nextVec, ForceMode.Impulse);
            rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

            hasGrenades--;
            grenades[hasGrenades].SetActive(false);
        }
    }
    public void Attack()
    {
        equipWeapon.Use();

        switch (equipWeapon.type)
        {
            case Weapon.Type.Melee:
                anim.SetTrigger("doSwing");
                Debug.Log("doSwing");
                break;

            case Weapon.Type.Range:
                anim.SetTrigger("doShot");
                Debug.Log("doShot");
                break;

            case Weapon.Type.Hammer:
                anim.SetTrigger("doStun");
                Debug.Log("doStun");
                break;

            case Weapon.Type.Bomb:
                anim.SetTrigger("doThrow");
                Debug.Log("doThrow");
                break;
        }

        SoundManager.instance.SFXPlay("Attack", clip);

    }
    public void Swap()
    {
        int weaponIndex = 0;
        equipWeaponIndexNum = index.weaponIndex;
        weaponIndex = equipWeaponIndexNum;

        if ((true))
        {
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    public void SwapOut()
    {
        isSwap = false;
    }
    public void FreezeRotatoin()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void FixedUpdate()
    {
        FreezeRotatoin();
    }
}
