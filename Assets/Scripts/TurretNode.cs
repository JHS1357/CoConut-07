using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNode : MonoBehaviour
{
	public Color hoverColor;		// 마우스 오버 시 강조 색
	public Vector3 positionOffset;	// 포탑 설치 시 세부 위치 조정

	private GameObject turret;   

	private Renderer rend;
	private Color originColor;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		originColor = rend.material.color;
	}

	private void OnMouseEnter()
	{
		rend.material.color = hoverColor;
	}

	private void OnMouseExit()
	{
		rend.material.color = originColor;
	}

	private void OnMouseDown()
	{
		// 포탑 설치 불가능
		if (BuildManager.instance.GetBuildTurret() == null)
		{
			Debug.Log("포탑 설치 불가능 - TODO: 화면에 경고 띄우기");
			return;
		}

		GameObject buildTurret = BuildManager.instance.GetBuildTurret();
		turret = Instantiate(buildTurret, transform.position + positionOffset, transform.rotation);
	}
}

