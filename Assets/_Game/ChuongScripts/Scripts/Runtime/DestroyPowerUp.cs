using ChuongCustom;
using UnityEngine;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    public class DestroyPowerUp : PowerUpButton<DestroyPowerUp>
    {
        [SerializeField] private RectTransform numBg;
        private bool _isActive;
        
        public override int Amount => Data.Player.powerUp;
        public override void Active()
        {
            _isActive = !_isActive;
            numBg.gameObject.SetActive(_isActive);
            if(!_isActive) return;
            InputInGame.Instance.ToPowerUpInput();
        }

        public void Done()
        {
            FetchData();
            _isActive = false;
            numBg.gameObject.SetActive(true);
        }
    }
}