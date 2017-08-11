using System.Windows.Controls;

namespace DQ11
{
	class BagEquipmentItem : AllStatus
	{
		private readonly ComboBox mItem;
		private readonly ComboBox mKind;
		private readonly TextBox mCount;
		private readonly uint mAddress;
		private uint mPage;

		public BagEquipmentItem(ComboBox item, ComboBox kind, TextBox count, uint address)
		{
			mItem = item;
			mKind = kind;
			mCount = count;
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
			foreach (ItemInfo info in item.Equipments)
			{
				mItem.Items.Add(info);
			}
		}

		public override void Open()
		{
			uint address = 0x40EC + mPage * 12 * 4 + mAddress;
			SaveData saveData = SaveData.Instance();
			uint id = saveData.ReadNumber(address, 2);
			Item item = Item.Instance();
			// 不明があれば削る.
			if (!(mItem.Items[mItem.Items.Count - 1] is ItemInfo))
			{
				mItem.Items.RemoveAt(mItem.Items.Count - 1);
			}

			mKind.Items.Clear();
			mKind.IsEnabled = false;
			ItemInfo info = item.GetEquipmentInfo(id);
			if (info == null)
			{
				mItem.Items.Add("不明" + id.ToString());
				mItem.SelectedIndex = mItem.Items.Count - 1;
			}
			else
			{
				if(info.Count > 1)
				{
					mKind.IsEnabled = true;
					mKind.Items.Add("");
					for (uint i = 1; i < info.Count; i++)
					{
						mKind.Items.Add("+" + i.ToString());
					}
					mItem.SelectedIndex = (int)(info.ID - id);
				}
				mItem.Text = info.Name;
			}

			uint count = saveData.ReadNumber(address + 2, 2);
			mCount.Text = count.ToString();
		}

		public override void Save()
		{
			ItemInfo info = mItem.SelectedItem as ItemInfo;
			if (info == null) return;
			uint kind = 0;
			if (info.Count > 1)
			{
				kind = (uint)mKind.SelectedIndex;
			}
			uint address = 0x40EC + mPage * 12 * 4 + mAddress;
			SaveData saveData = SaveData.Instance();
			saveData.WriteNumber(address, 2, info.ID + kind);

			uint count;
			if (uint.TryParse(mCount.Text, out count) == false) return;
			if (count < 1) count = 1;
			if (count > 99) count = 99;
			if (info == Item.Instance().None) count = 0;
			saveData.WriteNumber(address + 2, 2, count);
		}

		public override void Page(uint page)
		{
			mPage = page;
		}
	}
}
