using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Shop
{
    [CreateAssetMenu(fileName = "Shop Content", menuName = "Shop/Shop Content", order = 0)]
    public class ShopContent : ScriptableObject
    {
        [SerializeField] private List<SwordSkinItem> _swordSkinItems;
        [SerializeField] private List<BowSkinItem> _bowSkinItems;

        public IEnumerable<SwordSkinItem> SwordSkinItems => _swordSkinItems;
        public IEnumerable<BowSkinItem> BowSkinItems => _bowSkinItems;

        private void Awake()
        {
            /*
            var skinDuplicate = SwordSkinItems
                ?.OrderBy(x => x.SkinType)
                .GroupBy(i => i.SkinType)
                ?.Where(x => x.Count() > 1)
                ?.ToList();
            
            if (skinDuplicate?.Count > 0)
            {
                skinDuplicate.ForEach(x => Debug.Log(x.Key + " " + x.Count()));
                throw new InvalidOperationException(skinDuplicate.ToString());
            }
            */
        }
    }
}