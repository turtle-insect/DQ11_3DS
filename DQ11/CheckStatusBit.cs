using System;
using System.Windows.Controls;

namespace DQ11
{
    class CheckStatusBit : ListStatus
    {
		private readonly CheckBox mValue;
		private readonly uint mAddress;
		private readonly uint mBit;

		public CheckStatusBit(CheckBox value, uint address, uint bit)
		{
			mValue = value;
			mAddress = address;
			mBit = bit;
		}
		public override void Read()
		{
			mValue.IsChecked = SaveData.Instance().ReadBit(mAddress + Base, mBit);
		}

		public override void Write()
		{
			SaveData.Instance().WriteBit(mAddress + Base, mBit, mValue.IsChecked == true);
		}
	}
}
