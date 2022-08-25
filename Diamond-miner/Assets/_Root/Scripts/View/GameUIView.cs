using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private Button _pauseMenuButton;
        [SerializeField] private Button _removeStoneButton;
        [SerializeField] private Button _powerButton;
        [SerializeField] private TMP_Text _diamondСounts;
        [SerializeField] private TMP_Text _gameTime;

        public Button PauseMenuButton => _pauseMenuButton;
        public Button RemoveStoneButton => _removeStoneButton;
        public Button PowerButton => _powerButton;
        public TMP_Text GameTime => _gameTime;
        public TMP_Text DiamondСounts => _diamondСounts;
    }
}
