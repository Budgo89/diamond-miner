using UnityEngine;
using UnityEngine.UI;

namespace View
{
    internal class LanguageMenuView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        public Button BackButton => _backButton;
    }
}
