using Photon.Pun;

public class PhotonManager : MonoBehaviourPunCallbacks
{
   
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }
    
}
