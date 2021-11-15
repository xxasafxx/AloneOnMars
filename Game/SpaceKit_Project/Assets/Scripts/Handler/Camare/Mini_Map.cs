using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Map : MonoBehaviour
{
	GameObject player;

	void LateUpdate()
    {

		player = GameObject.Find("--Astronaut--");

		Vector3 newPos = player.transform.position;
		newPos.y = transform.position.y;
		transform.position = newPos;

		transform.rotation = Quaternion.Euler(90f, player.transform.eulerAngles.y, 0f);
    }
}
