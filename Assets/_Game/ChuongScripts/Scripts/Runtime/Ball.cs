using System;
using ChuongCustom;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallData[] _ballData;
        [SerializeField] private Transform circle;
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private CircleCollider2D col2D;
        public bool canMerge;
        private int _id;
        public int count;
        
        public static int Count;

        public int Score => _ballData[_id].score;
        public int Id => _id;

        private void OnValidate()
        {
            rigid = GetComponent<Rigidbody2D>();
            col2D = GetComponentInChildren<CircleCollider2D>();
        }

        public void SetupBall(int id)
        {
            id = Mathf.Clamp(id, 0, _ballData.Length - 1);
            _id = id;
            var data = _ballData[id];
            icon.sprite = data.sprite;
            count = Count++;
            circle.transform.localScale = data.scale * Vector3.one;
            rigid.isKinematic = true;
            col2D.isTrigger = true;
            canMerge = false;
        }

        public void DropBall()
        {
            rigid.isKinematic = false;
            col2D.isTrigger = false;
            DOVirtual.DelayedCall(0.35f, () =>
            {
                canMerge = true;
            });
        }

        private void Upgrade()
        {
            if (_id + 1 >= _ballData.Length) return;
            ScoreManager.Score += Score;
            //upgrade
            _id++;
            var upgradeData = _ballData[_id];
            OnUpgrade(upgradeData);
        }

        private Tween OnUpgrade(BallData data)
        {
            return circle.transform.DOScale(data.scale * Vector3.one, 0.2f).OnComplete(() =>
            {
                icon.sprite = data.sprite;
            });
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!canMerge) return;
            if (!col.collider.tag.Equals("ball")) return;
            var ball = col.gameObject.GetComponent<Ball>();
            if (!ball.canMerge) return;
            if (_id + 1 >= _ballData.Length) return;
            if (ball.Id != _id) return;
            if (count > ball.count)
            {
                Destroy(gameObject);
                return;
            }
            Upgrade();
        }
    }

    [Serializable]
    public class BallData
    {
        public float scale;
        public Sprite sprite;
        public int score;
    }
    
}