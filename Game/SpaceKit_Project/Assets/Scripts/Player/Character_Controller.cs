using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Controller : MonoBehaviour
{
	Animator anim;
	Rigidbody rb;
	Game_Manager game_Manager;

	[Header("Movment")]
	[SerializeField] float walkSpeed = 5f;
	[SerializeField] float sprintSpeed = 10f;
	[SerializeField] int jumpForce = 5;
	float curSpeed;

	[Header("Bools")]
	private bool isGrunded;

	[Header("Health")]
	[SerializeField] Image HealthImage;
	[SerializeField] Image DamageImage;
	[SerializeField] Text HPText;
	[HideInInspector] public float curHealth;

	void Start()
    {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		game_Manager = FindObjectOfType<Game_Manager>();

		curHealth = 100;

		UpdateHpUI();
	}

    void Update()
    {
		Movement();
		Sprint();
		Jump();

	}

	private void Movement()
	{
		Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * curSpeed;

		rb.velocity = (transform.forward * moveInput.y) + (transform.right * moveInput.x) + (transform.up * rb.velocity.y);
	}

	void Sprint()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if(curSpeed != sprintSpeed)
			{
				curSpeed = sprintSpeed;
			}
		}
		else
		{
			curSpeed = walkSpeed;
		}
	}

	void Jump()
	{
		float moveUp = Input.GetAxis("Jump") * jumpForce;

		if (isGrunded && moveUp != 0)
		{
			rb.AddForce(transform.up * moveUp, ForceMode.VelocityChange);
			isGrunded = false;
		}
	}

	public void UseHealth(int amount)
	{
		if (curHealth > 0)
		{
			curHealth -= amount;
            StartCoroutine(DamageImpact());

			UpdateHpUI();
		}
		else if(curHealth <= 0)
		{
			game_Manager.GetComponent<Game_Manager>().ChackGameOverUI();
		}
	}

	IEnumerator DamageImpact()
	{
		DamageImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		DamageImage.gameObject.SetActive(false);
	}

	public void UpdateHpUI()
	{
		HPText.text = curHealth.ToString() + "%";
		HealthImage.fillAmount = curHealth / 100;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrunded = true;
		}
	}
}
