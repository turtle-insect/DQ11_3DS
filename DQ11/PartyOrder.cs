using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class PartyOrder : AllStatus
	{
		private readonly ComboBox mParty;
		private readonly uint mAddress;
		public PartyOrder(ComboBox party, uint address)
		{
			mParty = party;
			mAddress = address;
		}

		public override void Open()
		{
			mParty.Items.Clear();
			List<String> names = Util.GetPartyNames();
			foreach (String name in names)
			{
				mParty.Items.Add(name);
			}
			mParty.Items.Add("-");
			uint charID = SaveData.Instance().ReadNumber(0x3E04 + mAddress, 1);
			if (charID == 0xFF) charID = (uint)(mParty.Items.Count - 1);
			mParty.SelectedIndex = (int)charID;
		}

		public override void Save()
		{
			//uint charID = (uint)mParty.SelectedIndex;
			//if (charID == mParty.Items.Count - 1) charID = 0xFF;
			//SaveData.Instance().WriteNumber(0x3E04 + mAddress, 1, charID);
		}
	}
}
