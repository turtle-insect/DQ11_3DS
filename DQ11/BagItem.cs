using System.Windows.Controls;

namespace DQ11
{
	class BagItem : AllStatus
	{
		private readonly Label mItem;
		private readonly TextBox mCount;
		private readonly uint mAddress;
		private readonly uint mNumber;
		private uint mPage;

		public BagItem(Label item, TextBox count, uint address, uint number)
		{
			mItem = item;
			mCount = count;
			mAddress = address;
			mNumber = number;
		}

		public override void Open()
		{
			uint address = mAddress + mPage * 12 * 4 + mNumber;
			SaveData saveData = SaveData.Instance();
			uint id = saveData.ReadNumber(address, 2);
			Item item = Item.Instance();
			ItemInfo info = item.GetItemInfo(id);
			mItem.Content = info.Name;
			if(info.Count > 0 && id - info.ID > 0)
			{
				mItem.Content += " +" + (id - info.ID).ToString();
			}

			uint count = saveData.ReadNumber(address + 2, 2);
			mCount.Text = count.ToString();
		}

		public override void Save()
		{
			uint count;
			if (!uint.TryParse(mCount.Text, out count)) return;

			uint address = mAddress + mPage * 12 * 4 + mNumber;
			SaveData saveData = SaveData.Instance();
			saveData.WriteNumber(address + 2, 2, count);
		}

		public override void Page(uint page)
		{
			mPage = page;
		}
	}
}
