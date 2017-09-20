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
