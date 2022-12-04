using System.Collections;
using System.Collections.Generic;
using MB;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPhoton : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform[] _position;

    void Start()
    {
        Vector3 pos = GetPositionSpawn();

        PhotonNetwork.Instantiate(_playerPrefab.name, pos, Quaternion.identity);
    }
    
    void Update()
    {
        
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    private Vector3 GetPositionSpawn()
    {
        var ray = Physics2D.Raycast(Camera.main.transform.position, _position[0].position);
        if (ray.rigidbody == null)
            return _position[0].position;
        var player = ray.rigidbody.gameObject.GetComponent<Player>();
        if (player != null)
            return _position[1].position;
        return _position[0].position;
    }

}
