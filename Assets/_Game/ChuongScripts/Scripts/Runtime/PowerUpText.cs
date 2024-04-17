using ChuongCustom;
using TMPro;
using UnityEngine;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    public class PowerUpText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            GameAction.OnPowerUpChange += Show;
            Show();
        }

        private void OnDisable()
        {
            GameAction.OnPowerUpChange -= Show;
        }

        private void Show()
        {
            _text.SetText(Data.Player.powerUp.ToString());
        }
    }
}