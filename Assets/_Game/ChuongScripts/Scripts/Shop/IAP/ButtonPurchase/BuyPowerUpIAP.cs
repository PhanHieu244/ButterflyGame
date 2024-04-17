using UnityEngine;

namespace ChuongCustom
{
    public class BuyPowerUpIAP : ValuePurchase
    {
        protected override void Setup()
        {
        }

        protected override void OnPurchaseSuccess()
        {
            ToastManager.Instance.ShowMessageToast("Buy Success!!");
            //todo buy pu
            Data.Player.powerUp += value;
            GameAction.OnPowerUpChange?.Invoke();
        }
    }
}