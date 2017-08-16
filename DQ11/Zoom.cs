using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DQ11
{
	class Zoom : AllStatus
	{
		private readonly ListBox mZoom;
		public Zoom(ListBox zoom)
		{
			mZoom = zoom;
		}

		public override void Open()
		{
			mZoom.Items.Clear();
			SaveData saveData = SaveData.Instance();
			foreach(ItemInfo info in Item.Instance().Zooms)
			{
				CheckBox check = new CheckBox();
				check.Content = info;
				check.IsChecked = saveData.ReadBit(0x78E0 + info.ID / 8, info.ID % 8);
				mZoom.Items.Add(check);
			}
		}

		public override void Save()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var item in mZoom.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				saveData.WriteBit(0x78E0 + info.ID / 8, info.ID % 8, check.IsChecked == true);
			}
		}
	}
}
