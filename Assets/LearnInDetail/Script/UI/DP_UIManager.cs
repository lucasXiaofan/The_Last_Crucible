using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP
{
    public class DP_UIManager : MonoBehaviour
    {
        public GameObject playerUIBar;
        public void TurnOnorOffUI(bool on)
        {
            playerUIBar.SetActive(on);
        }
    }
}

