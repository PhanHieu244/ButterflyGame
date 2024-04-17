using System;
using ChuongCustom;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    public abstract class PowerUpButton<T> : Singleton<T>, IPointerDownHandler
        where T : Component
    {
        [SerializeField] private TextMeshProUGUI amount;

        public abstract int Amount { get; }

        private void OnEnable()
        {
            FetchData();
        }

        public void FetchData()
        {
            amount.SetText(Amount.ToString());
        }

        public void OnClick()
        {
            if (Amount <= 0) return;
            Active();
        }

        public abstract void Active();

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick();
        }
    }
}