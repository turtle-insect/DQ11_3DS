using System.Windows.Controls;

namespace DQ11
{
	class CharStringStatus : ListStatus
	{
		private readonly TextBox mName;
		private readonly uint mAddress;
		private readonly uint mSize;

		public CharStringStatus(TextBox name, uint address, uint size)
		{
			mName = name;
			mAddress = address;
			mSize = size * 2;
		}

		public override void Read()
		{
			mName.Text = SaveData.Instance().ReadUnicode(Base + mAddress, mSize);
		}

		public override void Write()
		{
			SaveData.Instance().WriteUnicode(Base + mAddress, mSize, mName.Text);
		}
	}
}
