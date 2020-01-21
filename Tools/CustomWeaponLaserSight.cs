using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
[RequireComponent(typeof(Weapon))]
/// <summary>
/// same as WeaponLaserSight but works in front of onewayplatforms. Should be attached as components to wands which need a lasersight.
/// </summary>
public class CustomWeaponLaserSight : WeaponLaserSight
{


/// <summary>
/// Set the sorting layer of the line so it shows up in front of onewayplatforms
/// </summary>
protected override void Initialization()
{
	base.Initialization();
	_line.sortingLayerName="Player";
}

}
}
