using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Weapon_Shop : MonoBehaviour
{
	Weapon_Manager weaponManager;
	Game_Manager manager;
	public WeaponType type;

	[Header("Prefab")]
	[SerializeField] private GameObject weaponPrefab;

	[Header("UI")]
	[SerializeField] private GameObject PanelObj;
	[SerializeField] private GameObject textObj;

	[Header("Price")]
	[SerializeField] private int weaponPrice;
	[SerializeField] private string weaponName;

	private void Start()
	{
		weaponManager = FindObjectOfType<Weapon_Manager>();
		manager = FindObjectOfType<Game_Manager>();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PanelObj.SetActive(true);
			UpdatePrice();

			if (Input.GetKeyDown(KeyCode.E) && manager.Coin >= weaponPrice)
			{
				if (NotTheSameWeapon(weaponManager.equipWeapons))
				{
					InisWeapon();
				}
			}
		}
	}


	public bool NotTheSameWeapon(GameObject[] objects)
	{
		for (int i = 0; i < objects.Length; i++)
		{
			if (objects[i] != null && objects[i].GetComponent<Weapon>().type == type)
			{
				Debug.Log("cant pickup wepon");

				return false;
			}
		}

		return true;
	}

	public void InisWeapon()
	{
		GameObject weapon = Instantiate(weaponPrefab);
		weaponManager.EquipWeapon(weapon);

		manager.Coin -= weaponPrice;

		weapon.transform.localPosition = Vector3.zero;
		weapon.transform.localRotation = Quaternion.Euler(0, 180, 0);
	}

	private void UpdatePrice()
	{
		textObj.GetComponent<Text>().text = "" + weaponName + " " + weaponPrice + "$";
	}

	private void OnTriggerExit(Collider other)
	{
		PanelObj.SetActive(false);
	}
}
