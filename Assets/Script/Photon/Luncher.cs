using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Luncher : MonoBehaviourPunCallbacks
{
    public static Luncher instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TextMeshProUGUI errorText, roomName;
    public GameObject mainMenu, loading, roomMenu, errorMenu, creatRoomMenu,findRoomMenu;
    [SerializeField] Transform roomListContent , PlayerListContent;
    [SerializeField] GameObject roomListItemPrefab, palyerListItemPrefab;
    [SerializeField] GameObject PlayGameButton;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        loading.SetActive(true);
        Debug.Log("Connected To Master using setting");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        mainMenu.SetActive(true);
        loading.SetActive(false);
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("000");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        loading.SetActive(true);
        findRoomMenu.SetActive(false);
       

    }
    public void CreatRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        creatRoomMenu.SetActive(false);
        loading.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        loading.SetActive(false);
        roomMenu.SetActive(true);
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform item in PlayerListContent)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(palyerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        PlayGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PlayGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMenu.SetActive(true);
        mainMenu.SetActive(false);
        loading.SetActive(false);
        roomMenu.SetActive(false);
        findRoomMenu.SetActive(false);
        creatRoomMenu.SetActive(false);
        errorText.text = "Room Creation Faild : " + message;
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(palyerListItemPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
   

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
