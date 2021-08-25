using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    float hAxis;
    float vAxis;

    bool wDown;
    bool fDown;
    bool jDown;

    bool isJump = false;
    bool isFireReady;

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    GameObject nearObject;
    Weapon equipWeapon;

    float fireDelay;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

    }
    // Start is called before the first frame update

    // Update is called once per frame
    //void Update()
    //{
    //    //GetInput();
    //    //Move();
    //    //Turn();
    //    Jump();

    //    Attack();
    //}

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
        //fDown = Input.GetButton("Fire1");  // 마우스 왼쪽 클릭시 일반 공격
    }
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;


            transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown )
        {
            rigid.AddForce(Vector3.up * 8, ForceMode.Impulse);
            anim.SetTrigger("doJump");
            isJump = true;

        }
    }
    public void Attack()
    {

        if (fDown && isFireReady)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
        }

    }
}
