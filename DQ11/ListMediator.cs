using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DQ11
{
	class ListMediator
	{
		public ObservableCollection<IListItem> List { get; set; } = new ObservableCollection<IListItem>();

		public void Clear()
		{
			List.Clear();
		}

		public void Remove(int index)
		{
			if (index >= List.Count) return;

			for(int i = index; i < List.Count - 1; i++)
			{
				List[i].Remove(List[i + 1]);
			}
			List[List.Count - 1].Clear();
		}

		public void Append(IListItem item)
		{
			List.Add(item);
		}

		public void Up(int index)
		{
			if (index <= 0) return;
			if (index >= List.Count) return;
			List[index].Swap(List[index - 1]);
		}

		public void Down(int index)
		{
			if (index >= List.Count - 1) return;
			List[index].Swap(List[index + 1]);
		}
	}
}
