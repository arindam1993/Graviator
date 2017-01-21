using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using MultiplayerWithBindingsExample;

public class GraviatorPlayerManager : MonoBehaviour {

    public GameObject playerPrefab;

    const int maxPlayers = 4;

    List<GraviatorPlayer> players = new List<GraviatorPlayer>(maxPlayers);
    Stack<int> availablePlayers;

    PlayerActions joystickListener;


    void Awake()
    {
        int[] _ap = { 3, 2, 1, 0 };
        availablePlayers = new Stack<int>(_ap);
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
        if (JoinButtonWasPressedOnListener(joystickListener))
        {
            var inputDevice = InputManager.ActiveDevice;

            if (ThereIsNoPlayerUsingJoystick(inputDevice))
            {
                CreatePlayer(inputDevice);
            }
        }
    }


    bool JoinButtonWasPressedOnListener(PlayerActions actions)
    {
        return actions.Green.WasPressed || actions.Red.WasPressed || actions.Blue.WasPressed || actions.Yellow.WasPressed;
    }


    GraviatorPlayer FindPlayerUsingJoystick(InputDevice inputDevice)
    {
        var playerCount = players.Count;
        for (var i = 0; i < playerCount; i++)
        {
            var player = players[i];
            if (player.Actions.Device == inputDevice)
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
        var player = FindPlayerUsingJoystick(inputDevice);
        if (player != null)
        {
            RemovePlayer(player);
        }
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

            return player;
        }

        return null;
    }


    void RemovePlayer(GraviatorPlayer player)
    {
        availablePlayers.Push(player.PlayerIndex);
        Debug.Log("Player:" + player.PlayerIndex + " Left");
        players.Remove(player);
        player.Actions = null;
        Destroy(player.gameObject);
    }

}
