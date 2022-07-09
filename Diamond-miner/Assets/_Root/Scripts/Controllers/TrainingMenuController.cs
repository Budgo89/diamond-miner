using System.Collections.Generic;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using View;

namespace Controllers
{
    public class TrainingMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/TrainingMenu");

        private Transform _placeForUi;
        private TrainingMenuView _trainingMenuView;
        private GameLevel _gameLevel;

        private Button _nextButton;

        private List<TMP_Text> _textList;
        private TMP_Text _introText;
        private TMP_Text _moveText;
        private TMP_Text _earthText;
        private TMP_Text _obstaclesText;

        private List<TMP_Text> _textEnemyList;
        private TMP_Text _enemyText;

        private List<GameObject> _animations;
        private GameObject _diamonds;
        private GameObject _moveAnimation;
        private GameObject _earthAnimation;
        private GameObject _obstaclesAnimation;

        private List<GameObject> _animationenemyList;
        private GameObject _enemyAnimation;

        private int _index = 0;

        public TrainingMenuController(Transform placeForUi, GameLevel gameLevel)
        {
            _placeForUi = placeForUi;
            _gameLevel = gameLevel;

            _trainingMenuView = LoadView(_placeForUi);

            _textList = new List<TMP_Text>();
            _textEnemyList = new List<TMP_Text>();
            _animations = new List<GameObject>();
            _animationenemyList = new List<GameObject>();

            AddElements();
            SubscribeButton();
            StartTrainings();
        }

        private void StartTrainings()
        {
            if (_gameLevel.CurrentLevel == 0)
            {
                _textList[0].gameObject.SetActive(true);
                _animations[0].gameObject.SetActive(true);
            }
            else if (_gameLevel.CurrentLevel == 2)
            {
                _textEnemyList[0].gameObject.SetActive(true);
                _animationenemyList[0].gameObject.SetActive(true);
            }
        }

        private void AddElements()
        {
            _nextButton = _trainingMenuView.NextButton;
            Debug.Log("Кнопку добавили");
            _introText = _trainingMenuView.IntroText;
            _moveText = _trainingMenuView.MoveText;
            _earthText = _trainingMenuView.EarthText;
            _obstaclesText = _trainingMenuView.ObstaclesText;
            _enemyText = _trainingMenuView.EnemyText;
            _textList.Add(_introText);
            _textList.Add(_moveText);
            _textList.Add(_earthText);
            _textList.Add(_obstaclesText);
            _textEnemyList.Add(_enemyText);

            _diamonds = _trainingMenuView.Diamonds;
            _enemyAnimation = _trainingMenuView.EnemyAnimation;
            _moveAnimation = _trainingMenuView.MoveAnimation;
            _earthAnimation = _trainingMenuView.EarthAnimation;
            _obstaclesAnimation = _trainingMenuView.ObstaclesAnimation;
            _animations.Add(_diamonds);
            _animations.Add(_moveAnimation);
            _animations.Add(_earthAnimation);
            _animations.Add(_obstaclesAnimation);
            _animationenemyList.Add(_enemyAnimation);
        }

        private void SubscribeButton()
        {
            _nextButton.onClick.AddListener(OnNextButtonClick);
            Debug.Log("На кнопку подписаны");
        }

        private void OnNextButtonClick()
        {
            _index++;

            if (_gameLevel.CurrentLevel == 0)
            {
                if (_index >= _animations.Count)
                {
                    SaveManagement.SetTraining(1);
                    SceneManager.LoadScene(1);
                }
                else
                {
                    _textList[_index - 1].gameObject.SetActive(false);
                    _animations[_index - 1].gameObject.SetActive(false);

                    _textList[_index].gameObject.SetActive(true);

                    _animations[_index].gameObject.SetActive(true);
                }
            }
            else if (_gameLevel.CurrentLevel == 2)
            {
                if (_index >= _animationenemyList.Count)
                {
                    SaveManagement.SetTraining(1);
                    SceneManager.LoadScene(1);
                }
                else
                {
                    _textEnemyList[_index - 1].gameObject.SetActive(false);
                    _animationenemyList[_index - 1].gameObject.SetActive(false);

                    _textEnemyList[_index].gameObject.SetActive(true);

                    _animationenemyList[_index].gameObject.SetActive(true);
                }
            }
        }

        private void UnsubscribeButton()
        {
            _nextButton.onClick.RemoveAllListeners();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private TrainingMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<TrainingMenuView>();
        }
    }
}
