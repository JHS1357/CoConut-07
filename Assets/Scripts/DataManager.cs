using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        //else
        //Destroy(this.gameObject);
    }

    public int[] enemyHP = { 100, 200, 300, 400 };
    public float[] enemySpeed = { 3.0f, 4.0f, 5.0f, 6.0f };
}
