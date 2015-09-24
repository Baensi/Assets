using UnityEngine;
using System.Collections;

namespace Engine.Objects {

	public enum ObjectType : int {

		// предметы общего назначения
		useItem  = 0x00, // используемый предмет
		bookItem = 0x01, // предмет-книга
		noUse    = 0x02, // часть интерьера/ не используемый

		// защита
		protectionHelmet   = 0xa0, // шлем
		protectionArmor    = 0xa1, // броня
		protectionBracers  = 0xa2, // наручи
		protectionLeggings = 0xa3, // поножи
		protectionBoots    = 0xa4, // ботинки

		// оружие
		weaponShield = 0xf0, // шит
		weaponSword  = 0xf1, // меч
		weaponRange  = 0xf2, // лук
		weaponMagic  = 0xf3  // магия

	};

}