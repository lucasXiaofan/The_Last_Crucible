using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    [CreateAssetMenu(menuName = "DP_Items/Weapon")]
    public class DP_WeaponItem : DP_Item
    {
        public GameObject modelPrefab;
        public bool isArmed;
        [Header("One handed attack animations")]
        public string Oh_Light_Attack_1;
        public string Oh_Light_Attack_2;
        public string Oh_Heavy_Attack_1;
        public string Oh_Heavy_Attack_2;

    }
}
