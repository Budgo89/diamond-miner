using MB;
using System;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using UnityEngine.AI;
using View;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class DiamondController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/DiamondEnd");
        private List<GameObject> _diamonds;
        private Player _player;

        public Action DiamondRaised;
        private NavMeshSurface2d _navMeshSurface;
        private DiamondEndView _diamondEndView;
        private PauseManager _pauseManager;


        private int _diamondCount;


        public DiamondController(List<GameObject> gameObjects, Player player, NavMeshSurface2d navMeshSurface, PauseManager pauseManager)
        {
            _diamonds = gameObjects;
            _player = player;
            _navMeshSurface = navMeshSurface;
            _diamondCount = _diamonds.Count;
            //this.DiamondRaised += DiamondСheck;
        }

        public DiamondController(List<GameObject> gameObjects, Player player, NavMeshSurface2d navMeshSurface)
        {
            _diamonds = gameObjects;
            _player = player;
            _navMeshSurface = navMeshSurface;
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
                _diamondEndView = LoadView(diamond.gameObject.transform);
                diamond.gameObject.SetActive(false);
                _navMeshSurface.BuildNavMesh();
                DiamondRaised?.Invoke();
                _diamondEndView.AudioSource.Play();
                Object.Destroy(_diamondEndView.GameObject, 1);
            }
        }
        
        internal void RaiseDiamondPlayer2(int sign, Vector2 vector2, Vector2 player)
        {
            var ray = Physics2D.Raycast(player, sign * vector2, 0);
            if (ray.rigidbody == null)
                return;
            var diamond = ray.rigidbody.gameObject.GetComponent<Diamond>();
            if (diamond != null)
            {
                _diamonds.Remove(diamond.gameObject);
                _diamondEndView = LoadView(diamond.gameObject.transform);
                diamond.gameObject.SetActive(false);
                _navMeshSurface.BuildNavMesh();
                DiamondRaised?.Invoke();
                _diamondEndView.AudioSource.Play();
                Object.Destroy(_diamondEndView.GameObject, 1);
            }
        }

        private DiamondEndView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab);
            objectView.transform.position = placeForUi.position;
            AddGameObject(objectView);

            return objectView.GetComponent<DiamondEndView>();
        }
    }
}
