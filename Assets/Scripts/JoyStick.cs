using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class JoyStick : MonoBehaviour
{

    // 공개
    public Transform Player;        
    public Transform Stick;
    public NavMeshAgent playerNav;

    //float PlayerSpeed;
    // 비공개
    private Vector3 StickFirstPos;  
    private Vector3 JoyVec;         
    private float Radius;           
    private bool MoveFlag;          

    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;
        playerNav = Player.GetComponent<NavMeshAgent>();
        // 캔버스 크기에대한 반지름 조절.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        MoveFlag = false;
    }

    void Update()
    {
        if (MoveFlag)
            playerNav.Move(new Vector3(JoyVec.x, 0, JoyVec.y) * Time.deltaTime * Player.GetComponent<PlayerController>().moveSpeed);
            //Player.transform.Translate(new Vector3(JoyVec.x, 0, JoyVec.y) * Time.deltaTime * PlayerSpeed, Space.World);

            //    Player.transform.Translate(Vector3.forward * Time.deltaTime * 10f);


    }

    // 드래그
    public void Drag(BaseEventData _Data)
    {
        Player.GetComponentInChildren<Animator>().SetBool("isRun", true);
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        // 조이스틱을 이동시킬 방향을 구함.(오른쪽,왼쪽,위,아래)
        JoyVec = (Pos - StickFirstPos).normalized;

        // 조이스틱의 처음 위치와 현재 내가 터치하고있는 위치의 거리를 구한다.
        float Dis = Vector3.Distance(Pos, StickFirstPos);

        // 거리가 반지름보다 작으면 조이스틱을 현재 터치하고 있는 곳으로 이동.
        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        // 거리가 반지름보다 커지면 조이스틱을 반지름의 크기만큼만 이동.
        else
            Stick.position = StickFirstPos + JoyVec * Radius;

        //Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
    }

    // 드래그 끝.
    public void DragEnd()
    {
        Player.GetComponentInChildren<Animator>().SetBool("isRun", false);
        Stick.position = StickFirstPos; // 스틱을 원래의 위치로.
        JoyVec = Vector3.zero;          // 방향을 0으로.
        MoveFlag = false;
    }
}