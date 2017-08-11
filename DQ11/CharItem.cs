using System.Windows.Controls;

namespace DQ11
{
	class CharItem : CharStatus
	{
		private readonly ComboBox mPage;
		private readonly ComboBox mItem;
		private readonly ComboBox mKind;
		private readonly uint mAddress;

		public CharItem(ComboBox page, ComboBox item, ComboBox kind, uint address)
		{
			mPage = page;
			mItem = item;
			mKind = kind;
			mAddress = address;
			mItem.SelectionChanged += Item_SelectionChanged;
		}

		private void Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ItemInfo info = ((ComboBox)sender).SelectedItem as ItemInfo;
			if (info == null) return;
			mKind.Items.Clear();
			mKind.IsEnabled = info.Count > 1;
			if (info.Count > 1)
			{
				mKind.Items.Add("");
				for (uint i = 1; i < info.Count; i++)
				{
					mKind.Items.Add("+" + i.ToString());
				}
				mKind.SelectedIndex = 0;
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
			uint id = SaveData.Instance().ReadNumber((uint)mPage.SelectedIndex * 24 + 0x24 + Base + mAddress, 2);
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
					mKind.SelectedIndex = (int)(id - info.ID);
				}
			}
		}

		public override void Write()
		{
			ItemInfo info = mItem.SelectedItem as ItemInfo;
			if (info == null) return;
			uint kind = 0;
			if(info.Count > 1)
			{
				kind = (uint)mKind.SelectedIndex;
			}
			SaveData.Instance().WriteNumber((uint)mPage.SelectedIndex * 24 + 0x24 + Base + mAddress, 2, info.ID + kind);
		}
	}
}
