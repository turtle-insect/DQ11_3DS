namespace DQ11
{
	abstract class AllStatus
	{
		public virtual void Init() { }
		public abstract void Open();
		public abstract void Save();
		public virtual void Page(uint page) { }
		public virtual void Max() { }
		public virtual void Min() { }
	}
}
