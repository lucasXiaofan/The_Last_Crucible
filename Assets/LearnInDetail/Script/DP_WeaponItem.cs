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

    }
}
