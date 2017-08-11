using System.Windows.Controls;

namespace DQ11
{
	class AllCheckBoxStatus : AllStatus
	{
		private CheckBox mValue;
		private uint mAddress;

		public AllCheckBoxStatus(CheckBox value, uint address)
		{
			mValue = value;
			mAddress = address;
		}

		public override void Open()
		{
			uint value = SaveData.Instance().ReadNumber(mAddress, 1);
			mValue.IsChecked = value != 0;
		}

		public override void Save()
		{
			uint value = 0;
			if (mValue.IsChecked == true) value = 1;
			SaveData.Instance().WriteNumber(mAddress, 1, value);
		}
	}
}
