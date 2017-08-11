﻿using System.Windows.Controls;

namespace DQ11
{
	class CharNumberStatus : CharStatus
	{
		private uint mCopy = uint.MaxValue;
		private TextBox mValue;
		private Token mToken;
		private readonly uint mMinValue;
		private readonly uint mMaxValue;

		public CharNumberStatus(TextBox value, uint address, uint size, uint min, uint max)
		{
			mValue = value;
			mToken = new Token(address, size);
			mMinValue = min;
			mMaxValue = max;
		}

		public override void Read()
		{
			uint value = SaveData.Instance().ReadNumber(Address + mToken.Address, mToken.Size);
			mValue.Text = value.ToString();
		}

		public override void Write()
		{
			uint value;
			if (!uint.TryParse(mValue.Text, out value)) return;
			if (value < mMinValue) value = mMinValue;
			if (value > mMaxValue) value = mMaxValue;
			SaveData.Instance().WriteNumber(Address + mToken.Address, mToken.Size, value);
		}

		public override void Copy()
		{
			uint value;
			if (!uint.TryParse(mValue.Text, out value)) return;
			mCopy = value;
		}

		public override void Paste()
		{
			if (mCopy == uint.MaxValue) return;
			mValue.Text = mCopy.ToString();
		}

		public override void Max()
		{
			mValue.Text = mMaxValue.ToString();
		}

		public override void Min()
		{
			mValue.Text = mMinValue.ToString();
		}
	}
}
