using System;
using System.ComponentModel;

namespace DQ11
{
	class Yochi : IListItem, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private readonly uint mBaseAddress;

		public String Name
		{
			get
			{
				return SaveData.Instance().ReadUnicode(mBaseAddress + 0x00, 12);
			}
			set
			{
				SaveData.Instance().WriteUnicode(mBaseAddress + 0x00, 12, value);
			}
		}

		public uint Color
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x7B, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x7B, 1, value);
			}
		}

		public uint Rank
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x7A, 1) - 1;
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x7A, 1, value + 1);
			}
		}

		public uint Motivation
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x78, 2);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x78, 2, value, 1, 999);
			}
		}

		public uint Hat
		{
			get
			{
				uint id = SaveData.Instance().ReadNumber(mBaseAddress + 0x7C, 2);
				if (id == 0) return 0;
				return id - 2722;
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x7C, 2, value == 0 ? 0U : value + 2722);
			}
		}

		public uint Weapon
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x7E, 2);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x7E, 2, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeaponName"));
			}
		}

		public String WeaponName
		{
			get
			{
				uint id = Weapon;
				Item item = Item.Instance();
				ItemInfo info = item.GetEquipmentInfo(id);
				String value = info.Name;
				if (id > info.ID)
				{
					value += " +" + (id - info.ID).ToString();
				}
				return value;
			}
		}

		public uint Info
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x84, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x84, 1, value);
			}
		}

		public uint Boost
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x85, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x85, 1, value);
			}
		}

		public uint FirstPerson
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x80, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x80, 1, value);
			}
		}

		public String First
		{
			get
			{
				return SaveData.Instance().ReadUnicode(mBaseAddress + 0x3C, 12);
			}
			set
			{
				SaveData.Instance().WriteUnicode(mBaseAddress + 0x3C, 12, value);
			}
		}

		public String Second
		{
			get
			{
				return SaveData.Instance().ReadUnicode(mBaseAddress + 0x0E, 12);
			}
			set
			{
				SaveData.Instance().WriteUnicode(mBaseAddress + 0x0E, 12, value);
			}
		}

		public String Third
		{
			get
			{
				return SaveData.Instance().ReadUnicode(mBaseAddress + 0x1C, 12);
			}
			set
			{
				SaveData.Instance().WriteUnicode(mBaseAddress + 0x1C, 12, value);
			}
		}

		public String Four
		{
			get
			{
				return SaveData.Instance().ReadUnicode(mBaseAddress + 0x2A, 12);
			}
			set
			{
				SaveData.Instance().WriteUnicode(mBaseAddress + 0x2A, 12, value);
			}
		}

		public uint ID
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + Util.YochiDateSize - 4, 4);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + Util.YochiDateSize - 4, 4, value);
			}
		}

		public uint PassLv
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x70, 1);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x70, 1, value, 1, 99);
			}
		}

		public uint PassStory
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x6C, 1);
			}
			set
			{
				Util.WriteNumber(mBaseAddress + 0x6C, 1, value, 1, 0xFF);
			}
		}

		public uint PassSex
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x6E, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x6E, 1, value);
			}
		}

		public uint PassGraduate
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x6F, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x6F, 1, value);
			}
		}

		public uint PassAge
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x71, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x71, 1, value);
			}
		}

		public uint PassPersonality
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x72, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x72, 1, value);
			}
		}

		public uint PassHobby
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x73, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x73, 1, value);
			}
		}

		public uint PassHistory
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress + 0x74, 1);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress + 0x74, 1, value);
			}
		}

		public String PassMessage
		{
			get
			{
				return SaveData.Instance().ReadUnicode(mBaseAddress + 0x4A, 32);
			}
			set
			{
				SaveData.Instance().WriteUnicode(mBaseAddress + 0x4A, 32, value);
			}
		}

		public bool PassEscapeNG
		{
			get
			{
				return SaveData.Instance().ReadBit(mBaseAddress + 0x75, 0);
			}

			set
			{
				SaveData.Instance().WriteBit(mBaseAddress + 0x75, 0, value);
			}
		}

		public bool PassShopNG
		{
			get
			{
				return SaveData.Instance().ReadBit(mBaseAddress + 0x75, 1);
			}

			set
			{
				SaveData.Instance().WriteBit(mBaseAddress + 0x75, 1, value);
			}
		}

		public bool PassArmorNG
		{
			get
			{
				return SaveData.Instance().ReadBit(mBaseAddress + 0x75, 2);
			}

			set
			{
				SaveData.Instance().WriteBit(mBaseAddress + 0x75, 2, value);
			}
		}

		public bool PassAshamed
		{
			get
			{
				return SaveData.Instance().ReadBit(mBaseAddress + 0x75, 3);
			}

			set
			{
				SaveData.Instance().WriteBit(mBaseAddress + 0x75, 3, value);
			}
		}





		public Yochi(uint address)
		{
			mBaseAddress = address;
		}

		public uint Address()
		{
			return mBaseAddress;
		}

		public void Clear()
		{
			SaveData savedata = SaveData.Instance();
			savedata.Fill(mBaseAddress, Util.YochiDateSize, 0x00);
			savedata.WriteNumber(mBaseAddress + Util.YochiDateSize - 4, 4, 0xFFFFFFFF);
		}

		public void Create()
		{
			Byte[] prof = { 0x4B, 0x30, 0x81, 0x30, 0x80, 0x30, 0x57, 0x30, 0xC3, 0x30, 0xC1, 0x30, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 0x00, 0x01, 0x05, 0x00, 0x00, 0xA4, 0x00,
										0x05, 0x00, 0x1B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };

			SaveData savedata = SaveData.Instance();
			for (uint i = 0; i < prof.Length; i++)
			{
				savedata.WriteNumber(mBaseAddress + i, 1, prof[i]);
			}
			savedata.WriteNumber(mBaseAddress + Util.YochiDateSize - 4, 4, (uint)new Random().Next());
		}

		public void Remove(IListItem item)
		{
			SaveData savedata = SaveData.Instance();
			savedata.Copy(item.Address(), mBaseAddress, Util.YochiDateSize);
		}

		public void Swap(IListItem item)
		{
			SaveData savedata = SaveData.Instance();
			savedata.Swap(item.Address(), mBaseAddress, Util.YochiDateSize);
		}
	}
}
