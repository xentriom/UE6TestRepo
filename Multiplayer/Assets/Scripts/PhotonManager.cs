using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        ConnectToPhoton();
    }

    void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("SandboxRoom", new RoomOptions { MaxPlayers = 5 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("PlayerPrefab", new Vector3(0, 1, 0), Quaternion.identity);
    }
}
