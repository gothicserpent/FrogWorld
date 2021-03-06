﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;

namespace MoreMountains.CorgiEngine
{
public class PersistentInventoryPickableItem : InventoryPickableItem
{

int InitialQuantity;

protected override void Start()
{
	base.Start();
	InitialQuantity = Quantity; //need this value for PickSuccess quantity adding
}

/// <summary>
/// Describes what happens when the object is successfully picked using the SaveManager
/// </summary>
protected override void PickSuccess()
{
	base.PickSuccess();

	int inivalue = SaveManager.Instance.INIRead("Inventory",Item.ItemID);
	inivalue+=InitialQuantity;
	SaveManager.Instance.INIWrite("Inventory",Item.ItemID,inivalue);

//if the player is near the location of one of his last deaths, then the death section key should lower InitialQuantity in value. if the (value-InitialQuantity)==0, then the key should be deleted from the death section. if the death section has no keys left, it should be deleted. this will prevent the death sections from constantly respawning items
//public void PlayerDeathINIDelete(float x, float y, string Item)
	SaveManager.Instance.PlayerDeathINIDelete(this.transform.position.x, this.transform.position.y, Item.ItemID, InitialQuantity); //delete the value from the death section in the ini when it's picked up
}

}
}
