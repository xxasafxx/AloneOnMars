using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turrent_Shop : MonoBehaviour
{
	Game_Manager manager;

	[Header("Panel")]
	[SerializeField] private GameObject PanelObj;

	[Header("Text")]
	[SerializeField] private GameObject TextGun;
	[SerializeField] private GameObject TextRocket;

	[Header("BuyPrice")]
	[SerializeField] private int Rocketturrent_BuyPrice;
	[SerializeField] private int Gunturrent_BuyPrice;

	[Header("Prefabs")]
	[SerializeField] private GameObject RocketTurrentPrafab;
	[SerializeField] private GameObject GunTurrentPrafab;

	private void Start()
	{
		manager = FindObjectOfType<Game_Manager>();
		PanelObj.SetActive(false);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PanelObj.SetActive(true);
			UpdateWallPrice();

			if (Input.GetKeyDown(KeyCode.X) && manager.Coin >= Rocketturrent_BuyPrice)
			{
				BuyRocketTurrnet();
			}
			
			if(Input.GetKeyDown(KeyCode.Z) && manager.Coin >= Gunturrent_BuyPrice)
			{
				BuyGunTurrnet();
			}
		}
	}

	public void BuyRocketTurrnet()
	{
		manager.Coin -= Rocketturrent_BuyPrice;
		PanelObj.SetActive(false);
		Instantiate(RocketTurrentPrafab, transform.position, Quaternion.identity);
	}

	public void BuyGunTurrnet()
	{
		manager.Coin -= Gunturrent_BuyPrice;
		PanelObj.SetActive(false);
		Instantiate(GunTurrentPrafab, transform.position, Quaternion.identity);
	}

	private void UpdateWallPrice()
	{
		TextGun.GetComponent<Text>().text = "Turrent_Gun" + " " + Gunturrent_BuyPrice + " $";
		TextRocket.GetComponent<Text>().text = "Turrent_Rocket" + " " +  Rocketturrent_BuyPrice + " $";
	}

	private void OnTriggerExit(Collider other)
	{
		PanelObj.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
	}
}
