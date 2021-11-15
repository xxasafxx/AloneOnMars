using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteHolder : MonoBehaviour
{
	Image image;
	
	private void Start()
	{
		
		image = GetComponent<Image>();
	}

	public void AddSprite(Sprite S) // called from weaponmanager
	{
		image.sprite = S;
	}
}
