using System;
using System.Collections.Generic;

namespace DQ11
{
	class Item
	{
		private static Item mThis;
		public ItemInfo None { get; private set; } = new ItemInfo(0xFFFF, "なし", 1);
		public List<ItemInfo> Tools { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Equipments { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Hats { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Titles { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Zooms { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Importants { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Recipes { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Quests { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Storys { get; private set; } = new List<ItemInfo>();
		public List<ItemInfo> Techniques { get; private set; } = new List<ItemInfo>();
		// #で始まる文字はコメント扱い
		// タブ区切りで解釈
		// ID\t名前で区切られている前提として扱う

		private Item()
		{}

		public static Item Instance()
		{
			if(mThis == null)
			{
				mThis = new Item();
				mThis.Init();
			}
			return mThis;
		}

		private void Init()
		{
			AppendList("item\\tool.txt", Tools);
			AppendList("item\\equipment.txt", Equipments);
			AppendList("item\\hat.txt", Hats);
			AppendList("item\\title.txt", Titles);
			AppendList("item\\zoom.txt", Zooms);
			AppendList("item\\important.txt", Importants);
			AppendList("item\\recipe.txt", Recipes);
			AppendList("item\\quest.txt", Quests);
			AppendList("item\\story.txt", Storys);
			AppendList("item\\technique.txt", Techniques);
			Tools.Sort((a, b) => (int)(a.ID - b.ID));
			Equipments.Sort((a, b) => (int)(a.ID - b.ID));
		}

		public ItemInfo SearchBinaryNear(List<ItemInfo> items, uint id)
		{
			int min = 0;
			int max = items.Count;
			for (; min < max;)
			{
				int mid = (min + max) / 2;
				if (items[mid].ID == id) return items[mid];
				else if (items[mid].ID > id) max = mid;
				else min = mid + 1;
			}
			if (min < 1) return null;
			ItemInfo info = items[min - 1];
			if (info.ID < id && info.ID + info.Count > id) return info;
			return null;
		}

		public ItemInfo SearchBinary(List<ItemInfo> items, uint id)
		{
			int min = 0;
			int max = items.Count;
			for(;min < max;)
			{
				int mid = (min + max) / 2;
				if (items[mid].ID == id) return items[mid];
				else if (items[mid].ID > id) max = mid;
				else min = mid + 1;
			}
			return null;
		}

		public ItemInfo GetToolItemInfo(uint id)
		{
			if (None.ID == id) return None;
			return SearchBinary(Tools, id);
		}

		public ItemInfo GetEquipmentInfo(uint id)
		{
			if (None.ID == id) return None;
			return SearchBinaryNear(Equipments, id);
		}

		public ItemInfo GetHatItemInfo(uint id)
		{
			if (None.ID == id) return None;
			return SearchBinary(Tools, id);
		}

		public ItemInfo GetItemInfo(uint id)
		{
			ItemInfo info = GetToolItemInfo(id);
			if (info != null) return info;
			return GetEquipmentInfo(id);
		}

		private void AppendList(String filename, List<ItemInfo> items)
		{
			if (!System.IO.File.Exists(filename)) return;
			String[] lines = System.IO.File.ReadAllLines(filename);
			foreach(String line in lines)
			{
				if (line.Length < 3) continue;
				if (line[0] == '#') continue;
				String[] values = line.Split('\t');
				if (values.Length < 2) continue;
				uint id = 0;
				if (values[0].Length > 1 && values[0][1] == 'x') id = Convert.ToUInt32(values[0], 16);
				else id = Convert.ToUInt32(values[0]);

				uint count = 1;
				if (values.Length >= 3) count = Convert.ToUInt32(values[2]);
				items.Add(new ItemInfo(id, values[1], count));
			}
		}
	}
}
