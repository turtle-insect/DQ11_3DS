using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ11
{
	interface IListItem
	{
		uint Address();
		void Clear();
		void Create();
		void Remove(IListItem item);
		void Swap(IListItem item);
	}
}
