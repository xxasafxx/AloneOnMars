using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
	Animator anim;
	Rigidbody rb;

	Game_Manager manager;
	EnemySpawoner spawoner;

	Transform target;
	Transform Basetarget;

	[Header("Int")]
	[SerializeField] private int moveSpeed = 5;
	[SerializeField] private int stopDis = 2;

	private float shotCounter;
	private int CoinValue = 15;
	int fireRate = 1;

	[Header("Health")]
	[SerializeField] private int curHealth;
	[SerializeField] private Slider Healthslider;

	private bool isDead = false;
	private bool playerContact = false;

	void Start()
	{
		manager = FindObjectOfType<Game_Manager>();
		spawoner = FindObjectOfType<EnemySpawoner>();

		target = GameObject.Find("--Astronaut--").transform;
		Basetarget = GameObject.Find("Base").transform;

		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		curHealth = 100 + (manager.Wave * 10);
		Healthslider.maxValue = curHealth;
		Healthslider.value = curHealth;
	}

	private void Update()
	{
		if (!playerContact)
		{
			float curDistance = Vector3.Distance(transform.position, Basetarget.transform.position);

			if (!isDead)
			{
				if (curDistance <= stopDis)
				{
					shotCounter -= Time.deltaTime;

					if (shotCounter <= 0)
					{
						shotCounter = fireRate;

						Attack();
					}
				}
				if (curDistance > stopDis)
				{
					Movement(Basetarget); // go to Base
				}
			}

		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			float curDistance = Vector3.Distance(transform.position, target.transform.position);

			playerContact = true;

			if (!isDead)
			{
				if (curDistance <= stopDis)
				{
					shotCounter -= Time.deltaTime;

					if (shotCounter <= 0)
					{
						shotCounter = fireRate;

						Attack();
					}
				}
				if (curDistance > stopDis)
				{
					Movement(target); // go to plyar
				}
			}
		} 
	}

	private void Movement(Transform target)
	{
		Vector3 pos = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
		rb.MovePosition(pos);

		rb.AddForce(new Vector3(0, -10f, 0),ForceMode.Impulse); // gravity
		transform.LookAt(target.position);

		anim.SetFloat("Move", rb.velocity.magnitude);
		anim.SetBool("IsAttack", false);
		
	}

	private void Attack()
	{
		if (!anim.GetBool("IsAttack"))
		{
			anim.SetFloat("AttackType", Random.Range(0, 3));
		}
		
		
		anim.SetBool("IsAttack", true);
		FindObjectOfType<Character_Controller>().UseHealth(15);	
	}

	public void Health(int damageAmount)
	{
		curHealth -= damageAmount;
		Healthslider.value = curHealth;

		if (!isDead)
		{
			Game_Manager mm = FindObjectOfType<Game_Manager>();
			mm.AddPoints(CoinValue);
			if (curHealth <= 0)
			{
				Die();
			}
		}
	}

	void Die()
	{
		isDead = true;
		spawoner.EnemiesLeftInRound -= 1;
		GetComponent<CapsuleCollider>().enabled = false;

		anim.SetFloat("Move", 0);
		anim.SetBool("IsDead", true);
		anim.SetBool("IsAttack", false);

		manager.AddPoints(5);
		Destroy(gameObject,1f);

	}

	private void OnTriggerExit(Collider other)
	{

		playerContact = false;

	}
}
