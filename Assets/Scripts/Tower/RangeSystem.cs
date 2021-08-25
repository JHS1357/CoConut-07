using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSystem : MonoBehaviour
{
    public LayerMask enemy_mask;
    public List<GameObject> enemy_list;
    public Transform target = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In");
        if (((1 << other.gameObject.layer) & enemy_mask) != 0)
        {
            enemy_list.Add(other.gameObject);
            if (target == null) target = other.transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("Out");
        if (((1 << other.gameObject.layer) & enemy_mask) != 0)
        {
            foreach (GameObject obj in enemy_list)
                if (obj.transform == other.gameObject.transform)
                {
                    enemy_list.Remove(obj);
                    if (obj.transform == target)
                    {
                        if (enemy_list.Count > 0)
                            target = enemy_list[0].transform;
                        else
                            target = null;
                    }
                    break;
                }
        }
    }
}
