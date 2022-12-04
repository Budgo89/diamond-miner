using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
   
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    void Update()
    {
        
    }
}
