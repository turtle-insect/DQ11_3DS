using System.Windows.Controls;

namespace DQ11
{
	class CharItem : ListStatus
	{
		private readonly ComboBox mPage;
		private readonly Label mItem;
		private readonly uint mAddress;
		private ItemInfo mInfo;
		private uint mKind;

		public CharItem(ComboBox page, Label item, Button change, uint address)
		{
			mPage = page;
			mItem = item;
			mAddress = address;

			change.Click += ItemChange_Click;
		}

		private void ItemChange_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			var dialog = new ItemSelectWindow();
			dialog.Info = mInfo;
			dialog.Kind = mKind;
			dialog.ShowDialog();
			if(dialog.Info != null)
			{
				mInfo = dialog.Info;
				mKind = dialog.Kind;
				setItemName();
			}
		}

		public override void Read()
		{
			uint id = SaveData.Instance().ReadNumber((uint)mPage.SelectedIndex * 24 + 0x24 + Base + mAddress, 2);
			Item item = Item.Instance();

			mInfo = item.GetItemInfo(id);
			mKind = id - mInfo.ID;
			setItemName();
		}

		public override void Write()
		{
			if (mInfo == null) return;

			SaveData.Instance().WriteNumber((uint)mPage.SelectedIndex * 24 + 0x24 + Base + mAddress, 2, mInfo.ID + mKind);
		}

		private void setItemName()
		{
			if (mInfo == null)
			{
				mItem.Content = "不明";
			}
			else
			{
				mItem.Content = mInfo.Name;
				if (mKind > 0)
				{

					mItem.Content += "+" + mKind.ToString();
				}
			}
		}
	}
}
