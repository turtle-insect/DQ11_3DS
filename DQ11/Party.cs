using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DQ11
{
	class Party : IListItem ,INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private const uint mSize = 1;
		private readonly List<Character> mChars;
		private readonly uint mBaseAddress;

		public String Name
		{
			get
			{
				return mChars[ID].Name;
			}
		}
		public int ID
		{
			get
			{
				return (int)SaveData.Instance().ReadNumber(mBaseAddress, mSize);
			}

			set
			{
				SaveData.Instance().WriteNumber(mBaseAddress, mSize, (uint)value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
			}
		}
		public Party(List<Character> chars, uint address)
		{
			mChars = chars;
			mBaseAddress = address;
		}

		public uint Address()
		{
			return mBaseAddress;
		}

		public void Remove(IListItem item)
		{
			SaveData savedate = SaveData.Instance();
			savedate.WriteNumber(mBaseAddress, mSize, savedate.ReadNumber(item.Address(), mSize));
		}

		public void Create()
		{
			SaveData.Instance().WriteNumber(mBaseAddress, mSize, 0x00);
		}

		public void Clear()
		{
			SaveData.Instance().WriteNumber(mBaseAddress, mSize, 0xFF);
		}

		public void Swap(IListItem item)
		{
			SaveData savedate = SaveData.Instance();
			savedate.Swap(Address(), item.Address(), mSize);
		}
	}
}
