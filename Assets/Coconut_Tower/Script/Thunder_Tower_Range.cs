using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Tower_Range : RangeSystem
{

    float attack_delay = 0.0f;
    GameObject thunder_impact = null;

    bool is_attack = false;
    // Start is called before the first frame update
    void Start()
    {
        thunder_impact = transform.GetChild(1).gameObject;
        thunder_impact.SetActive(false);
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetChild(2).transform.Rotate(this.transform.up, 120 * Time.deltaTime);

        if (enemy_list.Count > 0 && attack_delay <= 0.0f && is_attack == false)
        {
            Vector3 dest = target.position;
            dest.y = this.transform.position.y;
            thunder_impact.transform.position = dest;
            thunder_impact.SetActive(true);

            attack_delay = 3.0f;

            Enemy e = target.GetComponent<Enemy>();
            e.ApplyStatus(2, 0.5f, 0.7f);
            e.GetDamage(20);

            is_attack = true;
        }

        if (attack_delay <= 0.0f)
        {
            thunder_impact.SetActive(false);
            is_attack = false;

        }
        attack_delay -= Time.deltaTime;


    }
}
