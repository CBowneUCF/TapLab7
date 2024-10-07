using System;
using System.Collections.Generic;
using static Zork.Program;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{

	class Program
	{

		static void Main(string[] args)
		{
			const string defaultGameFilename = "Zork.json";
			string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename);

			Game.Start(gameFilename);
			Console.WriteLine("Thank you for playing!");
		}

		private enum CommandLineArguments { GameFilename = 0}

		/*
		private enum Fields
		{
			Name = 0,
			Description
		}
		private enum CommandLineArguments
		{
			RoomsFilename = 0
		}

		private static (int Row, int Column) Location = (1, 1);

		private static Room CurrentRoom => Rooms[Location.Row, Location.Column];

		private static Room[,] Rooms;

		private static readonly List<Commands> Directions = new List<Commands> { Commands.NORTH, Commands.SOUTH, Commands.EAST, Commands.WEST };

		static void Main(string[] args)
		{
			const string defaultRoomsFilename = "Rooms.json";
			string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);

			Console.WriteLine("Welcome to Zork!");
			InitializeRooms(roomsFilename);

			Room? previousRoom = null;
			Commands command = Commands.UNKNOWN;
			while (command != Commands.QUIT)
			{
				Console.Write(CurrentRoom + "\n");
				if (previousRoom != CurrentRoom)
				{
					Console.WriteLine(CurrentRoom.Description);
					previousRoom = CurrentRoom;
				}
				Console.Write("> ");
				command = ToCommand(Console.ReadLine().Trim());

				string outputString;
				switch (command)
				{
					case Commands.QUIT:
						outputString = "Thanks for playing!";
						break;

					case Commands.LOOK:
						outputString = CurrentRoom.Description;
						break;

					case Commands.NORTH:
					case Commands.SOUTH:
					case Commands.EAST:
					case Commands.WEST:
						outputString = Move(command) ? $"You moved {command}." : "The way is shut";
						break;

					default:
						outputString = "Unknown command.";
						break;
				}

				Console.WriteLine(outputString);
			}


		}

		private static bool Move(Commands direction)
		{
			Assert.IsTrue(IsDirection(direction), "Invalid direction.");

			bool isValidMove = true;
			switch (direction)
			{
				case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
					Location.Row++;
					break;
				//break;
				case Commands.SOUTH when Location.Row > 0:
					Location.Row--;
					break;
				//break;
				case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
					Location.Column++;
					break;
				//break;
				case Commands.WEST when Location.Column > 0:
					Location.Column--;
					break;
				//break;
				default:
					isValidMove = false;
					break;
			}
			return isValidMove;
		}

		private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
		private static bool IsDirection(Commands command) => Directions.Contains(command);

		public static void InitializeRooms(string roomsFilename) => Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));



		*/
	}

	public static class Assert
	{
		[System.Diagnostics.Conditional("DEBUG")]
		public static void IsTrue(bool expression, string message = null) { if (!expression) throw new Exception(message); }
	}
}