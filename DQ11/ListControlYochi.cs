using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DQ11
{
	class ListControlYochi : IListControl
	{
		public void Append(uint index)
		{
			if (index >= Util.YochiCount)
			{
				MessageBox.Show("ヨッチ族の登録数が上限に達しています");
				return;
			}

			Byte[] prof = { 0x4B, 0x30, 0x81, 0x30, 0x80, 0x30, 0x57, 0x30, 0xC3, 0x30, 0xC1, 0x30, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 0x00, 0x01, 0x05, 0x00, 0x00, 0xA4, 0x00,
										0x05, 0x00, 0x1B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };

			if (prof.Length != Util.YochiDateSize)
			{
				// Debug Message.
				MessageBox.Show("ヨッチ族のプロフ確認");
				return;
			}

			uint address = index * Util.YochiDateSize + Util.YochiStartAddress;
			SaveData saveDate = SaveData.Instance();
			for (uint i = 0; i < prof.Length; i++)
			{
				saveDate.WriteNumber(address + i, 1, prof[i]);
			}
			// Random Better ?
			saveDate.WriteNumber(address - 4, 4, index);
		}

		public void Load(ListBox control)
		{
			List<String> names = Util.GetYochiNames();
			for (int i = 0; i < names.Count; i++)
			{
				ListBoxItem item = new ListBoxItem();
				item.Content = names[i];
				control.Items.Add(item);
			}
		}

		public void Remove(uint index)
		{
			SaveData saveDate = SaveData.Instance();
			for (uint i = (uint)index; i < Util.YochiCount - 1; i++)
			{
				saveDate.Copy((i + 1) * Util.YochiDateSize + Util.YochiStartAddress,
										i * Util.YochiDateSize + Util.YochiStartAddress,
										Util.YochiDateSize);
			}
			saveDate.WriteNumber((Util.YochiCount - 1) * Util.YochiDateSize + Util.YochiStartAddress, Util.YochiDateSize, 0);
			saveDate.WriteNumber(Util.YochiCount * Util.YochiDateSize + Util.YochiStartAddress - 4, 4, 0xFFFFFFFF);
		}

		public void RePlace(uint from, uint to)
		{
			from = Util.YochiStartAddress + from * Util.YochiDateSize;
			to = Util.YochiStartAddress + to * Util.YochiDateSize;
			SaveData saveData = SaveData.Instance();
			for (uint i = 0; i < Util.YochiDateSize; i++)
			{
				uint value = saveData.ReadNumber(to + i, 1);
				saveData.WriteNumber(to + i, 1, saveData.ReadNumber(from + i, 1));
				saveData.WriteNumber(from + i, 1, value);
			}
		}
	}
}
