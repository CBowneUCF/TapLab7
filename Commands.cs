using System;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
	public class Command : IEquatable<Command>
	{
		public string Name { get; set; }

		public string[] Verbs { get; }

		public Action<Game, CommandContext> Action { get; }

		public Command(string name, string verb, Action<Game, CommandContext> action)
			: this(name, new string[] { verb }, action) { }
		
		public Command(string name, IEnumerable<string> verbs, Action<Game, CommandContext> action)
		{
			Name = name;
			Verbs = verbs.ToArray();
			Action = action;
		}

		public static bool operator ==(Command lhs, Command rhs)
		{
			if (ReferenceEquals(lhs, rhs)) return true;
			if (lhs is null || rhs is null) return false;

			return lhs.Name == rhs.Name;
		}
		public static bool operator !=(Command lhs, Command rhs) => !(lhs == rhs);

		public override bool Equals(object obj) => obj is Command ? this == (Command)obj : false;
		public bool Equals(Command other) => this == other;
		public override int GetHashCode() => Name.GetHashCode();
		public override string ToString() => Name;
	}

	public class CommandContext
	{
		public string CommandString { get; }
		public Command Command { get; }
		public CommandContext(string commandString, Command command)
		{
			CommandString = commandString;
			Command = command;
		}
	}

	public class CommandManager
	{
		public CommandManager() => mCommands = new HashSet<Command>();
		public CommandManager(IEnumerable<Command> commands) => mCommands = new HashSet<Command>(commands);
		public CommandContext Parse(string commandString)
		{
			var commandQuery = from command in mCommands
							   where command.Verbs.Contains(commandString, StringComparer.OrdinalIgnoreCase)
							   select new CommandContext(commandString, command);
			var result = commandQuery.FirstOrDefault();
			return (result != null && result.Command != null) ? result : null;
		}
		public bool PerformCommand(Game game, string commandString)
		{
			bool result;
			CommandContext commandContext = Parse(commandString);
			if (commandContext != null && commandContext.Command != null)
			{
				commandContext.Command.Action(game, commandContext);
				result = true;
			}
			else result = false;
			return result;
		}

		public void AddCommand(Command command) => mCommands.Add(command);
		public void RemoveCommand(Command command) => mCommands.Remove(command);
		public void AddCommands(IEnumerable<Command> commands) => mCommands.UnionWith(commands);
		public void ClearCommands() => mCommands.Clear();
		private HashSet<Command> mCommands;
	}



	[CommandClass]
	public static class MovementCommands
	{
		[Command("NORTH", new string[] { "NORTH", "N" })]
		public static void North(Game game, CommandContext commandContext) => Move(game, Directions.North);
		[Command("SOUTH", new string[] { "SOUTH", "S" })]
		public static void South(Game game, CommandContext commandContext) => Move(game, Directions.South);
		[Command("EAST", new string[] { "EAST", "E" })]
		public static void East(Game game, CommandContext commandContext) => Move(game, Directions.East);
		[Command("WEST", new string[] { "WEST", "W" })]
		public static void West(Game game, CommandContext commandContext) => Move(game, Directions.West);
		private static void Move(Game game, Directions direction)
		{
			bool playerMoved = game.Player.Move(direction);
			if (playerMoved == false)
			{
				Console.WriteLine("The way is shut!");
			}
		}
	}
	[CommandClass]
	public static class LookCommand
	{
		[Command("LOOK", new string[] { "LOOK", "L" })]
		public static void Look(Game game, CommandContext commandContext) => Console.WriteLine(game.Player.Location.Description);
	}
	[CommandClass]
	public static class QuitCommand
	{
		[Command("QUIT", new string[] { "QUIT", "Q", "GOODBYE", "BYE" })]
		public static void Quit(Game game, CommandContext commandContext)
		{
			if (game.ConfirmAction("Are you sure you want to quit? "))
			{
				game.Quit();
			}
		}
	}
	[CommandClass]
	public static class RestartCommand
	{
		[Command("RESTART", new string[] { "RESTART", "R", "RESET" })]
		public static void Restart(Game game, CommandContext commandContext)
		{
			if (game.ConfirmAction("Are you sure you want to restart? "))
			{
				game.Restart();
			}
		}
	}

}