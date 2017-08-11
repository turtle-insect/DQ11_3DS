namespace DQ11
{
	class Token
	{
		public Token() : this(0, 0) { }
		public Token(uint address, uint size)
		{
			Address = address;
			Size = size;
		}
		public uint Address { get; set; }
		public uint Size { get; set; }
	}
}
