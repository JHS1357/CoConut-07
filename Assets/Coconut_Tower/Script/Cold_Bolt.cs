using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cold_Bolt : RangeSystem
{

    Transform tgt = null;
    Coroutine move = null;
    int count = 0;

    bool is_impact = false;
    float impact_time = 0.0f;
    //float projectile_speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (tgt != null && move == null && !is_impact
            && this.transform.Find("Cold_Bolt").gameObject.activeSelf)
            move = StartCoroutine(fire());

        if (is_impact)
            impact_effect();
    }

    void impact_effect()
    {

        if (impact_time > 0.0f)
            impact_time -= Time.deltaTime;
        else
        {
            this.transform.Find("WhityBomb").gameObject.SetActive(false);
            is_impact = false;
        }


    }

    IEnumerator fire()
    {
        Vector3 targetPos = this.transform.position;
        targetPos.y = 0.0f;

        Vector3 destine = tgt.position;
        destine.y = 0.0f;

        Vector3 dir = tgt.position - this.transform.position;
        dir.y = 0.0f;
        dir.Normalize();

        float left_dist = Vector3.Distance(destine, targetPos);
        float total_dist = left_dist;
        float proj_height = this.transform.position.y * 1.25f;

        while (this.transform.position != destine)
        {
            float delta = total_dist * Time.deltaTime;

            if (left_dist - delta <= 0.01f)
                delta = left_dist;
            targetPos += dir * delta;
            left_dist -= delta;
            float height = Mathf.Sin(Mathf.PI * (((total_dist - left_dist) / total_dist) * 0.6f + 0.4f));
            targetPos.y = proj_height * height;

            if (left_dist <= 0.1f)
                targetPos = destine;

            this.transform.position
                = targetPos;

            yield return 0;
        }

        move = null;
        impact();

        count++;
    }

    void impact()
    {
        foreach (GameObject enemy in enemy_list)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            e.ApplyStatus(1, 2.5f, 0.5f);
            e.GetDamage(20);
        }

        this.transform.Find("Cold_Bolt").gameObject.SetActive(false);
        this.transform.Find("WhityBomb").gameObject.SetActive(true);
        is_impact = true;
        impact_time = 1.0f;
    }


    public void SetTarget(Transform dst)
    {
        tgt = dst;
        Vector3 init_vec = tgt.position;
        init_vec.y = this.transform.position.y;
        this.transform.LookAt(init_vec);
    }
}
