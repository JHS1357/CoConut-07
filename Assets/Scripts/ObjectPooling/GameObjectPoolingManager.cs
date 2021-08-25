using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolingManager : MonoBehaviour
{
    [System.Serializable]
    public class Class
    {
        public List<GameObject> myGameObject = new List<GameObject>();
    }
    [SerializeField]
    public List<Class> myWave = new List<Class>();


    static public GameObjectPoolingManager instance = null;

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

    //부모[] 자식[] 리턴
    public GameObject RequestGameObject(int arr0, int arr1)
    {
        return myWave[arr0].myGameObject[arr1];
    }

    //부모 배열 통채로 리턴
    public List<GameObject> RequestGameObjects(int arr0)
    {
        List<GameObject> arr = new List<GameObject>();
        foreach (GameObject a in myWave[arr0].myGameObject)
        {
            arr.Add(a);
        }
        return arr;
    }

    public void ObjectSetting()
    {
        // 초기화
        myWave = new List<Class>();
        // 부모 탐색
        int myWaveCount = transform.childCount;
        for (int i =0; i < myWaveCount; i++)
        {
            // 부모 배열 생성
            myWave.Add(new Class());
            // 자식 오브젝트 탐색
            int myGameObjectCount = transform.GetChild(i).transform.childCount;            
            for (int j = 0; j < myGameObjectCount; j++)
            {
                // 할당
                myWave[i].myGameObject.Add(transform.GetChild(i).transform.GetChild(j).gameObject);
                // 자식 꺼두기
                myWave[i].myGameObject[j].SetActive(false);
            }
        }
    }
}
