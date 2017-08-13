using System;
using System.Collections.Generic;

namespace DQ11
{
	class Util
	{
		public static readonly uint CharDateSize = 0x200;
		public static readonly uint YochiStartAddress = 0x9DE4;
		public static readonly uint YochiDateSize = 0x8C;
		public static readonly uint YochiCount = 70;

		public static List<String> GetPartyNames()
		{
			List<String> result = new List<string>();
			SaveData data = SaveData.Instance();
			for (uint i = 0; i < 31; i++)
			{
				String name = data.ReadUnicode(i * CharDateSize + 2, 12);
				result.Add(name);
			}
			return result;
		}

		public static List<String> GetYochiNames()
		{
			List<String> result = new List<string>();
			SaveData data = SaveData.Instance();
			for (uint i = 0; i < 70; i++)
			{
				String name = data.ReadUnicode(i * YochiDateSize + YochiStartAddress, 12);
				if (String.IsNullOrEmpty(name)) break;
				result.Add(name);
			}
			return result;
		}
	}
}
