using UnityEngine;

namespace ChuongCustom
{
    public class BuyReviveIAP : ValuePurchase
    {
        protected override void Setup()
        {
        }

        protected override void OnPurchaseSuccess()
        {
            ToastManager.Instance.ShowMessageToast("Buy Success!!");
            Data.Player.revive += value;
            GameAction.OnReviveChange?.Invoke();
        }
    }
}