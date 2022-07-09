using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TrainingMenuView : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Button _nextButton;

        [Header("Text")]
        [SerializeField] private TMP_Text _introText;
        [SerializeField] private TMP_Text _moveText;
        [SerializeField] private TMP_Text _earthText;
        [SerializeField] private TMP_Text _obstaclesText;
        [SerializeField] private TMP_Text _enemyText;

        [Header("Animations")] 
        [SerializeField] private GameObject _diamonds;
        [SerializeField] private GameObject _moveAnimation;
        [SerializeField] private GameObject _earthAnimation;
        [SerializeField] private GameObject _obstaclesAnimation;
        [SerializeField] private GameObject _enemyAnimation;

        public Button NextButton => _nextButton;

        public TMP_Text IntroText => _introText;
        public TMP_Text MoveText => _moveText;
        public TMP_Text EarthText => _earthText;
        public TMP_Text ObstaclesText => _obstaclesText;
        public TMP_Text EnemyText => _enemyText;

        public GameObject Diamonds => _diamonds;
        public GameObject EnemyAnimation => _enemyAnimation;
        public GameObject MoveAnimation => _moveAnimation;
        public GameObject EarthAnimation => _earthAnimation;
        public GameObject ObstaclesAnimation => _obstaclesAnimation;
    }
}
