using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	abstract class BagBaseMgr
	{
		protected static readonly uint OnePageCount = 12;
		private ComboBox mPage;
		private uint mMax;
		protected uint mAddress;
		protected readonly List<AllStatus> mItems = new List<AllStatus>();

		public void Init(List<AllStatus> status, Panel panel, ComboBox page, uint address, uint max)
		{
			mPage = page;
			mAddress = address;
			mMax = max;
			mPage.SelectionChanged += ComboBoxPage_SelectionChanged;

			uint maxPage = mMax / OnePageCount + (uint)(mMax % OnePageCount == 0 ? 0 : 1);
			for (uint i = 0; i < maxPage; i++)
			{
				mPage.Items.Add((i + 1).ToString() + " / " + maxPage.ToString());
			}
			page.SelectedIndex = 0;
			CreateComp(status, panel);
		}

		public abstract void CreateComp(List<AllStatus> status, Panel panel);

		private void ComboBoxPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			mItems.ForEach(x => x.Save());
			mItems.ForEach(x => x.Page((uint)mPage.SelectedIndex));
			mItems.ForEach(x => x.Open());
		}

		protected void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Item item = Item.Instance();
			int itemCount = item.Tools.Count;
			Button button = sender as Button;
			if (button == null) return;
			uint i = (uint)button.Tag;
			i += (uint)mPage.SelectedIndex * OnePageCount;
			SaveData saveData = SaveData.Instance();
			for (; i < mMax - 1; i++)
			{
				saveData.WriteNumber(mAddress + i * 4, 4, saveData.ReadNumber(mAddress + (i + 1) * 4, 4));
			}
			saveData.WriteNumber(mAddress + (mMax - 1) * 4, 4, 0xFFFF);
			mItems.ForEach(x => x.Open());
		}
	}
}
