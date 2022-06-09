using System.Collections.Generic;
using UnityEngine;

namespace MB
{
    public class DiamondScanner : MonoBehaviour
    {
        private List<GameObject> _diamonds { get; set; }

        public List<GameObject> GetDiamonds()
        {
            DoGetDiamonds();
            return _diamonds;
        }

        private void DoGetDiamonds()
        {
            _diamonds = new List<GameObject>();
            Diamond[] diamonds = FindObjectsOfType(typeof(Diamond)) as Diamond[];
            foreach (var diamond in diamonds)
            {
                _diamonds.Add(diamond.gameObject);
            }
        }
    }
}
