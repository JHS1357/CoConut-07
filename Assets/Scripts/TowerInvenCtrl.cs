using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInvenCtrl : MonoBehaviour
{
    static public TowerInvenCtrl instance;
    public int TowerCount = 4;
    //public bool[] canUsableTower = new bool[4];       //후에 해금으로 변경.

    [HideInInspector]
    public enum TowerType { Tower1, Tower2, Tower3, Tower4, None };

    public TowerType SelectedTower;

    public GameObject towerPrefab1;

    public Color usableTower = Color.blue;
    public Color unusableTower = Color.gray;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        //for (int i = 0; i < TowerCount; i++)
        //{
        //    canUsableTower[i] = false;
        //}

        ////사용가능한 타워?
        //canUsableTower[0] = true;

        SelectedTower = TowerType.None;

        SetTowerInventoryImage();
    }

    //public void SwapTower(int index)
    //{
    //    //if (usingTower[index] == false)
    //    //{
    //    //    //무기교체신호 주기.
    //    //    Debug.Log(index);
    //    //    usingTower[index] = true;
    //    //}

    //    //for (int i = 0; i < TowerCount; i++)
    //    //{
    //    //    if (i != index)
    //    //        usingTower[i] = false;
    //    //}

    //    SetTowerInventoryImage();
    //}

    public void WeaponSelect(int index)
    {
        switch(SelectedTower)
        {                                                       //끄기           끄고 키기
            case TowerType.Tower1:      //하나 선택 되어있음. -> 같은거 선택시/다른거 선택시. 나누어야함.
                if (index == 0)
                {
                    SelectedTower = TowerType.None;
                    BuildManager.instance.standardTurretPrefab = null;
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                else
                {
                    SelectedTower = (TowerType)index;
                    BuildManager.instance.standardTurretPrefab = null;  //지금은 없음.
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = true;
                    this.transform.GetChild(0).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                break;
            case TowerType.Tower2:
                if (index == 1)
                {
                    SelectedTower = TowerType.None;
                    BuildManager.instance.standardTurretPrefab = null;
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                else
                {
                    SelectedTower = (TowerType)index;
                    BuildManager.instance.standardTurretPrefab = null;  //지금은 없음.
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = true;
                    this.transform.GetChild(1).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                break;
            case TowerType.Tower3:
                if (index == 2)
                {
                    SelectedTower = TowerType.None;
                    BuildManager.instance.standardTurretPrefab = null;
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                else
                {
                    SelectedTower = (TowerType)index;
                    BuildManager.instance.standardTurretPrefab = null;  //지금은 없음.
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = true;
                    this.transform.GetChild(2).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                break;
            case TowerType.Tower4:
                if (index == 3)
                {
                    SelectedTower = TowerType.None;
                    BuildManager.instance.standardTurretPrefab = null;
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                else
                {
                    SelectedTower = (TowerType)index;
                    BuildManager.instance.standardTurretPrefab = null;  //지금은 없음.
                    this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = true;
                    this.transform.GetChild(3).GetComponent<Jun_TweenRuntime>().enabled = false;
                }
                break;
            case TowerType.None:        //선택 안되었을때 -> 하나 선택으로 바꾸기.
                SelectedTower = (TowerType)index;
                if(index ==0)
                    BuildManager.instance.standardTurretPrefab = towerPrefab1;

                //this.GetComponent<MonoBehaviour>().enabled = true;
                this.transform.GetChild(index).GetComponent<Jun_TweenRuntime>().enabled = true;
                
                break;
        }
        SetTowerInventoryImage();

        //false 일때 해당 무기 선택시 -> 매니저에 프리팹 세팅. 후 색변경.

        //true 일때 해당 무기 선택시. 끄기
    }

    void SetTowerInventoryImage()
    {
        for (int i = 0; i < TowerCount; i++)
        {
            this.transform.GetChild(i).GetComponent<Image>().color = unusableTower;
        }
        switch (SelectedTower)
        {                                  
            case TowerType.Tower1:
                this.transform.GetChild(0).GetComponent<Image>().color = usableTower;

                break;
            case TowerType.Tower2:
                this.transform.GetChild(1).GetComponent<Image>().color = usableTower;
                break;
            case TowerType.Tower3:
                this.transform.GetChild(2).GetComponent<Image>().color = usableTower;
                break;
            case TowerType.Tower4:
                this.transform.GetChild(3).GetComponent<Image>().color = usableTower;
                break;
            case TowerType.None:
                
                break;
        }
        //for (int i = 0; i < TowerCount; i++)
        //{
        //    if (canUsableTower[i] == true)
        //        this.transform.GetChild(i).GetComponent<Image>().color = unusableTower;
        //    else
        //        this.transform.GetChild(i).GetComponent<Image>().color = usableTower;
        //}


    }
}
