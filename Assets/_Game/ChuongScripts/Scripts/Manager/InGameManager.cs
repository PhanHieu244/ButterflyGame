using System;
using _Game.ChuongScripts.Scripts.Runtime;
using UnityEngine;

namespace ChuongCustom
{
    public class InGameManager : Singleton<InGameManager>
    {
        [SerializeField] private Spawner spawner;

        public static Action OnLose;

        private void Start()
        {
            OnLose += LoseGame;
            spawner.Spawn();
            ScoreManager.Score = 0;
        }

        private void OnDestroy()
        {
            OnLose -= LoseGame;
        }

        private void LoseGame()
        {
            ScoreManager.OnUpdateHighScore();
            ScreenManager.Instance.OpenScreen(ScreenType.Result);
        }
    }
}