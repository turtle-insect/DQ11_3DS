using System.Windows.Controls;

namespace DQ11
{
	class ListActionObserver
	{
		private readonly ListBox mList;
		private readonly Button mUp;
		private readonly Button mDown;
		private readonly Button mAppend;
		private readonly Button mRemove;
		private readonly IListControl mOpe;
		public ListActionObserver(ListBox listbox, Button up, Button down, Button append, Button remove, IListControl ope)
		{
			mList = listbox;
			mUp = up;
			mDown = down;
			mAppend = append;
			mRemove = remove;
			mOpe = ope;

			mUp.Click += Up_Click;
			mDown.Click += Down_Click;
			mAppend.Click += Append_Click;
			mRemove.Click += Remove_Click;
		}

		public void Load()
		{
			mList.Items.Clear();
			mOpe.Load(mList);
		}

		private void Remove_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			int index = mList.SelectedIndex;
			if (index < 0) return;
			mOpe.Remove((uint)index);
			Load();
		}

		private void Append_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			mOpe.Append((uint)mList.Items.Count);
			Load();
		}

		private void Down_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			int index = mList.SelectedIndex;
			if (index < 0) return;
			if (index == mList.Items.Count - 1) return;
			mOpe.RePlace((uint)index, (uint)index + 1);
			Load();
			mList.SelectedIndex = index + 1;
		}

		private void Up_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			int index = mList.SelectedIndex;
			if (index <= 0) return;
			mOpe.RePlace((uint)index, (uint)index - 1);
			Load();
			mList.SelectedIndex = index - 1;
		}
	}
}
