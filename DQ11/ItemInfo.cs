using System;

namespace DQ11
{
	class ItemInfo
	{
		public ItemInfo(uint id, String name, uint count)
		{
			ID = id;
			Name = name;
			Count = count;
		}

		public uint ID { get; set; }
		public String Name { get; set; }
		public uint Count { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
