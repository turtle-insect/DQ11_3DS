using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DQ11
{
	class ListControlParty : IListControl
	{
		public void Append(uint index)
		{
			if (index >= Util.CharCount)
			{
				MessageBox.Show("パーティーの登録数が上限に達しています");
				return;
			}

			SaveData.Instance().WriteNumber(Util.PartyStartAddress + index, 1, 0);
		}

		public void Load(ListBox control)
		{
			List<String> names = Util.GetPartyNames();
			for (uint i = 0; i < Util.CharCount; i++)
			{
				uint value = SaveData.Instance().ReadNumber(Util.PartyStartAddress + i, 1);
				if (value == 0xFF) break;
				ListBoxItem item = new ListBoxItem();
				item.Content = names[(int)value];
				control.Items.Add(item);
			}
			SaveData.Instance().WriteNumber(0x6580, 1, (uint)control.Items.Count);
		}

		public void Remove(uint index)
		{
			SaveData saveData = SaveData.Instance();
			for (uint i = index; i < Util.CharCount - 1; i++)
			{
				saveData.WriteNumber(Util.PartyStartAddress + i, 1, saveData.ReadNumber(Util.PartyStartAddress + i + 1, 1));
			}
			saveData.WriteNumber(Util.PartyStartAddress + Util.CharCount - 1, 1, 0xFF);
		}

		public void RePlace(uint from, uint to)
		{
			from = Util.PartyStartAddress + from;
			to = Util.PartyStartAddress + to;
			SaveData saveData = SaveData.Instance();
			uint value = saveData.ReadNumber(to, 1);
			saveData.WriteNumber(to, 1, saveData.ReadNumber(from, 1));
			saveData.WriteNumber(from, 1, value);
		}
	}
}
