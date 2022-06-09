using MB;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    internal class DiamondController : BaseController
    {
        private List<GameObject> _diamonds;
        private Player _player;

        public DiamondController(List<GameObject> gameObjects, Player player)
        {
            _diamonds = gameObjects;
            _player = player;
        }

        internal void RaiseDiamond(int sign, Vector2 vector2)
        {
            var ray = Physics2D.Raycast(_player.transform.position, sign * vector2, 0);
            if (ray.rigidbody == null)
                return;
            var diamond = ray.rigidbody.gameObject.GetComponent<Diamond>();
            if (diamond != null)
            {
                _diamonds.Remove(diamond.gameObject);
                diamond.gameObject.SetActive(false);
            }
        }
    }
}
