using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class Zoom : AllStatus
	{
		private readonly ListBox mZoom;
		private readonly ButtonCheckObserver mButtonCheck;
		Dictionary<uint, CheckBox> mDict = new Dictionary<uint, CheckBox>();
		public Zoom(ListBox zoom, Button check, Button uncheck)
		{
			mZoom = zoom;
			mButtonCheck = new ButtonCheckObserver(check, uncheck);
		}

		public override void Init()
		{
			mZoom.Items.Clear();
			foreach (ItemInfo info in Item.Instance().Zooms)
			{
				CheckBox check = new CheckBox();
				check.Content = info;
				mDict.Add(info.ID, check);
				mZoom.Items.Add(check);

				mButtonCheck.Append(check);
			}
		}

		public override void Open()
		{
			SaveData saveData = SaveData.Instance();
			foreach(ItemInfo info in Item.Instance().Zooms)
			{
				bool value = saveData.ReadBit(0x78E0 + info.ID / 8, info.ID % 8);
				if (!value) continue;
				if (!mDict.ContainsKey(info.ID)) continue;
				mDict[info.ID].IsChecked = true;
			}
		}

		public override void Save()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var info in Item.Instance().Zooms)
			{
				if (!mDict.ContainsKey(info.ID)) continue;
				saveData.WriteBit(0x78E0 + info.ID / 8, info.ID % 8, mDict[info.ID].IsChecked == true);
			}
		}
	}
}
