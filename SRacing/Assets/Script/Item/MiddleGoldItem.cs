using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleGoldItem : BaseItem
{
	public override void OnGetItem(PlayerController player)
	{
		base.OnGetItem(player);
		GameManager.Instance.AddCoin(5000000);
	}

}
