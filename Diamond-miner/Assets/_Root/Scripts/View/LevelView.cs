using UnityEngine;

namespace View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private int diamondCount;

        public int DiamondCount => diamondCount;
    }
}
