using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using InControl;
using MultiplayerWithBindingsExample;

public class GraviatorPlayerManager : MonoBehaviour {

    public GameObject playerPrefab;

    const int maxPlayers = 4;

    List<GraviatorPlayer> players = new List<GraviatorPlayer>(maxPlayers);
    Stack<int> availablePlayers;

    PlayerActions joystickListener;

    public static GraviatorPlayerManager instance;

    public class PlayerData
    {
        public PlayerActions Action;
        public int PlayerIndex;
    }

    public List<PlayerData> joinedPlayers;

    public GameObject MainMenuCanvas;

    bool gameStarted = false;

    public AudioClip gameMusic;
    public AudioMixerGroup targetMixerGroup;

    void Awake()
    {
        int[] _ap = { 3, 2, 1, 0 };
        availablePlayers = new Stack<int>(_ap);
        joinedPlayers = new List<PlayerData>();

        instance = this;
    }

    void OnEnable()
    {
        InputManager.OnDeviceDetached += OnDeviceDetached;
        joystickListener = PlayerActions.CreateWithJoystickBindings();
    }


    void OnDisable()
    {
        InputManager.OnDeviceDetached -= OnDeviceDetached;
        joystickListener.Destroy();
    }


    void Update()
    {
        if(!gameStarted)
        {
            if (JoinButtonWasPressedOnListener(joystickListener))
            {
                var inputDevice = InputManager.ActiveDevice;

                if (ThereIsNoPlayerUsingJoystick(inputDevice))
                {
                    AddPlayer(inputDevice);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if( joinedPlayers.Count > 1)
                {
                    Debug.Log("Start button pressed");
                    MainMenuCanvas.SetActive(false);
                    gameStarted = true;

                    MusicManager.CrossFade(gameMusic, targetMixerGroup);
                    UnityTimer.Instance.CallAfterDelay( ()=>{
                        CreateJoinedPlayers();
                    }, 1.0f);
                }
                
            }
        }
       
    }


    bool JoinButtonWasPressedOnListener(PlayerActions actions)
    {
        return actions.Green.WasPressed || actions.Red.WasPressed || actions.Blue.WasPressed || actions.Yellow.WasPressed;
    }


    PlayerData FindPlayerUsingJoystick(InputDevice inputDevice)
    {
        var playerCount = joinedPlayers.Count;
        for (var i = 0; i < playerCount; i++)
        {
            var player = joinedPlayers[i];
            if (player.Action.Device == inputDevice)
            {
                return player;
            }
        }

        return null;
    }


    bool ThereIsNoPlayerUsingJoystick(InputDevice inputDevice)
    {
        return FindPlayerUsingJoystick(inputDevice) == null;
    }




    void OnDeviceDetached(InputDevice inputDevice)
    {
        //var player = FindPlayerUsingJoystick(inputDevice);
        //if (player != null)
        //{
        //    RemovePlayer(player);
        //}
    }


    GraviatorPlayer CreatePlayer(InputDevice inputDevice)
    {
        if (players.Count < maxPlayers)
        {
            
            // Pop a position off the list. We'll add it back if the player is removed.
            int availablePlayer = availablePlayers.Pop();
            Debug.Log("Player:" + availablePlayer + " Joined");
            var playerPosition = SpawnPoints.Instance.GetSpawnPoint(availablePlayer);

            var gameObject = (GameObject)Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            var player = gameObject.GetComponent<GraviatorPlayer>();

            // Create a new instance and specifically set it to listen to the
            // given input device (joystick).
            var actions = PlayerActions.CreateWithJoystickBindings();
            actions.Device = inputDevice;

            player.Actions = actions;


            player.PlayerIndex = availablePlayer;

            players.Add(player);

            //Debug.Log("Player" + (availablePlayer + 1));
            //gameObject.layer = LayerMask.NameToLayer("Player"+(availablePlayer+1));

            return player;
        }

        return null;
    }

    void AddPlayer(InputDevice inputDevice)
    {
        if (players.Count < maxPlayers)
        {

            // Pop a position off the list. We'll add it back if the player is removed.
            int availablePlayer = availablePlayers.Pop();
            Debug.Log("Player:" + availablePlayer + " Joined");

            var actions = PlayerActions.CreateWithJoystickBindings();
            actions.Device = inputDevice;

            PlayerData pD = new PlayerData()
            {
                Action = actions,
                PlayerIndex = availablePlayer
            };

            joinedPlayers.Add(pD);

            //var playerPosition = SpawnPoints.Instance.GetSpawnPoint(availablePlayer);

            //var gameObject = (GameObject)Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            //var player = gameObject.GetComponent<GraviatorPlayer>();

            //// Create a new instance and specifically set it to listen to the
            //// given input device (joystick).
            //var actions = PlayerActions.CreateWithJoystickBindings();
            //actions.Device = inputDevice;

            //player.Actions = actions;


            //player.PlayerIndex = availablePlayer;

            //players.Add(player);

            //Debug.Log("Player" + (availablePlayer + 1));
            //gameObject.layer = LayerMask.NameToLayer("Player"+(availablePlayer+1));

        }


    }

    public void CreateJoinedPlayers()
    {
        foreach(PlayerData pD in joinedPlayers)
        {
            var playerPosition = SpawnPoints.Instance.GetSpawnPoint(pD.PlayerIndex);

            var gameObject = (GameObject)Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            var player = gameObject.GetComponent<GraviatorPlayer>();

            // Create a new instance and specifically set it to listen to the
            // given input device (joystick).
          
            player.Actions = pD.Action;


            player.PlayerIndex = pD.PlayerIndex;

            players.Add(player);
        }
    }



    void RemovePlayer(GraviatorPlayer player)
    {
        availablePlayers.Push(player.PlayerIndex);
        UIManager.Instance.energyBars[player.PlayerIndex].Hide();
        Debug.Log("Player:" + player.PlayerIndex + " Left");
        players.Remove(player);
        player.Actions = null;
        Destroy(player.gameObject);
    }

}
