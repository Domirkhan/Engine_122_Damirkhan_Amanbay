using System.Collections.Generic;
using UnityEngine;

namespace ZombieIo.Items
{
    public class ItemsService : MonoBehaviour
    {
        private const string PATH_TO_ITEMS = "Prefabs/Items/";
        
        
        public enum ItemClass : byte
        {
            None = 0,
            SmallExperience = 1,
            MediumExperience = 2,
            LargeExperience = 3,
        }

        
        private Dictionary<ItemClass, Queue<Item>> backupItems = new();
        private Dictionary<ItemClass, List<Item>> activeItems = new();


        public Item GetItem(ItemClass itemClass, Vector3 spawnPosition)
        {
            Item newItem = null;
            if (backupItems.ContainsKey(itemClass))
            {
                if (backupItems[itemClass].Count > 0)
                    newItem = backupItems[itemClass].Dequeue();
            }
            else
            {
                backupItems.Add(itemClass, new Queue<Item>());
            }

            if (newItem == null)
            {
                newItem = Instantiate(Resources.Load<GameObject>(PATH_TO_ITEMS + itemClass).GetComponent<Item>());
            }
            
            if (!activeItems.ContainsKey(itemClass))
                activeItems.Add(itemClass, new List<Item>());
            activeItems[itemClass].Add(newItem);
            newItem.Initialize(spawnPosition);
            
            return newItem;
        }
        
        public void RemoveItem(Item item)
        {
            activeItems[item.ItemClass].Remove(item);
            backupItems[item.ItemClass].Enqueue(item);
            item.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (!GameManager.Instance.IsGameActive)
                return;
            
            foreach ((ItemClass itemClass, List<Item> items) in activeItems)
            {
                for (int i = 0; i < items.Count; i++)
                    items[i].OnUpdate();
            }
        }
    }
}