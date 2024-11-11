using System;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team11
{
    public class ScoreTrigger : MonoBehaviour
    {
        public delegate void ScoreUpdate(int points);

        public event ScoreUpdate OnScoreUpdate;
        public event Action OnScorePoint;

        [field: SerializeField] public int Score { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
           if (other.CompareTag("Player"))
                    return;

            Score++;
            OnScorePoint?.Invoke();
            OnScoreUpdate?.Invoke(Score);
        }
    }
}
