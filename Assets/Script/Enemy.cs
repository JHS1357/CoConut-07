using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rot_speed = 10.0f;
    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;


    private Transform target;
    private int wavepointIndex = 0;

    MinimapIcon myMinimapicon = null;
    Animator myAnimator = null;

    [Header("State")]
    public int HP = 100;
    public float move_speed = 3.0f;
    float init_move_speed;


    private void OnEnable()
    {
        HP = DataManager.instance.enemyHP[GameManager.instance.nCurWave];
        move_speed = DataManager.instance.enemySpeed[GameManager.instance.nCurWave];
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;

        init_move_speed = move_speed;

        this.transform.position = GameObject.Find("StartPos").transform.position;
        target = Waypoints.points[0];
        //github check

        myAnimator = GetComponentInChildren<Animator>();
        myAnimator.SetBool("Walk Forward", true);

        GameObject obj = Instantiate(Resources.Load("Prefabs/MiniMapIcon") as GameObject);
        myMinimapicon = obj.GetComponent<MinimapIcon>();
        myMinimapicon.Initalize(this.transform);
        myMinimapicon.myColor = Color.red;
    }

    private void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * move_speed * Time.deltaTime, Space.World); 

        this.transform.forward = Vector3.Lerp(this.transform.forward, dir, Time.deltaTime * rot_speed);

        // 상태이상 적용을 위해 추가한 코드
        //살아있을때만 체크해야 속도조절 가능.
        if(this.HP > 0)
            CheckStatus();

        if (Vector3.Distance(transform.position, target.transform.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            //Destroy(gameObject);
            //도달이 되면??????
            //

            this.gameObject.SetActive(false);
            GameManager.instance.nUsedMonsterCnt++;
            myMinimapicon.gameObject.SetActive(false);

            CoconutLife.instance.EnemyEntry();

            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    public void GetDamage(int dam)
    {
        HP -= dam;
        //후에 디펜스도 추가.
        if (HP <= 0)
        {
            //죽는 모션 이전에 변경할 것들
            GetComponentInChildren<Animator>().SetBool("Walk Forward", false);
            move_speed = 0.0f;
            myMinimapicon.gameObject.SetActive(false);
            StartCoroutine(EnemyDie());
            return;
        }
        else
            myAnimator.SetTrigger("Take Damage");
    }


    // TO-DO 슬로우 등 상태이상 적용 필요
    // 일단은 제가 혼자 실험하면서 구현했던 코드를 임시로 가져옵니다.
    // START
    public bool is_slow = false;
    float slow_power;
    public bool is_stun = false;
    public void ApplyStatus(int status_code, float duration, float power)
    {
        switch (status_code)
        {
            // 1 = slow
            case 1:
                is_slow = true;
                slow_power = power;
                left_slow = duration;
                break;

            // 2 = stun
            case 2:
                if(Random.Range(0.0f, 1.0f) < power)
                {
                    is_stun = true;
                    left_stun = duration;
                }
                break;
        default:
                break;
        }
        
    }
    float left_slow = 0.0f;
    float left_stun = 0.0f;
    void CheckStatus()
    {
        if (is_slow == true && init_move_speed == move_speed)
        {
            move_speed = init_move_speed * slow_power;
            return;
        }
        else if(is_stun == true && init_move_speed == move_speed)
        {
            move_speed = 0.0f;
        }

        left_slow -= Time.deltaTime;
        left_stun -= Time.deltaTime;

        if(left_slow <= 0.0f && left_stun <= 0.0f)
        {
            slow_power = 1.0f;
            move_speed = init_move_speed;
        }
    }
    // END

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            HP -= 100;
            Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamege(reactVec, false));
        }
        else if (other.tag == "Bullet")
        {
            HP -= 50;
            Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamege(reactVec, false));
        }
    }
    public void HitByRocket(Vector3 exlopsoinPos)
    {
        HP -= 200;
        Vector3 reactVec = transform.position - exlopsoinPos;
        StartCoroutine(OnDamege(reactVec, true));
    }
    IEnumerator OnDamege(Vector3 reactVec, bool isRocket)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (HP > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;  
            gameObject.layer = 14;
            rigid.freezeRotation = false;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            rigid.AddTorque(reactVec * 5, ForceMode.Impulse);
            GetDamage(0);
        }

    }

    IEnumerator EnemyDie()
    {
        myAnimator.SetTrigger("Die");
        //다이라는 애니메이션이                                           //끝나고 나면
        yield return new WaitUntil(() => (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die") && (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)));

        //죽는 모션 이후에 변경할 것들
        GameManager.instance.nUsedMonsterCnt++;
        this.gameObject.SetActive(false);

        yield return null;
    }
}
