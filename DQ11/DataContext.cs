using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DQ11
{
	class DataContext
	{
		public List<Character> Char { get; set; } = new List<Character>();

		public void Init()
		{
			Char.Clear();
			for(uint i = 0; i < Util.CharCount; i++)
			{
				Char.Add(new Character(i * 0x200));
			}
		}
	}
}
