using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DQ11
{
    class AllStringStatus : AllStatus
    {
		private readonly TextBox mValue;
		private readonly uint mAddress;
		private readonly uint mLen;

		public AllStringStatus(TextBox value, uint address, uint len)
		{
			mValue = value;
			mAddress = address;
			mLen = len;
		}

		public override void Open()
		{
			mValue.Text = SaveData.Instance().ReadUnicode(mAddress, mLen * 2);
		}

		public override void Save()
		{
			SaveData.Instance().WriteUnicode(mAddress, mLen * 2, mValue.Text);
		}
	}
}
