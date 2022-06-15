using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private List<Button> _levelButtons;
        [SerializeField] private List<TMP_Text> _levelButtonTexts;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _nextButton;

        public List<Button> LevelButtons => _levelButtons;
        public List<TMP_Text> LevelButtonTexts => _levelButtonTexts;
        public Button BackButton => _backButton;
        public Button NextButton => _nextButton;
    }
}
