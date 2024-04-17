using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using static System.String;

namespace ChuongCustom
{
    public class IAPDataManager : MonoBehaviour
    {
        [SerializeField] private List<IAPData> data;
        private static Dictionary<string, IAPData> _dataDict;

        private void Awake()
        {
            _dataDict = new();
            foreach (var iapData in data)
            {
                _dataDict.Add(iapData.productID, iapData);
            }
        }

        public static IAPData GetData(string productID)
        {
            return _dataDict[productID];
        }

        [Button]
        private void LoadIapKey()
        {
            data ??= new();
            var fields =
                typeof(IAPKey).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var field in fields)
            {
                var productKey = field.GetValue(null).ToString();
                if(IsNullOrEmpty(productKey)) continue;
                if(data.Any(iapData => iapData.productID == productKey)) continue;
                data.Add(new IAPData()
                {
                    productID = productKey
                });
            }
        }

        /*[Button]
        private void Sort()
        {
            data?.Sort((first, second) => first.amount.CompareTo(second.amount));
        }*/
    }

    [Serializable]
    public class IAPData
    {
        [IAPKey] public string productID;
        public string price;
        public int value;
    }
}



