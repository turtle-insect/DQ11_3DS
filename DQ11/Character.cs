using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DQ11
{
	class Character : INotifyPropertyChanged
	{
		private readonly uint mBaseAddress;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<CharItem> Item { get; set; } = new ObservableCollection<CharItem>();
		public List<CharStatus> Status { get; set; } = new List<CharStatus>();

		public Character(uint address)
		{
			mBaseAddress = address;
			for(uint i = 0; i < 24; i++)
			{
				Item.Add(new CharItem(mBaseAddress + 0x24 + i * 2));
			}

			Status.Add(new CharStatus(mBaseAddress + 0x12, 7) { Name = "ゾーン" });
			Status.Add(new CharStatus(mBaseAddress + 0x12, 1) { Name = "どく" });
			Status.Add(new CharStatus(mBaseAddress + 0x12, 2) { Name = "のろい" });
			Status.Add(new CharStatus(mBaseAddress + 0x12, 3) { Name = "のろい" });
			Status.Add(new CharStatus(mBaseAddress + 0x12, 4) { Name = "しに" });
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

		public uint Lv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10, 1);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10, 1, value, 1, 99);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Lv"));
			}
		}

		public uint Exp
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x14, 4);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x14, 4, value, 0, 9999999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Exp"));
			}
		}

		public uint HP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x20, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x20, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HP"));
			}
		}

		public uint MP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x22, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x22, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MP"));
			}
		}

		public uint MaxHP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x100, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x100, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxHP"));
			}
		}

		public uint MaxMP
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x102, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x102, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxMP"));
			}
		}

		public uint AttackMagic
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10C, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10C, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AttackMagic"));
			}
		}

		public uint HealMagic
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10E, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10E, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HealMagic"));
			}
		}

		public uint Attack
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x104, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x104, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Attack"));
			}
		}

		public uint Diffence
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x10A, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x10A, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Diffence"));
			}
		}

		public uint Speed
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x108, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x108, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed"));
			}
		}

		public uint Skillful
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x106, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x106, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skillful"));
			}
		}

		public uint Charm
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x110, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x110, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Charm"));
			}
		}

		public uint Skill
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x112, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x112, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Skill"));
			}
		}

		public uint Strategy
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x1C, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x1C, 1, value);
			}
		}

		public bool Zone
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x12, 1) == 0x80;
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x12, 1, value ? 0x80U : 0);
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

		public void Min()
		{
			Lv = 1;
			Exp = 0;
			HP = 1;
			MP = 0;
			MaxHP = 0;
			MaxMP = 0;
			AttackMagic = 0;
			HealMagic = 0;
			Attack = 0;
			Diffence = 0;
			Speed = 0;
			Skillful = 0;
			Charm = 0;
			Skill = 0;
		}

		public void Max()
		{
			Lv = 99;
			Exp = 9999999;
			HP = 999;
			MP = 999;
			MaxHP = 999;
			MaxMP = 999;
			AttackMagic = 999;
			HealMagic = 999;
			Attack = 999;
			Diffence = 999;
			Speed = 999;
			Skillful = 999;
			Charm = 999;
			Skill = 999;
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
