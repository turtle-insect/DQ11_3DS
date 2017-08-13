using System;
using System.Windows.Controls;

namespace DQ11
{
	class CharSelectStatus : CharStatus
	{
		private readonly ComboBox mValue;
		private readonly uint mAddress;
		private readonly uint mSize;

		public CharSelectStatus(ComboBox value, uint address, uint size)
		{
			mValue = value;
			mAddress = address;
			mSize = size;
		}

		public override void Read()
		{
			uint value = SaveData.Instance().ReadNumber(Base + mAddress, mSize);
			mValue.SelectedIndex = (int)value;
		}

		public override void Write()
		{
			SaveData.Instance().WriteNumber(Base + mAddress, mSize, (uint)mValue.SelectedIndex);
		}
	}
}
