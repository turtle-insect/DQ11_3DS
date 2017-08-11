using System.Windows.Controls;

namespace DQ11
{
	class AllChoiceStatus : AllStatus
	{
		private ComboBox mValue;
		private uint mAddress;
		public AllChoiceStatus(ComboBox value, uint address)
		{
			mValue = value;
			mAddress = address;
		}
		public override void Open()
		{
			mValue.SelectedIndex = (int)SaveData.Instance().ReadNumber(mAddress, 1);
		}

		public override void Save()
		{
			uint value = (uint)mValue.SelectedIndex;
			SaveData.Instance().WriteNumber(mAddress, 1, value);
		}
	}
}
