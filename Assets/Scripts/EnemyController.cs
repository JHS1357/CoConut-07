using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyNav;
    MinimapIcon myMinimapicon;


    // Start is called before the first frame update
    void Start()
    {
        enemyNav = this.GetComponent<NavMeshAgent>();

        GameObject obj = Instantiate(Resources.Load("Prefabs/MiniMapIcon") as GameObject);
        myMinimapicon = obj.GetComponent<MinimapIcon>();
        myMinimapicon.Initalize(this.transform);
        myMinimapicon.myColor = Color.red;
    }

    public void mySetDestination(Vector3 endPos)
    {
        //enemyNav.SetDestination(endPos);
        //enemyNav.SetDestination(endPos)
    }

    void Update()
    {
        
    }
}
