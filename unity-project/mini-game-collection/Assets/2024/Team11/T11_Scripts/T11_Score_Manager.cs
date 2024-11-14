using MiniGameCollection.Games2024.Team11;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


namespace MiniGameCollection.Games2024.Team11
{
    public class T11_Score_Manager : MonoBehaviour
    {
        //UI Variables
        public TextMeshProUGUI player1;
        public TextMeshProUGUI player2;
        public TextMeshProUGUI timer;
        public TextMeshProUGUI message;
        public int player1Score = 0;
        public int player2Score = 0;
        public float time = 60;
        public bool playTime;

        public T11_Player_Movement play1;
        public T11_Player_Movement play2;


        // Start is called before the first frame update
        void Start()
        {
            playTime = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (playTime)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    playTime = false;
                    time = 0;

                }
            }
            else if (!playTime)
            {
                if (player2Score > player1Score)
                {
                    message.text = "Player 2 Wins!";
                    play1.enabled = false;
                    play2.enabled = false;

                }
                if (player2Score < player1Score)
                {
                    message.text = "Player 1 Wins!";
                    play1.enabled = false;
                    play2.enabled = false;
                }
                if (player2Score == player1Score)
                {
                    message.text = "Draw!";
                    play1.enabled = false;
                    play2.enabled = false;
                }
            }


            player1.text = player2Score.ToString();
            player2.text = player1Score.ToString();
            timer.text = time.ToString();

        }


        private void OnCollisionEnter(Collision collision)
        {
            bool collidedWith1 = collision.gameObject.GetComponentInChildren<T11_Tag_Script_Player1>();
            bool collidedWith2 = collision.gameObject.GetComponentInChildren<T11_Tag_Script_Player2>();

            if (collidedWith1 && !collidedWith2) player2Score++;
            if (collidedWith2 && !collidedWith2) player1Score++;
        }

    }
}
