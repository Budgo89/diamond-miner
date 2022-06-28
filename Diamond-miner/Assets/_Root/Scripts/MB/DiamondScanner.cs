using System.Collections.Generic;
using UnityEngine;

namespace MB
{
    public class DiamondScanner : MonoBehaviour
    {
        private List<GameObject> _diamonds { get; set; } = new List<GameObject>();

        public List<GameObject> GetDiamonds()
        {
            _diamonds = new List<GameObject>();
            DoGetDiamonds();
            return _diamonds;
        }

        private void DoGetDiamonds()
        {
            Diamond[] diamonds = FindObjectsOfType(typeof(Diamond)) as Diamond[];
            foreach (var diamond in diamonds)
            {
                if(_diamonds.Contains(diamond.gameObject))
                    continue;
                _diamonds.Add(diamond.gameObject);
            }
        }

        public void Remove()
        {
            _diamonds.Clear();
        }
    }
}
