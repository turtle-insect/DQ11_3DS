using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQ11
{
	interface IListItem
	{
		void Copy(IListItem item);
		void Swap(IListItem item);
		void Init();
	}
}
