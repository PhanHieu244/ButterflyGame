using ChuongCustom;
using TMPro;
using UnityEngine;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    public class ReviveText: MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            GameAction.OnReviveChange += Show;
            Show();
        }

        private void OnDisable()
        {
            GameAction.OnReviveChange -= Show;
        }

        private void Show()
        {
            _text.SetText(Data.Player.revive.ToString());
        }
    }
}