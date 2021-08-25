using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationSentence : MonoBehaviour
{
    private TextMeshPro tmp;
    public string[] sentence;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        // "왼쪽 조이스틱으로 캐릭터를 직접 움직이세요!", "무기변경은 아래 아이템창에서 원하는 무기로 선택가능합니다!", "포탑설치는 신중히해주세요!"
    }

    public void ChangeSentence()
    {

        tmp.text = sentence[Random.Range(0, sentence.Length + 1)];
    }
}