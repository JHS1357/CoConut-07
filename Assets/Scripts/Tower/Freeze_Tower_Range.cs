using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_Tower_Range : RangeSystem
{
    GameObject freeze_area = null;
    float attack_delay = 0.0f;

    bool is_attack = false;
    // Start is called before the first frame update
    void Start()
    {
        freeze_area = transform.parent.GetChild(1).gameObject;
        freeze_area.SetActive(false);
    }

    // Update is called once per frame
    void Update() 
    {
        if (enemy_list.Count > 0 && attack_delay <= 0.0f && is_attack == false)
        {
            freeze_area.SetActive(true);
            // freeze_area.transform.position = this.transform.position;
            attack_delay = 3.0f;

            foreach(GameObject enemy in enemy_list)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.ApplyStatus(1, 2.0f, 0.5f);
                e.GetDamage(10);
            }

            is_attack = true;
        }

        if (attack_delay <= 1.0f)
        {
            freeze_area.SetActive(false);
            is_attack = false;

        }
        attack_delay -= Time.deltaTime;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemy_mask) != 0)
        {
            enemy_list.Add(other.gameObject);
            if (target == null) target = other.transform;

            if (is_attack)
            {
                Enemy e = other.GetComponent<Enemy>();
                e.ApplyStatus(1, 2.0f, 0.5f);
                e.GetDamage(10);
            }
        }
    }
}
