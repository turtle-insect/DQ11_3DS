using System.Windows.Controls;

namespace DQ11
{
	class CharChoiceStatus : ListStatus
	{
		private readonly ComboBox mValue;
		private readonly uint mAddress;
		private readonly uint mSize;
		private readonly int mDiff;

		public CharChoiceStatus(ComboBox value, uint address, uint size)
			: this(value, address, size, 0)
		{
		}

		public CharChoiceStatus(ComboBox value, uint address, uint size, int diff)
		{
			mValue = value;
			mAddress = address;
			mSize = size;
			mDiff = diff;
		}

		public override void Read()
		{
			mValue.SelectedIndex = (int)SaveData.Instance().ReadNumber(Base + mAddress, mSize) - mDiff;
		}

		public override void Write()
		{
			SaveData.Instance().WriteNumber(Base + mAddress, mSize, (uint)(mValue.SelectedIndex + mDiff));
		}
	}
}
