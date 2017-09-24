using System;
using System.Collections.Generic;

namespace DQ11
{
	class DataContext
	{
		public List<Character> Char { get; set; } = new List<Character>();
		public ListMediator Party { get; set; } = new ListMediator();
		public ListMediator Yochi { get; set; } = new ListMediator();
		public Item Item { get; } = Item.Instance();
		public List<ItemInfo> YochiHat { get; } = new List<ItemInfo>();

		public uint PlayHour
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E24, 4) / 3600;
			}
			set
			{
				uint number = SaveData.Instance().ReadNumber(0x3E24, 4) % 3600;
				SaveData.Instance().WriteNumber(0x3E24, 4, value * 3600 + number);
			}
		}

		public uint PlayMinute
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E24, 4) % 3600 / 60;
			}
			set
			{
				uint number = SaveData.Instance().ReadNumber(0x3E24, 4);
				number = number / 3600 * 3600 + number % 60;
				SaveData.Instance().WriteNumber(0x3E24, 4, value * 60 + number);
			}
		}

		public uint PlaySecond
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E24, 4) % 60;
			}
			set
			{
				uint number = SaveData.Instance().ReadNumber(0x3E24, 4);
				number = number / 60 * 60;
				SaveData.Instance().WriteNumber(0x3E24, 4, value + number);
			}
		}

		public uint GoldHand
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E28, 4);
			}

			set
			{
				Util.WriteNumber(0x3E28, 4, value, 0, 9999999);
			}
		}

		public uint TotalGold
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E2C, 4);
			}

			set
			{
				Util.WriteNumber(0x3E2C, 4, value, 0, 9999999);
			}
		}

		public uint GoldBank
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6584, 4);
			}

			set
			{
				Util.WriteNumber(0x6584, 4, value, 0, 9999999);
			}
		}

		public uint CasinoCoin
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FF4, 4);
			}

			set
			{
				Util.WriteNumber(0x6FF4, 4, value, 0, 9999999);
			}
		}

		public uint SmallMedal
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FE4, 4);
			}

			set
			{
				Util.WriteNumber(0x6FE4, 4, value, 0, 9999999);
			}
		}

		public uint TotalSmallMedal
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FE8, 4);
			}

			set
			{
				Util.WriteNumber(0x6FE8, 4, value, 0, 9999999);
			}
		}

		public uint DepositSmallMedal
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FF0, 4);
			}

			set
			{
				Util.WriteNumber(0x6FF0, 4, value, 0, 9999999);
			}
		}

		public uint RebuildJewel
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x962C, 4);
			}

			set
			{
				Util.WriteNumber(0x962C, 4, value, 0, 9999999);
			}
		}

		public uint Smith
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67E8, 4);
			}

			set
			{
				Util.WriteNumber(0x67E8, 4, value, 0, 9999999);
			}
		}

		public uint Camp
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67A8, 4);
			}

			set
			{
				Util.WriteNumber(0x67A8, 4, value, 0, 9999999);
			}
		}

		public uint INI
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67AC, 4);
			}

			set
			{
				Util.WriteNumber(0x67AC, 4, value, 0, 9999999);
			}
		}

		public uint Break
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67B4, 4);
			}

			set
			{
				Util.WriteNumber(0x67B4, 4, value, 0, 9999999);
			}
		}

		public uint Cooperation
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6798, 4);
			}

			set
			{
				Util.WriteNumber(0x6798, 4, value, 0, 9999999);
			}
		}

		public uint Slot
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67DC, 4);
			}

			set
			{
				Util.WriteNumber(0x67DC, 4, value, 0, 9999999);
			}
		}

		public uint Poker
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67C4, 4);
			}

			set
			{
				Util.WriteNumber(0x67C4, 4, value, 0, 9999999);
			}
		}

		public uint Roulette
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67D0, 4);
			}

			set
			{
				Util.WriteNumber(0x67D0, 4, value, 0, 9999999);
			}
		}

		public uint KillMonster
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E30, 4);
			}

			set
			{
				Util.WriteNumber(0x3E30, 4, value, 0, 9999999);
			}
		}

		public bool EscapeNG
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6A7F, 1) == 1;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x6A7F, 1, value == true ? 1U : 0);
			}
		}

		public bool ShopNG
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6A80, 1) == 1;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x6A80, 1, value == true ? 1U : 0);
			}
		}

		public bool ArmorNG
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6A81, 1) == 1;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x6A81, 1, value == true ? 1U : 0);
			}
		}

		public bool Ashamed
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6A83, 1) == 1;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x6A83, 1, value == true ? 1U : 0);
			}
		}

		public uint BattleSpeed
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FBF, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FBF, 1, value);
			}
		}

		public uint BGMVolume
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FC0, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC0, 1, value);
			}
		}

		public uint SEVolume
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FC1, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC1, 1, value);
			}
		}

		public uint CameraRotate
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FC2, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC2, 1, value);
			}
		}

		public uint CStickRotate
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FC3, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC3, 1, value);
			}
		}

		public uint ViewMode
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FCC, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FCC, 1, value);
			}
		}

		public uint VillageState
		{
			get
			{
				uint value = SaveData.Instance().ReadNumber(0x6F51, 1);
				uint[] table = { 0xFF, 0x00, 0x03, 0x0B, 0x10 };
				for(int i = 0; i < table.Length; i++)
				{
					if (value == table[i]) return (uint)i;
				}
				return uint.MaxValue;
			}

			set
			{
				uint[] table = { 0xFF, 0x00, 0x03, 0x0B, 0x10 };
				SaveData.Instance().WriteNumber(0x6F51, 1, table[value]);
			}
		}

		public uint VillageHierarchy
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69F1, 1);
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69F1, 1, value);
			}
		}

		public bool VillageDQ1
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69F5, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69F5, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ2
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69F6, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69F6, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ3
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69F7, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69F7, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ4
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69F8, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69F8, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ5
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69F9, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69F9, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ6
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69FA, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69FA, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ7
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69FB, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69FB, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ8
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69FC, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69FC, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ9
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69FD, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69FD, 1, value ? 2U : 0);
			}
		}

		public bool VillageDQ10
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x69FE, 1) == 2;
			}

			set
			{
				SaveData.Instance().WriteNumber(0x69FE, 1, value ? 2U : 0);
			}
		}

		public DataContext()
		{
			YochiHat.Add(Item.Instance().None);
			Item.Instance().Hats.ForEach(x => { YochiHat.Add(x); });
			YochiHat.Add(new ItemInfo(118, "魔王の剣", 0));
			YochiHat.Add(new ItemInfo(119, "テスト用アイテム2", 0));
			YochiHat.Add(new ItemInfo(120, "テスト用アイテム3", 0));
			YochiHat.Add(new ItemInfo(121, "テスト用アイテム4", 0));

			Char.Clear();
			for(uint i = 0; i < Util.CharCount; i++)
			{
				Char.Add(new Character(i * 0x200));
			}


			PartyInit();
			YochiInit();
		}

		public void PartyInit()
		{
			Party.Clear();
			for (uint i = 0; i < Util.CharCount; i++)
			{
				uint address = Util.PartyStartAddress + i;
				uint value = SaveData.Instance().ReadNumber(address, 1);
				if (value == 0xFF) break;
				Party.Append(new Party(Char, address));
			}
		}

		public void PartyAppend()
		{
			uint address = Util.PartyStartAddress + (uint)Party.List.Count;
			Party item = new Party(Char, address);
			item.Create();
			Party.Append(item);
		}

		public void YochiInit()
		{
			Yochi.Clear();
			for (uint i = 0; i < Util.YochiCount; i++)
			{
				uint address = Util.YochiStartAddress + i * Util.YochiDateSize;
				uint value = SaveData.Instance().ReadNumber(address + Util.YochiDateSize - 4, 4);
				if (value == 0xFFFFFFFF) break;
				Yochi.Append(new Yochi(address));
			}
		}

		public void YochiAppend()
		{
			uint address = Util.YochiStartAddress + (uint)Yochi.List.Count * Util.YochiDateSize;
			Yochi item = new Yochi(address);
			item.Create();
			Yochi.Append(item);
		}
	}
}
