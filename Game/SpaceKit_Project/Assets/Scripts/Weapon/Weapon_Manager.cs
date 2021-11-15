using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weapon_Manager : MonoBehaviour
{
	
	[SerializeField] private Transform weaponPos; 
	[SerializeField] private SpriteHolder[] SpriteSlots;

	public GameObject[] equipWeapons = new GameObject[2];
    public int equipindex = 0;
	int indexSprit = 0;
	bool isFull = false;

	public GameObject ammoText;
	public GameObject RealodPanel;

	private void Start()
	{
		RealodPanel.SetActive(false);
		SpriteSlots = FindObjectsOfType<SpriteHolder>();
	}

	private void Update()
	{
		ChangeWeaponSprite(); 
		EquipManager();

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			equipindex = 0;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			equipindex = 1;

		}
	}

	private void ChangeWeaponSprite()
	{
		if (equipWeapons[0] != null)
		{
			for (int i = 0; i < SpriteSlots.Length; i++)
			{
				if (SpriteSlots[i] == SpriteSlots[equipindex])
				{
					continue;
				}
				SpriteSlots[i].GetComponent<Image>().color = Color.white;
			}
			SpriteSlots[equipindex].GetComponent<Image>().color = Color.red;
		}
	}

	public void EquipWeapon(GameObject newWeapon) // celld from pickup script
	{
		newWeapon.transform.SetParent(weaponPos);
		

        if (!isFull)
        {
			SpriteSlots[indexSprit].AddSprite(newWeapon.GetComponent<Weapon>().sprite);

			indexSprit++;
		   if (equipWeapons[0] == null)
		   {
		   	 equipWeapons[0] = newWeapon;
		   }

		   else if (equipWeapons[1] == null)
		   {
		   	  equipWeapons[1] = newWeapon;
			  isFull = true;
		   }
		   
        }
        else
        {
			indexSprit = equipindex;
            SpriteSlots[indexSprit].AddSprite(newWeapon.GetComponent<Weapon>().sprite);
            Destroy(equipWeapons[equipindex].gameObject);
            equipWeapons[equipindex] = null;
            equipWeapons[equipindex] = newWeapon;
        }
        
       
	}

	private void EquipManager()
    {
        if(equipindex == 1)
        {
            equipWeapons[1].gameObject.SetActive(true);
            equipWeapons[0].gameObject.SetActive(false);
        }
        else if (equipindex == 1)
        {
            equipWeapons[1].gameObject.SetActive(true);
            equipWeapons[0].gameObject.SetActive(false);
           
        }
        else if (equipindex == 0 && equipWeapons[0] != null)
        {
            equipWeapons[0].gameObject.SetActive(true);
            if(equipWeapons[1] != null)
            {
                equipWeapons[1].gameObject.SetActive(false);
            }
        }
    }
}
