using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    [CreateAssetMenu(menuName = "DP_Items/Objects")]
    public class DP_PickUpObjects : DP_Item
    {
        public GameObject modelPrefab;
        public bool isKey;
        public bool isFlask;
    }
}
