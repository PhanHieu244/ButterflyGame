using TMPro;
using UnityEngine;

namespace ChuongCustom
{
    public abstract class ValuePurchase : ButtonPurchase
    {
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private TextMeshProUGUI valueTMP;
        [SerializeField] protected int value;

        protected override void SetupPurchaseData(IAPData iapData)
        {
            value = iapData.value;
            price.SetText(iapData.price);
            valueTMP.SetText(iapData.value.ToString());
        }
    }
}