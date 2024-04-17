using System.Collections.Generic;
using System.Linq;
using ChuongCustom;
using DG.Tweening;
using UnityEngine;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    public class Spawner : Singleton<Spawner>
    {
        [SerializeField] private Ball ballPrefab;
        public Ball CurrentBall { get; private set; }

        private List<GameObject> _balls = new();

        public void Spawn(int id)
        {
            CurrentBall = Instantiate(ballPrefab);
            CurrentBall.transform.position = transform.position;
            CurrentBall.SetupBall(id);
        }

        public void Drop()
        {
            CurrentBall.DropBall();
            _balls.Add(CurrentBall.gameObject);
            CurrentBall = null;
            DOVirtual.DelayedCall(0.3f, Spawn);
        }

        private bool Move(float x)
        {
            if (CurrentBall == null) return false;
            var newPos = CurrentBall.transform.position;
            newPos.x = x;
            CurrentBall.transform.position = newPos;
            return true;
        }

        public void MoveAndDrop(float x)
        {
            if (!Move(x)) return;
            Drop();
        }

        public void DestroyAllBalls()
        {
            foreach (var ball in _balls.Where(ball => ball != null))
            {
                DestroyImmediate(ball);
            }
        }
        
        public void Spawn()
        {
            var max = 4;
            var score = ScoreManager.Score;
            if (score <= 3000)
            {
                max = 4;
            }
            else if(score <= 5000)
            {
                max = 5;
            }
            else if(score <= 7000)
            {
                max = 6;
            }
            else if(score <= 10000)
            {
                max = 7;
            }
            else if(score <= 12000)
            {
                max = 8;
            }
            else
            {
                max = 9;
            }
            Spawn(Random.Range(0, 4));
        }
        
    }
}