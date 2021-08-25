using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cold_Bolt_Range : RangeSystem
{
    Vector3 init_forward;
    GameObject fire_proj = null;
    float attack_speed = 3.0f;
    float attack_delay = 0.0f;
    private Vector3 init_proj_pos ;

    // Start is called before the first frame update
    void Start()
    {
        init_forward = this.transform.forward;
        fire_proj = this.transform.Find("Cold_Bolt_Obj").gameObject;
        init_proj_pos = fire_proj.transform.position;
        fire_proj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Transform target_vect = target;
            target_vect.position.Set(target_vect.position.x, this.transform.position.y, target_vect.position.z);

            this.transform.LookAt(target_vect);
            if (attack_delay <= 0.0f)
            {
                attack_delay = attack_speed;

                fire_proj.transform.position = init_proj_pos;
                fire_proj.GetComponent<Cold_Bolt>().SetTarget(target);
                if(fire_proj.activeSelf == false) fire_proj.SetActive(true);
                fire_proj.transform.Find("Cold_Bolt").gameObject.SetActive(true);
                fire_proj.transform.Find("WhityBomb").gameObject.SetActive(false);
            }
        }
        

        if(attack_delay > 0.0f)
            attack_delay -= Time.deltaTime;
    }
}
