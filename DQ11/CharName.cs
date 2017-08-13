using System.Windows.Controls;

namespace DQ11
{
	class CharName : CharStatus
	{
		private readonly TextBox mName;
		private readonly uint mAddress;

		public CharName(TextBox name, uint address)
		{
			mName = name;
			mAddress = address;
		}

		public override void Read()
		{
			mName.Text = SaveData.Instance().ReadUnicode(Base + mAddress, 12);
		}

		public override void Write()
		{
			SaveData.Instance().WriteUnicode(Base + mAddress, 12, mName.Text);
		}
	}
}
