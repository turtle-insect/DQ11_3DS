using System;
using System.ComponentModel;

namespace DQ11
{
	class CharItem : INotifyPropertyChanged
	{
		private readonly uint mBaseAddress;

		public CharItem(uint address)
		{
			mBaseAddress = address;
		}
		public uint ID
		{
			get
			{
				return SaveData.Instance().ReadNumber(mBaseAddress, 2);
			}
			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress, 2, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
			}
		}

		public String Name
		{
			get
			{
				uint id = ID;
				Item item = Item.Instance();
				ItemInfo info = item.GetItemInfo(id);
				String value = info.Name;
				if(id > info.ID)
				{
					value += " +" + (id - info.ID).ToString();
				}
				return value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
