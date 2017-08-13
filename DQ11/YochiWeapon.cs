using System.Windows.Controls;

namespace DQ11
{
	class YochiWeapon : CharStatus
	{
		private readonly ComboBox mItem;
		public YochiWeapon(ComboBox item)
		{
			mItem = item;
		}

		public override void Init()
		{
			foreach(ItemInfo item in Item.Instance().Equipments)
			{
				mItem.Items.Add(item);
			}
		}
		public override void Read()
		{
			if(!(mItem.Items[mItem.Items.Count - 1] is ItemInfo))
			{
				mItem.Items.RemoveAt(mItem.Items.Count - 1);
			}
			uint id = SaveData.Instance().ReadNumber(Base + 0x7E, 2);
			ItemInfo item = Item.Instance().GetEquipmentInfo(id);
			if(item == null)
			{
				mItem.Items.Add("不明" + id.ToString());
				mItem.SelectedIndex = mItem.Items.Count - 1;
			}
			else
			{
				mItem.Text = item.Name;
			}
		}

		public override void Write()
		{
			ItemInfo info = mItem.SelectedItem as ItemInfo;
			if (info == null) return;

			SaveData.Instance().WriteNumber(Base + 0x7E, 2, info.ID);
		}
	}
}
