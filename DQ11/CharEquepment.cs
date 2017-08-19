using System.Windows.Controls;

namespace DQ11
{
	class CharEquepment : ListStatus
	{
		private readonly ComboBox mValue;
		private readonly uint mAddress;
		public CharEquepment(ComboBox value, uint address)
		{
			mValue = value;
			mAddress = address;
		}

		public override void Init()
		{
			mValue.Items.Clear();
			for(int i = 0; i < 24; i++)
			{
				mValue.Items.Add("持ち物" + (i+1).ToString() + "個目");
			}
			mValue.Items.Add("装備しない");
		}

		public override void Read()
		{
			uint value = SaveData.Instance().ReadNumber(Base + mAddress, 1);
			if (value == 0xFF) value = (uint)mValue.Items.Count - 1;
			mValue.SelectedIndex = (int)value;
		}

		public override void Write()
		{
			uint value = (uint)mValue.SelectedIndex;
			if (value == (uint)mValue.Items.Count - 1) value = 0xFF;
			SaveData.Instance().WriteNumber(Base + mAddress, 1, value);
		}
	}
}
