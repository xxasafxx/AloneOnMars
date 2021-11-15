using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGinner : MonoBehaviour
{
	
	float heathtoadd = 100;


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			
			other.gameObject.GetComponent<Character_Controller>().curHealth = heathtoadd;
			other.gameObject.GetComponent<Character_Controller>().UpdateHpUI();
		}
	}
}
