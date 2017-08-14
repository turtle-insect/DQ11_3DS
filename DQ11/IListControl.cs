using System.Windows.Controls;

namespace DQ11
{
	interface IListControl
	{
		void Load(ListBox control);
		void RePlace(uint from, uint to);
		void Append(uint index);
		void Remove(uint index);
	}
}
