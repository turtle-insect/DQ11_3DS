using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DQ11
{
	class Character
	{
		private readonly uint mBaseAddress;

		public ObservableCollection<CharItem> Item { get; set; } = new ObservableCollection<CharItem>();

		public Character(uint address)
		{
			mBaseAddress = address;
			for(uint i = 0; i < 24; i++)
			{
				Item.Add(new CharItem(mBaseAddress + 0x24 + i * 2));
			}
		}
		public String Name
		{
			get
			{
				return SaveData.Instance().ReadUnicode(mBaseAddress + 0x02, 12);
			}
			set
			{
				SaveData.Instance().WriteUnicode(mBaseAddress + 0x02, 12, value);
			}
		}

		public String Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10, 1).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10, 1, value, 1, 99);
			}
		}

		public String Exp
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x14, 4).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x14, 4, value, 0, 9999999);
			}
		}

		public String HP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x20, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x20, 2, value, 0, 999);
			}
		}

		public String MP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x22, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x22, 2, value, 0, 999);
			}
		}

		public String MaxHP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x100, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x100, 2, value, 0, 999);
			}
		}

		public String MaxMP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x102, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x102, 2, value, 0, 999);
			}
		}

		public String AttackMagic
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10C, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10C, 2, value, 0, 999);
			}
		}

		public String HealMagic
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10E, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10E, 2, value, 0, 999);
			}
		}

		public String Attack
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x104, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x104, 2, value, 0, 999);
			}
		}

		public String Diffence
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10A, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10A, 2, value, 0, 999);
			}
		}

		public String Speed
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x108, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x108, 2, value, 0, 999);
			}
		}

		public String Skillful
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x106, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x106, 2, value, 0, 999);
			}
		}

		public String Charm
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x110, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x110, 2, value, 0, 999);
			}
		}

		public String Skill
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x112, 2).ToString();
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x112, 2, value, 0, 999);
			}
		}

		public int Strategy
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(mBaseAddress + 0x1C, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x1C, 1, (uint)value);
			}
		}

		public int EquipRightHand
		{
			get
			{
				return Equip(0x54);
			}
			set
			{
				Equip(0x54, value);
			}
		}

		public int EquipLeftHand
		{
			get
			{
				return Equip(0x55);
			}
			set
			{
				Equip(0x55, value);
			}
		}

		public int EquipHead
		{
			get
			{
				return Equip(0x56);
			}
			set
			{
				Equip(0x56, value);
			}
		}

		public int EquipBody
		{
			get
			{
				return Equip(0x57);
			}
			set
			{
				Equip(0x57, value);
			}
		}

		public int EquipAccessory1
		{
			get
			{
				return Equip(0x58);
			}
			set
			{
				Equip(0x58, value);
			}
		}

		public int EquipAccessory2
		{
			get
			{
				return Equip(0x59);
			}
			set
			{
				Equip(0x59, value);
			}
		}

		private void Equip(uint address, int value)
		{
			if (value == -1) value = 0xFF;
			SaveData.Instance().WriteNumber(mBaseAddress + address, 1, (uint)value);
		}

		private int Equip(uint address)
		{
			int value = (int)SaveData.Instance().ReadNumber(mBaseAddress + address, 1);
			if (value == 0xFF) value = -1;
			return value;
		}
	}
}
