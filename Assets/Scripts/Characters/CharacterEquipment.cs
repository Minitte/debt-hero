using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterEquipment : MonoBehaviour {

	public ItemEquipment weapon;

	/// <summary>
	/// Stats of the character
	/// </summary>
	private CharacterStats _stats;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_stats = GetComponent<CharacterStats>();
	}

	/// <summary>
	/// Attempts to automaticly equip the item
	/// </summary>
	/// <param name="equipment"></param>
	public void Equip(ItemEquipment equipment) {
		switch (equipment.properties.type) {
			case ItemProperties.Type.EQUIPMENT_WEAPON:
				EquipWeapon(equipment);
				break;

			default:
				break;
		}
	}

	public void Unequip(ItemEquipment equipment) {
		switch (equipment.properties.type) {
			case ItemProperties.Type.EQUIPMENT_WEAPON:
				// if that equipment is actually equiped
				if (weapon.Equals(equipment)) {
					UnequipWeapon();
				}
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// Equips the give weapon and replaces the old one if any  
	/// </summary>
	/// <param name="wpn"></param>
	public void EquipWeapon(ItemEquipment wpn) {
		Debug.Assert(wpn.properties.type != ItemProperties.Type.EQUIPMENT_WEAPON, wpn.properties.name + " is not a weapon?");

		weapon.RemoveBonusStats();

		weapon = wpn;

		wpn.AddBonusStats(); 
	}

	/// <summary>
	/// Unequips the weapon and removes the bonuses
	/// </summary>
	public void UnequipWeapon() {
		if (weapon != null) {
			weapon.RemoveBonusStats();
			weapon = null;
		}
	}
}
