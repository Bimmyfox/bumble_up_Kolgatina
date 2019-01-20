using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Main : MonoBehaviour
    {
        public static Main self = null;

        Player player;
        StairwayGeneration stairway;
        public Queue<Enemy> enemies;


        public Player Player
        {
            get { return player; }
            set
            {
                if (value)
                    player = value;
            }
        }

        public StairwayGeneration Stairway
        {
            get { return stairway; }
            set
            {
                if (value)
                    stairway = value;
            }
        }

        void Awake()
        {
            if (self == null)
                self = this;
            enemies = new Queue<Enemy>();
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