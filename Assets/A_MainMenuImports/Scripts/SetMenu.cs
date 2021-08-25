using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenu : MonoBehaviour
{
    private GameObject map;

    private void Start()
    {
        map = Resources.Load<GameObject>("Prefabs/CubeMap");
        map.transform.position = transform.position;
    }
}
