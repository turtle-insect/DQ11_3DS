using System.Windows.Controls;

namespace DQ11
{
	class CharItem : CharStatus
	{
		private ComboBox mPage;
		private ComboBox mItem;
		private ComboBox mCount;
		private Token mToken;

		public CharItem(ComboBox page, ComboBox item, ComboBox count, uint address, uint size)
		{
			mPage = page;
			mItem = item;
			mCount = count;
			mToken = new Token(address, size);
			item.SelectionChanged += Item_SelectionChanged;
		}

		private void Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ItemInfo info = ((ComboBox)sender).SelectedItem as ItemInfo;
			if (info == null) return;
			mCount.Items.Clear();
			mCount.IsEnabled = info.Count > 1;
			if (info.Count > 1)
			{
				mCount.Items.Add("");
				for (uint i = 1; i < info.Count; i++)
				{
					mCount.Items.Add("+" + i.ToString());
				}
			}
		}

		public override void Init()
		{
			Item item = Item.Instance();
			mItem.Items.Add(item.None);
			foreach (ItemInfo info in item.Tools)
			{
				mItem.Items.Add(info);
			}
			foreach (ItemInfo info in item.Equipments)
			{
				mItem.Items.Add(info);
			}
		}

		public override void Read()
		{
			uint id = SaveData.Instance().ReadNumber((uint)mPage.SelectedIndex * 24 + 0x24 + Address + mToken.Address, mToken.Size);
			Item item = Item.Instance();
			// 不明があれば削る.
			if(!(mItem.Items[mItem.Items.Count - 1] is ItemInfo))
			{
				mItem.Items.RemoveAt(mItem.Items.Count - 1);
			}

			ItemInfo info = item.GetItemInfo(id);
			if (info == null)
			{
				mItem.Items.Add("不明" + id.ToString());
				mItem.SelectedIndex = mItem.Items.Count - 1;
			}
			else
			{
				mItem.Text = info.Name;
				if(info.Count > 1)
				{
					mCount.SelectedIndex = (int)(id - info.ID);
				}
			}
		}

		public override void Write()
		{
			ItemInfo info = mItem.SelectedItem as ItemInfo;
			if (info == null) return;
			uint count = 0;
			if(info.Count > 1)
			{
				count = (uint)mCount.SelectedIndex;
			}
			SaveData.Instance().WriteNumber((uint)mPage.SelectedIndex * 24 + 0x24 + Address + mToken.Address, mToken.Size, info.ID + count);
		}
	}
}
