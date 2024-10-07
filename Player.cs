using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Zork
{
	public class Player
	{
		public World World { get; }

		[JsonIgnore]
		public Room Location { get; private set; }

		[JsonIgnore]
		public string LocationName
		{
			get
			{
				return Location?.Name;
			}
			set
			{
				Location = World?.RoomsByName.GetValueOrDefault(value);
			}
		}

		public Player(World world, string startingLocation)
		{
            World = world;
			LocationName = startingLocation;
		}

		public bool Move(Directions direction)
		{
			if(Location.Neighbors.TryGetValue(direction, out Room destination))
			{
				Location = destination;
				return true;
			}
				
			return false;
        }

		public int Moves { get; set; }
	}
}