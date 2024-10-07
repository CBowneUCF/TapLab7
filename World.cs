using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Zork
{
	public class World
	{
		public HashSet<Room> Rooms { get; set; }

		[JsonIgnore]
		public IReadOnlyDictionary<string, Room> RoomsByName => mRoomsByName;

		public Player SpawnPlayer() => new Player(this, StartingLocation);

		[OnDeserialized]
		public void OnDeserialized(StreamingContext ctx)
		{
			mRoomsByName = Rooms.ToDictionary(room => room.Name, room => room);
			foreach (Room room in Rooms) room.UpdateNeighbors(this);
		}

		[JsonProperty]
		public string StartingLocation { get; set; }

		private Dictionary<string, Room> mRoomsByName;
		
	}
}