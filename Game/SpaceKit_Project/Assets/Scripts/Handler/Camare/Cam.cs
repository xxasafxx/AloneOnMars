using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

	float sensitivity = 100f;
	Transform player;

	float rotUpDown = 0f;

    void Start()
    {
		player = GameObject.Find("--Astronaut--").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

  
    void Update()
    {
		float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
		float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

		rotUpDown -= y;
		rotUpDown = Mathf.Clamp(rotUpDown, -90f, 90f);
		transform.localRotation = Quaternion.Euler(rotUpDown, 0f, 0f);

		player.Rotate(Vector3.up * x);
	}
}
