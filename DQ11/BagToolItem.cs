using System.Windows.Controls;

namespace DQ11
{
	class BagToolItem : AllStatus
	{
		private ComboBox mItem;
		private TextBox mCount;
		private uint mAddress;

		public BagToolItem(ComboBox item, TextBox count, uint address)
		{
			mItem = item;
			mCount = count;
			mAddress = address;
		}

		public override void Init()
		{
			Item item = Item.Instance();
			mItem.Items.Add(item.None);
			foreach (ItemInfo info in item.Tools)
			{
				mItem.Items.Add(info);
			}
		}

		public override void Open()
		{
			SaveData saveData = SaveData.Instance();
			uint id = saveData.ReadNumber(mAddress, 2);
			Item item = Item.Instance();
			ItemInfo info = item.GetToolItemInfo(id);
			if (info == null)
			{
				mItem.Items.Add("不明" + id.ToString());
				mItem.SelectedIndex = mItem.Items.Count - 1;
			}
			else
			{
				mItem.Text = info.Name;
			}

			uint count = saveData.ReadNumber(mAddress + 2, 2);
			mCount.Text = count.ToString();
		}

		public override void Save()
		{
			ItemInfo info = mItem.SelectedItem as ItemInfo;
			if (info == null) return;
			SaveData saveData = SaveData.Instance();
			saveData.WriteNumber(mAddress, 2, info.ID);

			uint count;
			if (uint.TryParse(mCount.Text, out count) == false) return;
			if (count < 0) count = 0;
			if (count > 99) count = 99;
			if (info == Item.Instance().None) count = 0;
			saveData.WriteNumber(mAddress + 2, 2, count);
		}
	}
}
