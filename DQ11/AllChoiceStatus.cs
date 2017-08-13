using System.Windows.Controls;

namespace DQ11
{
	class AllChoiceStatus : AllStatus
	{
		private readonly ComboBox mValue;
		private readonly uint mAddress;
		private readonly uint mSize;

		public AllChoiceStatus(ComboBox value, uint address, uint size)
		{
			mValue = value;
			mAddress = address;
			mSize = size;
		}
		public override void Open()
		{
			mValue.SelectedIndex = (int)SaveData.Instance().ReadNumber(mAddress, mSize);
		}

		public override void Save()
		{
			SaveData.Instance().WriteNumber(mAddress, mSize, (uint)mValue.SelectedIndex);
		}
	}
}
