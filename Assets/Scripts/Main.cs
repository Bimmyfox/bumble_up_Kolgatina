using UnityEngine;

namespace Game
{
    public enum StateGame
    {
        PLAY,
        DEFEAT
    };

    public class Main : MonoBehaviour
    {

        public static Main self = null;

        Player player;
        StateGame stateGame;

        public Player Player
        {
            get { return player; }
            set
            {
                if (value)
                    player = value;
            }
        }

        public StateGame StateGame
        {
            get { return stateGame; }
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
                stateGame = StateGame.DEFEAT;
                return;
            }
        }

        bool CheckDefeat()
        {
            return !player.isActiveAndEnabled;
        }
    }
}