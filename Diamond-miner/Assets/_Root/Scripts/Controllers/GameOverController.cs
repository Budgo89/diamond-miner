using System.Collections.Generic;
using MB;
using Profile;
using Tool;
using UnityEngine;

namespace Controllers
{
    internal class GameOverController : BaseController
    {
        private ProfilePlayers _profilePlayer;
        private Player _player;
        private int _diamondCount;
        private DiamondController _diamondController;
        private PauseManager _pauseManager;
        private List<GameObject> _enemys;
        private Collider2D _colliderPlayer;

        public GameOverController(ProfilePlayers profilePlayer, Player player,int diamondCount, DiamondController diamondController, PauseManager pauseManager, List<GameObject> enemys)
        {
            _profilePlayer = profilePlayer;
            _player = player;
            _diamondCount = diamondCount;
            _diamondController = diamondController;
            _pauseManager = pauseManager;
            _enemys = enemys;

            _colliderPlayer = _player.GetComponent<Collider2D>();

            _diamondController.DiamondRaised += DiamondСheck;
            foreach (var emeny in _enemys)
            {
                emeny.gameObject.GetComponent<EnemyView>().EnemyContact += PlayerCheck;
            }
        }

        private void DiamondСheck()
        {
            _diamondCount--;
            if (_diamondCount <= 0)
            {
                _profilePlayer.CurrentState.Value = GameState.FartherMenu;
            }
        }

        private void PlayerCheck(Collider2D collision)
        {
            if (collision == _colliderPlayer)
            {
                _profilePlayer.CurrentState.Value = GameState.GameOverMenu;
            }
        }

        protected override void OnDispose()
        {
            _diamondController.DiamondRaised -= DiamondСheck;
            foreach (var emeny in _enemys)
            {
                emeny.gameObject.GetComponent<EnemyView>().EnemyContact -= PlayerCheck;
            }
        }
    }
}