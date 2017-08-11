using System.Windows.Controls;

namespace DQ11
{
	abstract class CharStatus
	{
		protected uint Address { get; private set; } = 0;

		public virtual void Init() { }
		public void Load(Control parent)
		{
			if (parent == null) return;
			Token token = parent.Tag as Token;
			if (token == null) return;
			Address = token.Address;
		}

		public abstract void Read();
		public abstract void Write();
		public virtual void Copy() { }
		public virtual void Paste() { }
		public virtual void Max() { }
		public virtual void Min() { }
	}
}
