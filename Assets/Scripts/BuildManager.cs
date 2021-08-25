using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("빌드매니저 이미 있음!!");
            return;
        }

        instance = this;
    }

    // 포탑 프리팹 넣는 곳
    public GameObject standardTurretPrefab;

    //private void Start()
    //{
    //    BuildTurret = standardTurretPrefab;     
    //}

    //private GameObject BuildTurret;

    public GameObject GetBuildTurret()
    {
        return standardTurretPrefab;
    }
}
