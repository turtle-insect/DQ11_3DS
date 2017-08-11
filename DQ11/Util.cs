using System;
using System.Collections.Generic;

namespace DQ11
{
	class Util
	{
		public static List<String> GetPartyNames()
		{
			List<String> result = new List<string>();
			SaveData data = SaveData.Instance();
			for (uint i = 0; i < 31; i++)
			{
				String name = data.ReadUnicode(i * 0x200 + 2, 12);
				result.Add(name);
			}
			return result;
		}
	}
}
