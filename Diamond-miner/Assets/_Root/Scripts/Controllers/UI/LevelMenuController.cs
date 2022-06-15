using System.Collections.Generic;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Controllers.UI
{
    internal class LevelMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("UI/LevelMenu");

        private Transform _placeForUi;
        private ProfilePlayers _profilePlayer;
        private GameLevel _gameLevel;

        private LevelMenuView _levelMenuView;

        private List<Button> _levelButtons;


        public LevelMenuController(Transform placeForUi, ProfilePlayers profilePlayer, GameLevel gameLevel)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;
            _gameLevel = gameLevel;

            _levelMenuView = LoadView(placeForUi);
            _levelButtons = _levelMenuView.LevelButtons;

            SubscribeButton();
            ButtonActive();
        }

        private void SubscribeButton()
        {
            
            _levelButtons[0].onClick.AddListener(OnLevelClick0);
            _levelButtons[1].onClick.AddListener(OnLevelClick1);
            _levelButtons[2].onClick.AddListener(OnLevelClick2);
            _levelButtons[3].onClick.AddListener(OnLevelClick3);
            _levelButtons[4].onClick.AddListener(OnLevelClick4);
            _levelButtons[5].onClick.AddListener(OnLevelClick5);
            _levelButtons[6].onClick.AddListener(OnLevelClick6);
            _levelButtons[7].onClick.AddListener(OnLevelClick7);
            _levelButtons[8].onClick.AddListener(OnLevelClick8);
            _levelButtons[9].onClick.AddListener(OnLevelClick9);

            _levelButtons[10].onClick.AddListener(OnLevelClick10);
            _levelButtons[11].onClick.AddListener(OnLevelClick11);
            _levelButtons[12].onClick.AddListener(OnLevelClick12);
            _levelButtons[13].onClick.AddListener(OnLevelClick13);
            _levelButtons[14].onClick.AddListener(OnLevelClick14);
            _levelButtons[15].onClick.AddListener(OnLevelClick15);
            _levelButtons[16].onClick.AddListener(OnLevelClick16);
            _levelButtons[17].onClick.AddListener(OnLevelClick17);
            _levelButtons[18].onClick.AddListener(OnLevelClick18);
            _levelButtons[19].onClick.AddListener(OnLevelClick19);

            _levelButtons[20].onClick.AddListener(OnLevelClick20);
            _levelButtons[21].onClick.AddListener(OnLevelClick21);
            _levelButtons[22].onClick.AddListener(OnLevelClick22);
            _levelButtons[23].onClick.AddListener(OnLevelClick23);
            _levelButtons[24].onClick.AddListener(OnLevelClick24);
            _levelButtons[25].onClick.AddListener(OnLevelClick25);
            _levelButtons[26].onClick.AddListener(OnLevelClick26);
            _levelButtons[27].onClick.AddListener(OnLevelClick27);
            _levelButtons[28].onClick.AddListener(OnLevelClick28);
            _levelButtons[29].onClick.AddListener(OnLevelClick29);

            _levelButtons[30].onClick.AddListener(OnLevelClick30);
            _levelButtons[31].onClick.AddListener(OnLevelClick31);
            _levelButtons[32].onClick.AddListener(OnLevelClick32);
            _levelButtons[33].onClick.AddListener(OnLevelClick33);
            _levelButtons[34].onClick.AddListener(OnLevelClick34);
            _levelButtons[35].onClick.AddListener(OnLevelClick35);
            _levelButtons[36].onClick.AddListener(OnLevelClick36);
            _levelButtons[37].onClick.AddListener(OnLevelClick37);
            _levelButtons[38].onClick.AddListener(OnLevelClick38);
            _levelButtons[39].onClick.AddListener(OnLevelClick39);

            _levelButtons[40].onClick.AddListener(OnLevelClick40);
            _levelButtons[41].onClick.AddListener(OnLevelClick41);
            _levelButtons[42].onClick.AddListener(OnLevelClick42);
            _levelButtons[43].onClick.AddListener(OnLevelClick43);
            _levelButtons[44].onClick.AddListener(OnLevelClick44);
            _levelButtons[45].onClick.AddListener(OnLevelClick45);
            _levelButtons[46].onClick.AddListener(OnLevelClick46);
            _levelButtons[47].onClick.AddListener(OnLevelClick47);
            _levelButtons[48].onClick.AddListener(OnLevelClick48);
            _levelButtons[49].onClick.AddListener(OnLevelClick49);
        }

        #region OnLevelClick

        private void OnLevelClick49()
        {
            SetLevel(49);
        }

        private void OnLevelClick48()
        {
            SetLevel(48);
        }

        private void OnLevelClick47()
        {
            SetLevel(47);
        }

        private void OnLevelClick46()
        {
            SetLevel(46);
        }

        private void OnLevelClick45()
        {
            SetLevel(45);
        }

        private void OnLevelClick44()
        {
            SetLevel(44);
        }

        private void OnLevelClick43()
        {
            SetLevel(43);
        }

        private void OnLevelClick42()
        {
            SetLevel(42);
        }

        private void OnLevelClick41()
        {
            SetLevel(41);
        }

        private void OnLevelClick40()
        {
            SetLevel(40);
        }

        private void OnLevelClick39()
        {
            SetLevel(39);
        }

        private void OnLevelClick38()
        {
            SetLevel(38);
        }

        private void OnLevelClick37()
        {
            SetLevel(37);
        }

        private void OnLevelClick36()
        {
            SetLevel(36);
        }

        private void OnLevelClick35()
        {
            SetLevel(35);
        }

        private void OnLevelClick34()
        {
            SetLevel(34);
        }

        private void OnLevelClick33()
        {
            SetLevel(33);
        }

        private void OnLevelClick32()
        {
            SetLevel(32);
        }

        private void OnLevelClick31()
        {
            SetLevel(31);
        }

        private void OnLevelClick30()
        {
            SetLevel(30);
        }

        private void OnLevelClick29()
        {
            SetLevel(29);
        }

        private void OnLevelClick28()
        {
            SetLevel(28);
        }

        private void OnLevelClick27()
        {
            SetLevel(27);
        }

        private void OnLevelClick26()
        {
            SetLevel(26);
        }

        private void OnLevelClick25()
        {
            SetLevel(25);
        }

        private void OnLevelClick24()
        {
            SetLevel(24);
        }

        private void OnLevelClick23()
        {
            SetLevel(23);
        }

        private void OnLevelClick22()
        {
            SetLevel(22);
        }

        private void OnLevelClick21()
        {
            SetLevel(21);
        }

        private void OnLevelClick20()
        {
            SetLevel(20);
        }

        private void OnLevelClick19()
        {
            SetLevel(19);
        }

        private void OnLevelClick18()
        {
            SetLevel(18);
        }

        private void OnLevelClick17()
        {
            SetLevel(17);
        }

        private void OnLevelClick16()
        {
            SetLevel(16);
        }

        private void OnLevelClick15()
        {
            SetLevel(15);
        }

        private void OnLevelClick14()
        {
            SetLevel(14);
        }

        private void OnLevelClick13()
        {
            SetLevel(13);
        }

        private void OnLevelClick12()
        {
            SetLevel(12);
        }

        private void OnLevelClick11()
        {
            SetLevel(11);
        }

        private void OnLevelClick10()
        {
            SetLevel(10);
        }

        private void OnLevelClick9()
        {
            SetLevel(9);
        }

        private void OnLevelClick8()
        {
            SetLevel(8);
        }

        private void OnLevelClick7()
        {
            SetLevel(7);
        }

        private void OnLevelClick6()
        {
            SetLevel(6);
        }

        private void OnLevelClick5()
        {
            SetLevel(5);
        }

        private void OnLevelClick4()
        {
            SetLevel(4);
        }

        private void OnLevelClick3()
        {
            SetLevel(3);
        }

        private void OnLevelClick2()
        {
            SetLevel(2);
        }

        private void OnLevelClick1()
        {
            SetLevel(1);
        }

        private void OnLevelClick0()
        {
            SetLevel(0);
        }
        #endregion

        private void SetLevel(int level)
        {
            _gameLevel.CurrentLevel = level;
        }

        private void ButtonActive()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private void UnsubscribeButton()
        {
            foreach (var levelButton in _levelButtons)
            {
                levelButton.onClick.RemoveAllListeners();
            }

        }

        private LevelMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<LevelMenuView>();
        }
    }
}