using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
	Transform TargetToLook;
	[SerializeField] private Transform objToRotate;

	[Header("Proporties")]
	[SerializeField] private GameObject Bullet;
	[SerializeField] private Transform shootPoint;

	public int damage = 15;
	[SerializeField] private float fireRate = 2f;
	private float bulletSpeed = 30f;
	private float betweenShots;

	private void Update()
	{
		CloseTarget();
		objToRotate.LookAt(TargetToLook);
		betweenShots -= Time.deltaTime;
	}

	private void CloseTarget()
	{
		Enemy[] enemies = FindObjectsOfType<Enemy>();
		Transform closestTarget = null;
		float MaxDis = 50;
		
		foreach(Enemy enemy in enemies)
		{
			float targetDis = Vector3.Distance(transform.position, enemy.transform.position);

			if(targetDis < MaxDis)
			{
				closestTarget = enemy.transform;
				MaxDis = targetDis;
				FireBullet();
			}
		}

		TargetToLook = closestTarget;
	}

	void FireBullet()
	{
		


		if (betweenShots <= 0)
		{
			betweenShots = fireRate;

			GameObject go = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
			Rigidbody rb = go.GetComponent<Rigidbody>();
			go.GetComponent<ParticleSystem>().Play();

			rb.AddForce(shootPoint.forward * bulletSpeed, ForceMode.Impulse);
		}

		
	}


	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 50);
	}
	//public void UpgrateTurrent()
	//{
	//	gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
	//}
}
