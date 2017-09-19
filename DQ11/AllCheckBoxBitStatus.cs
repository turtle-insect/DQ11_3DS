using System.Windows.Controls;

namespace DQ11
{
	class AllCheckBoxBitStatus : AllStatus
	{
		private readonly CheckBox mValue;
		private readonly uint mAddress;
		private readonly uint mBit;

		public AllCheckBoxBitStatus(CheckBox value, uint address, uint bit)
		{
			mValue = value;
			mAddress = address;
			mBit = bit;
		}

		public override void Open()
		{
			mValue.IsChecked = SaveData.Instance().ReadBit(mAddress, mBit);
		}

		public override void Save()
		{
			SaveData.Instance().WriteBit(mAddress, mBit, mValue.IsChecked == true);
		}
	}
}
