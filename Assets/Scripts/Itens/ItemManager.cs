using EDGEE.Core.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetup;

        void Start()
        {
            Reset();
        }

        private void Reset()
        {
            foreach (var item in itemSetup)
            {
                item.soInt.valueInt = 0;
            }
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetup.Find(i => i.itemType == itemType);
        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount< 0) return;

            itemSetup.Find(i => i.itemType == itemType).soInt.valueInt += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {

            var item = itemSetup.Find(i => i.itemType == itemType);
            item.soInt.valueInt -= amount;

            if (item.soInt.valueInt < 0) item.soInt.valueInt = 0;
        }

        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }
        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

        [System.Serializable]
         public class ItemSetup
         {
           public ItemType itemType;
           public SOInt soInt;
           public Sprite icon;
         }
   }

}