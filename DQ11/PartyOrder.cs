using System;
using System.Windows.Controls;

namespace DQ11
{
	class PartyOrder : ListStatus
	{
		private readonly ComboBox mParty;
		public PartyOrder(ComboBox party)
		{
			mParty = party;
		}

		public override void Read()
		{
			uint value = SaveData.Instance().ReadNumber(Util.PartyStartAddress + Base, 1);
			if (value == 0xFF) return;
			mParty.SelectedIndex = (int)value;
		}

		public override void Write()
		{
			SaveData.Instance().WriteNumber(Util.PartyStartAddress + Base, 1, (uint)mParty.SelectedIndex);
		}
	}
}
