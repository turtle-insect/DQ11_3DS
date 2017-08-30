using System.Windows.Controls;

namespace DQ11
{
	class Technique : AllStatus
	{
		private readonly ListBox mList;
		private readonly ButtonCheckObserver mButtonCheck;
		public Technique(ListBox list, Button check, Button uncheck)
		{
			mList = list;
			mButtonCheck = new ButtonCheckObserver(check, uncheck);
		}

		public override void Init()
		{
			mList.Items.Clear();
			foreach (ItemInfo info in Item.Instance().Techniques)
			{
				CheckBox check = new CheckBox();
				check.Content = info;
				mList.Items.Add(check);

				mButtonCheck.Append(check);
			}
		}

		public override void Open()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var item in mList.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				check.IsChecked = saveData.ReadBit(info.ID, info.Count);
			}
		}

		public override void Save()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var item in mList.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				saveData.WriteBit(info.ID, info.Count, check.IsChecked == true);
			}
		}
	}
}
