using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopitem : BaseItem
{
	public override void OnGetItem(PlayerController player)
	{
		base.OnGetItem(player);
		GameManager.Instance.ShopManager.GoingShop();	
	}

}


