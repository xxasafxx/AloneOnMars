using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo_Shop : MonoBehaviour
{
	Weapon weapon;
	Game_Manager manager;

	[Header("UI")]
	[SerializeField] private GameObject PanelObj;
	[SerializeField] private GameObject TextObj;

	[Header("Price")]
	[SerializeField] private int ammoPrice;
	private bool isAmmoFull;

	private void Start()
	{
		weapon = FindObjectOfType<Weapon>();
		manager = FindObjectOfType<Game_Manager>();
		PanelObj.SetActive(false);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			weapon = other.gameObject.GetComponentInChildren<Weapon>();
			PanelObj.SetActive(true);
			UpdateWallPrice();

			if (Input.GetKeyDown(KeyCode.E) && manager.Coin >= ammoPrice)
			{
				BuyAmmo();
			}
		}
	}

	private void BuyAmmo()
	{
		if (!isAmmoFull)
		{
			manager.Coin -= ammoPrice;
			weapon.ammoInMag += 50;
			PanelObj.SetActive(false);
		}
	}

	private void UpdateWallPrice()
	{
		if (weapon.ammoInMag < 100 && weapon)
		{
			isAmmoFull = false;
			TextObj.GetComponent<Text>().text = "" + ammoPrice + "$";
		}
		else
		{
			isAmmoFull = true;
			TextObj.GetComponent<Text>().text = "Your Ammo is Full";
		}
	}

	private void OnTriggerExit(Collider other)
	{
		PanelObj.SetActive(false);
	}
}
