using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	Turrent turrent;

	private void Start()
	{
		turrent = FindObjectOfType<Turrent>();
		Destroy(gameObject, 2);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.GetComponent<Enemy>().Health(turrent.GetComponentInParent<Turrent>().damage);

			Destroy(gameObject);
		}
	}
}
