using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Main : MonoBehaviour
    {
        public static Main self = null;

        Player player;
 
        public Player Player
        {
            get { return player; }
            set
            {
                if (value)
                    player = value;
            }
        }

        void Awake()
        {
            if (self == null)
                self = this;
        }


        void Update()
        {
            if (CheckDefeat())
            {
                Debug.Log("Defeat :(");
                return;
            }
        }

      

        bool CheckDefeat()
        {
            return !player.isActiveAndEnabled;
        }
    }
}