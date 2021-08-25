using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    public Transform myTarget;
    Vector2 MinimapSize = Vector2.zero;
    // Start is called before the first frame update

    public Color myColor
    {
        set
        {
            GetComponentInChildren<Image>().color = value;
        }
    }

    void Start()
    {
        GameObject obj = GameObject.Find("MiniMap");
        MinimapSize = obj.GetComponent<RectTransform>().sizeDelta;
    }

    public void Initalize(Transform target)
    {
        this.transform.SetParent(GameObject.Find("MiniMap").transform);
        myTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget != null)
        {
            Vector2 viewportPos = Camera.allCameras[1].WorldToViewportPoint(myTarget.position);
            viewportPos.x = MinimapSize.x * viewportPos.x - MinimapSize.x / 2;
            viewportPos.y = MinimapSize.y * viewportPos.y - MinimapSize.y / 2;
            this.transform.localPosition = viewportPos;

        }
    }
}
