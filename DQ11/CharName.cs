using System.Windows.Controls;

namespace DQ11
{
	class CharName : CharStatus
	{
		private TextBox mName;

		public CharName(TextBox name)
		{
			mName = name;
		}

		public override void Read()
		{
			mName.Text = SaveData.Instance().ReadUnicode(Address + 2, 12);
		}

		public override void Write()
		{
			SaveData.Instance().WriteUnicode(Address + 2, 12, mName.Text);
		}
	}
}
