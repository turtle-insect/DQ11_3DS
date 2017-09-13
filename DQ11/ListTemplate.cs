using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ11
{
	class ListTemplate<T> where T : IListItem, new()
	{
		private int mMax;
		private readonly List<T> mList = new List<T>();

		public ListTemplate(int max)
		{
			mMax = max;
		}

		public void Remove(int index)
		{
			if (mList.Count >= index) return;
			if (mList.Count == 0) return;
			for(int i = index; i < mList.Count - 1; i++)
			{
				mList[i].Copy(mList[i + 1]);
			}
			mList[mList.Count - 1].Init();
		}

		public void Append()
		{
			if (mList.Count >= mMax) return;
			T t = new T();
			t.Init();
			mList.Add(t);
		}

		public void Up(int index)
		{
			if (index == 0) return;
			if (index >= mList.Count) return;
			mList[index].Swap(mList[index - 1]);
		}

		public void Down(int index)
		{
			if (index >= mList.Count - 1) return;
			mList[index].Swap(mList[index - 1]);
		}
	}
}
