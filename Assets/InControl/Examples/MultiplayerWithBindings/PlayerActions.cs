namespace MultiplayerWithBindingsExample
{
	using InControl;


	public class PlayerActions : PlayerActionSet
	{
		public PlayerAction Green;
		public PlayerAction Red;
		public PlayerAction Blue;
		public PlayerAction Yellow;
		public PlayerAction Left;
		public PlayerAction Right;
		public PlayerAction Up;
		public PlayerAction Down;
        public PlayerAction LeftAim;
        public PlayerAction RightAim;
        public PlayerAction UpAim;
        public PlayerAction DownAim;
        public PlayerTwoAxisAction Rotate;
        public PlayerTwoAxisAction Aim;
        public PlayerAction Start;
        public PlayerAction Select;
        public PlayerAction LB;
        public PlayerAction RB;
        public PlayerAction RT;
        public PlayerAction LT;


		public PlayerActions()
		{
			Green = CreatePlayerAction( "Green" );
			Red = CreatePlayerAction( "Red" );
			Blue = CreatePlayerAction( "Blue" );
			Yellow = CreatePlayerAction( "Yellow" );

			Left = CreatePlayerAction( "Left" );
			Right = CreatePlayerAction( "Right" );
			Up = CreatePlayerAction( "Up" );
			Down = CreatePlayerAction( "Down" );

            LeftAim = CreatePlayerAction("LeftAim");
            RightAim = CreatePlayerAction("RightAim");
            UpAim = CreatePlayerAction("UpAim");
            DownAim = CreatePlayerAction("DownAim");

            Rotate = CreateTwoAxisPlayerAction( Left, Right, Down, Up );
            Aim = CreateTwoAxisPlayerAction(LeftAim, RightAim, DownAim, UpAim);

            Start = CreatePlayerAction("Start");
            Select = CreatePlayerAction("Select");

            LB = CreatePlayerAction("LB");
            RB = CreatePlayerAction("RB");
            LT = CreatePlayerAction("LT");
            RT = CreatePlayerAction("RT");
        }




		public static PlayerActions CreateWithJoystickBindings()
		{
			var actions = new PlayerActions();

			actions.Green.AddDefaultBinding( InputControlType.Action1 );
			actions.Red.AddDefaultBinding( InputControlType.Action2 );
			actions.Blue.AddDefaultBinding( InputControlType.Action3 );
			actions.Yellow.AddDefaultBinding( InputControlType.Action4 );

            actions.LB.AddDefaultBinding(InputControlType.LeftBumper);
            actions.RB.AddDefaultBinding(InputControlType.RightBumper);
            actions.LT.AddDefaultBinding(InputControlType.LeftTrigger);
            actions.RT.AddDefaultBinding(InputControlType.RightTrigger);

            // keyboards bidings
            actions.Green.AddDefaultBinding(Key.DownArrow);
            actions.Red.AddDefaultBinding(Key.UpArrow);
            actions.Blue.AddDefaultBinding(Key.LeftArrow);
            actions.Yellow.AddDefaultBinding(Key.RightArrow);
            actions.LB.AddDefaultBinding(Key.A);
            actions.RB.AddDefaultBinding(Key.S);
            actions.LT.AddDefaultBinding(Key.D);
            actions.RT.AddDefaultBinding(Key.F);

            actions.Start.AddDefaultBinding(InputControlType.Start);
            actions.Select.AddDefaultBinding(InputControlType.Select);

			actions.Up.AddDefaultBinding( InputControlType.LeftStickUp );
			actions.Down.AddDefaultBinding( InputControlType.LeftStickDown );
			actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
			actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );

			actions.UpAim.AddDefaultBinding( InputControlType.RightStickUp );
			actions.DownAim.AddDefaultBinding( InputControlType.RightStickDown);
			actions.LeftAim.AddDefaultBinding( InputControlType.RightStickLeft);
			actions.RightAim.AddDefaultBinding( InputControlType.RightStickRight);

			return actions;
		}
	}
}

