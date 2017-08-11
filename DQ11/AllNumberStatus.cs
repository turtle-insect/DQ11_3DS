using System.Windows.Controls;

namespace DQ11
{
	class AllNumberStatus : AllStatus
	{
		private TextBox mValue;
		private Token mToken;
		private uint mMinValue;
		private uint mMaxValue;

		public AllNumberStatus(TextBox value, uint address, uint size, uint min, uint max)
		{
			mValue = value;
			mToken = new Token(address, size);
			mMinValue = min;
			mMaxValue = max;
		}
		public override void Open()
		{
			mValue.Text = SaveData.Instance().ReadNumber(mToken.Address, mToken.Size).ToString();
		}

		public override void Save()
		{
			uint value;
			if (!uint.TryParse(mValue.Text, out value)) return;
			if (value < mMinValue) value = mMinValue;
			if (value > mMaxValue) value = mMaxValue;
			SaveData.Instance().WriteNumber(mToken.Address, mToken.Size, value);
		}
	}
}
