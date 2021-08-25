using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInvenCtrl : MonoBehaviour
{
    static public WeaponInvenCtrl instance;
    public int weaponCount = 4;
    public bool[] usingWeapon = new bool[4];
    public Color usableWeapon = Color.blue;
    public Color unusableWeapon = Color.gray;

    public int weaponIndex = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < weaponCount; i++) 
        {
            usingWeapon[i] = false;
        }
        usingWeapon[0] = true;

        SetWeaponInventoryImage();
    }



    public void SwapWeapon(int index)
    {
        if (usingWeapon[index] == false)
        {
            //무기교체신호 주기.
            weaponIndex = index;
            Debug.Log(weaponIndex);
            usingWeapon[index] = true;
        }

        for (int i = 0; i < weaponCount; i++)
        {
            if (i != index)
                usingWeapon[i] = false;
        }

        SetWeaponInventoryImage();
    }

    void SetWeaponInventoryImage()
    {
        for (int i = 0; i < weaponCount; i++)
        {
            if (usingWeapon[i] == true)
                this.transform.GetChild(i).GetComponent<Image>().color = unusableWeapon;
            else
                this.transform.GetChild(i).GetComponent<Image>().color = usableWeapon;
        }
    }
}
