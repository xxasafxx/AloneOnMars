using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weapon : MonoBehaviour
{
	Weapon_Manager weaponmanager;
	Animator anim;
	AudioSource weaponSource;
	public WeaponType type;
	public Sprite sprite;

	[Header("Inislition")]
	[SerializeField] private Transform ShootPoint;
	[SerializeField] private float shootDis = 20f;
	private float originalField;
	private float zoom = 40;
	private float zoomSpeed = 1.5f;

	[Header("Proporte")]
	[SerializeField] private float fireRate = 0.5f;
	[SerializeField] private int curAmmo = 30;
	[SerializeField] private int damage = 10;
	public int ammoInMag = 100;

	float reloadSpeed = 1f;
	float shotCounter;
	int maxAmmo;

	[Header("Effects")]
	[SerializeField] private ParticleSystem muzzleFlash;
	[SerializeField] private ParticleSystem hitEffect;
	[SerializeField] private ParticleSystem EnemyHitEffect;

	[Header("Bools")]
	bool isAiming;
	bool isFiring;
	bool isReloading;

	[Header("UI")]
	[SerializeField] private AudioClip weapnSound;

	void Start()
    {
		weaponmanager = GetComponentInParent<Weapon_Manager>();

		anim = GetComponentInParent<Animator>();
		weaponSource = GetComponent<AudioSource>();

		originalField = Camera.main.fieldOfView;

		maxAmmo = curAmmo;

	}

   
    void Update()
	{
		MouseInput();


		if (isFiring && maxAmmo > 0)
		{
			shotCounter -= Time.deltaTime;

			if (shotCounter <= 0)
			{
				shotCounter = fireRate;

				Shoot();
			}
		}
		else
		{
			shotCounter -= Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			isFiring = false;
			if (maxAmmo < curAmmo && ammoInMag > 0 && !isReloading)
			{
				StartCoroutine(Reload());
			}
		}

		Aim();
		UpdateAmmoUI();
	}

	private void UpdateAmmoUI()
	{
		weaponmanager.ammoText.GetComponent<Text>().text = maxAmmo + "/" + ammoInMag;
	}

	private void Aim()
	{
		if (isAiming)
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, zoomSpeed * Time.deltaTime);
		}
		else
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, originalField, zoomSpeed * Time.deltaTime);
		}
	}

	private void MouseInput()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (!isReloading)
			{
				if (maxAmmo > 0)
				{
					isFiring = true;
				}
				else if(ammoInMag > 0 && !isReloading)
				{
					StartCoroutine(Reload());
				}
			}
			
		}
		else if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			isFiring = false;
		}

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			isAiming = true;
		}
		else if (Input.GetKeyUp(KeyCode.Mouse1))
		{
			isAiming = false;
		}
	}

	private void Shoot()
	{
		weaponSource.PlayOneShot(weapnSound, 0.7f);

		muzzleFlash.transform.position = ShootPoint.transform.position;
		muzzleFlash.Play();

		RaycastHit hit;


		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit, shootDis))
		{
			if (hit.collider.tag == "Enemy")
			{
				EnemyHitEffect.transform.position = hit.point;
				EnemyHitEffect.transform.forward = hit.normal;
				EnemyHitEffect.Play();
			}
			else
			{
				hitEffect.transform.position = hit.point;
				hitEffect.transform.forward = hit.normal;
				hitEffect.Play();
			}

			Enemy enemy = hit.collider.GetComponent<Enemy>();
			if (enemy)
			{
				enemy.Health(damage);
			}
		}

		maxAmmo--;
	}


	IEnumerator Reload()
	{
		anim.SetBool("Reload", true);

		isReloading = true;

		weaponmanager.RealodPanel.SetActive(true);

		yield return new WaitForSeconds(reloadSpeed);

		isReloading = false;

		weaponmanager.RealodPanel.SetActive(false);

		anim.SetBool("Reload", false);

		UpdateAmmo();

	}

	void UpdateAmmo()
	{

		if (ammoInMag <= 0) return;

		int bulletToLoad = curAmmo - maxAmmo;
		int bulletToDeduct = (ammoInMag >= bulletToLoad) ? bulletToLoad : ammoInMag;

		ammoInMag -= bulletToDeduct;
		maxAmmo += bulletToDeduct;
	}
}

public enum WeaponType
{
	BlasterPistol,
	BlasterRifle,
	BlasterShotGun,
	BlasterSmg
	
}
