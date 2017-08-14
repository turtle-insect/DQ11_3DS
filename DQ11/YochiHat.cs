using System.Windows.Controls;

namespace DQ11
{
	class YochiHat : ListStatus
	{
		private readonly ComboBox mHat;

		public YochiHat(ComboBox hat)
		{
			mHat = hat;
		}

		public override void Init()
		{
			mHat.Items.Clear();
			Item item = Item.Instance();
			mHat.Items.Add(item.None);
			foreach (ItemInfo info in item.Hats)
			{
				mHat.Items.Add(info);
			}
			mHat.Items.Add(new ItemInfo(118, "魔王の剣", 0));
			mHat.Items.Add(new ItemInfo(119, "テスト用アイテム2", 0));
			mHat.Items.Add(new ItemInfo(120, "テスト用アイテム3", 0));
			mHat.Items.Add(new ItemInfo(121, "テスト用アイテム4", 0));
		}

		public override void Read()
		{
			uint id = SaveData.Instance().ReadNumber(Base + 0x7C, 2);
			if(id == 0)
			{
				mHat.SelectedIndex = 0;
				return;
			}
			id -= 2723;
			if (id >= mHat.Items.Count - 1) id = uint.MaxValue;
			mHat.SelectedIndex = (int)id + 1;
		}

		public override void Write()
		{
			ItemInfo info = mHat.SelectedItem as ItemInfo;
			if (info == null) return;

			uint value = info.ID;
			if (value == Item.Instance().None.ID) value = 0;
			else value += 2723;
			SaveData.Instance().WriteNumber(Base + 0x7C, 2, value);
		}
	}
}
