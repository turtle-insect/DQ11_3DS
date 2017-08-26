using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class CheckBoxListItem : AllStatus
	{
		private readonly ListBox mList;
		private readonly List<ItemInfo> mInfos;
		private readonly ButtonCheckObserver mButtonCheck;
		private readonly uint mAddress;
		private readonly uint mCount;
		Dictionary<uint, CheckBox> mDict = new Dictionary<uint, CheckBox>();

		public CheckBoxListItem(ListBox list, Button check, Button uncheck, List<ItemInfo> infos, uint address, uint count)
		{
			mList = list;
			mInfos = infos;
			mAddress = address;
			mCount = count;

			mButtonCheck = new ButtonCheckObserver(check, uncheck);
		}

		public override void Init()
		{
			mList.Items.Clear();
			foreach (ItemInfo info in mInfos)
			{
				CheckBox check = new CheckBox();
				check.Content = info;
				mDict.Add(info.ID, check);
				mList.Items.Add(check);

				mButtonCheck.Append(check);
			}
		}

		public override void Open()
		{
			SaveData saveData = SaveData.Instance();
			for(uint i = 0; i < mCount; i++)
			{
				uint value = saveData.ReadNumber(mAddress + i * 2, 2);
				if (!mDict.ContainsKey(value)) continue;
				CheckBox check = mDict[value];
				check.IsChecked = true;
			}
		}

		public override void Save()
		{
			List<ItemInfo> infos = new List<ItemInfo>();
			foreach(var item in mList.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				if (check.IsChecked == false) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				infos.Add(info);
			}

			SaveData saveData = SaveData.Instance();
			saveData.Fill(mAddress, mCount * 2, 0xFF);

			for (int i = 0; i < infos.Count; i++)
			{
				saveData.WriteNumber(mAddress + (uint)i * 2, 2, infos[i].ID);
			}
		}
	}
}
