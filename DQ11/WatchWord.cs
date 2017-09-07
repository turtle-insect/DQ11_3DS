using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DQ11
{
	class WatchWord : AllStatus
	{
		private readonly ListBox mList;
		private readonly ButtonCheckObserver mButtonCheck;

		public WatchWord(ListBox list, Button check, Button uncheck)
		{
			mList = list;
			mButtonCheck = new ButtonCheckObserver(check, uncheck);
		}

		public override void Init()
		{
			foreach(var info in Item.Instance().WatchWords)
			{
				CheckBox check = new CheckBox();
				check.Content = info;
				mList.Items.Add(check);
				mButtonCheck.Append(check);
			}
		}

		public override void Open()
		{
			SaveData savedate = SaveData.Instance();
			foreach (var item in mList.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				check.IsChecked = savedate.ReadBit(info.ID, info.Count);
			}
		}

		public override void Save()
		{
			SaveData savedate = SaveData.Instance();
			foreach (var item in mList.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				savedate.WriteBit(info.ID, info.Count, check.IsChecked == true);
			}
		}
	}
}
