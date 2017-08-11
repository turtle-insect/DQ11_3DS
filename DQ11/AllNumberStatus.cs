using System.Windows.Controls;

namespace DQ11
{
	class AllNumberStatus : AllStatus
	{
		private readonly TextBox mValue;
		private readonly uint mAddress;
		private readonly uint mSize;
		private readonly uint mMinValue;
		private readonly uint mMaxValue;

		public AllNumberStatus(TextBox value, uint address, uint size, uint min, uint max)
		{
			mValue = value;
			mAddress = address;
			mSize = size;
			mMinValue = min;
			mMaxValue = max;
		}
		public override void Open()
		{
			mValue.Text = SaveData.Instance().ReadNumber(mAddress, mSize).ToString();
		}

		public override void Save()
		{
			uint value;
			if (!uint.TryParse(mValue.Text, out value)) return;
			if (value < mMinValue) value = mMinValue;
			if (value > mMaxValue) value = mMaxValue;
			SaveData.Instance().WriteNumber(mAddress, mSize, value);
		}
	}
}
