using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;
        [Header("One handed attack animations")]
        public string Oh_Light_Attack_1;
        public string Oh_Heavy_Attack_1;

    }
}
