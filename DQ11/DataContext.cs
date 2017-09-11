using System;
using System.Collections.Generic;

namespace DQ11
{
	class DataContext
	{
		public List<Character> Char { get; set; } = new List<Character>();

		public String PlayHour
		{
			get
			{
				return (SaveData.Instance().ReadNumber(0x3E24, 4) / 3600).ToString();
			}
			set
			{
				uint hour;
				if (!uint.TryParse(value, out hour)) return;
				uint number = SaveData.Instance().ReadNumber(0x3E24, 4) % 3600;
				SaveData.Instance().WriteNumber(0x3E24, 4, hour * 3600 + number);
			}
		}

		public String PlayMinute
		{
			get
			{
				return (SaveData.Instance().ReadNumber(0x3E24, 4) % 3600 / 60).ToString();
			}
			set
			{
				uint minute;
				if (!uint.TryParse(value, out minute)) return;
				uint number = SaveData.Instance().ReadNumber(0x3E24, 4);
				number = number / 3600 * 3600 + number % 60;
				SaveData.Instance().WriteNumber(0x3E24, 4, minute * 60 + number);
			}
		}

		public String PlaySecond
		{
			get
			{
				return (SaveData.Instance().ReadNumber(0x3E24, 4) % 60).ToString();
			}
			set
			{
				uint second;
				if (!uint.TryParse(value, out second)) return;
				uint number = SaveData.Instance().ReadNumber(0x3E24, 4);
				number = number / 60 * 60;
				SaveData.Instance().WriteNumber(0x3E24, 4, second + number);
			}
		}

		public String GoldHand
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E28, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x3E28, 4, value, 0, 9999999);
			}
		}

		public String TotalGold
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E2C, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x3E2C, 4, value, 0, 9999999);
			}
		}

		public String GoldBank
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6584, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x6584, 4, value, 0, 9999999);
			}
		}

		public String CasinoCoin
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FF4, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x6FF4, 4, value, 0, 9999999);
			}
		}

		public String SmallMedal
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FE4, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x6FE4, 4, value, 0, 9999999);
			}
		}

		public String TotalSmallMedal
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FE8, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x6FE8, 4, value, 0, 9999999);
			}
		}

		public String DepositSmallMedal
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6FF0, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x6FF0, 4, value, 0, 9999999);
			}
		}

		public String RebuildJewel
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x962C, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x962C, 4, value, 0, 9999999);
			}
		}

		public String BlackSmith
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67E8, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x67E8, 4, value, 0, 9999999);
			}
		}

		public String Camp
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67A8, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x67A8, 4, value, 0, 9999999);
			}
		}

		public String INI
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67AC, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x67AC, 4, value, 0, 9999999);
			}
		}

		public String Break
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67B4, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x67B4, 4, value, 0, 9999999);
			}
		}

		public String Cooperation
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x6798, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x6798, 4, value, 0, 9999999);
			}
		}

		public String Slot
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67DC, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x67DC, 4, value, 0, 9999999);
			}
		}

		public String Poker
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67C4, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x67C4, 4, value, 0, 9999999);
			}
		}

		public String Roulette
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x67D0, 4).ToString();
			}

			set
			{
				Util.WriteNumber(0x67D0, 4, value, 0, 9999999);
			}
		}

		public String KillMonster
		{
			get
			{
				return SaveData.Instance().ReadNumber(0x3E30, 4).ToString();
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

		public int BattleSpeed
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(0x6FBF, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FBF, 1, (uint)value);
			}
		}

		public int BGMVolume
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(0x6FC0, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC0, 1, (uint)value);
			}
		}

		public int SEVolume
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(0x6FC1, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC1, 1, (uint)value);
			}
		}

		public int CameraRotate
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(0x6FC2, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC2, 1, (uint)value);
			}
		}

		public int CStickRotate
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(0x6FC3, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FC3, 1, (uint)value);
			}
		}

		public int ViewMode
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(0x6FCC, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(0x6FCC, 1, (uint)value);
			}
		}

		public DataContext()
		{
			Char.Clear();
			for(uint i = 0; i < Util.CharCount; i++)
			{
				Char.Add(new Character(i * 0x200));
			}
		}
	}
}
