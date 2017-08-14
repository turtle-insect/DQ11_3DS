using Microsoft.Win32;
using System;

namespace DQ11
{
	class SaveData
	{
		private static SaveData mThis;
		private String mFileName = null;
		private Byte[] mBuffer = null;
		private Crc32 mCrc32 = new Crc32();

		private SaveData()
		{}

		public static SaveData Instance()
		{
			if (mThis == null) mThis = new SaveData();
			return mThis;
		}

		public bool Open()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return false;
			mFileName = dlg.FileName;
			mBuffer = System.IO.File.ReadAllBytes(mFileName);

			if (mCrc32.Calc(ref mBuffer, 0, 0xC8A8) == ReadNumber(0xC8AC, 4))
			{
				Backup();
				return true;
			}

			mFileName = null;
			mBuffer = null;
			return false;
		}

		public bool Save()
		{
			if (mFileName == null || mBuffer == null) return false;
			WriteNumber(0xC8AC, 4, mCrc32.Calc(ref mBuffer, 0, 0xC8A8));
			System.IO.File.WriteAllBytes(mFileName, mBuffer);
			return true;
		}

		public bool SaveAs()
		{
			if (mBuffer == null) return false;
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.FileName = System.IO.Path.GetFileName(mFileName);
			if (dlg.ShowDialog() == false) return false;
			mFileName = dlg.FileName;
			return Save();
		}

		public uint ReadNumber(uint address, uint size)
		{
			if (mBuffer == null) return 0;
			if (address + size > mBuffer.Length) return 0;
			uint result = 0;
			for(int i = 0; i < size; i++)
			{
				result += (uint)(mBuffer[address + i]) << (i * 8);
			}
			return result;
		}

		// 0 to 7.
		public bool ReadBit(uint address, uint bit)
		{
			if (bit < 0) return false;
			if (bit > 7) return false;
			if (mBuffer == null) return false;
			if (address > mBuffer.Length) return false;
			Byte mask = (Byte)(1 << (int)bit);
			Byte result = (Byte)(mBuffer[address] & mask);
			return result != 0;
		}

		public String ReadUnicode(uint address, uint size)
		{
			if (mBuffer == null) return "";
			if (address + size > mBuffer.Length) return "";

			Byte[] tmp = new Byte[size];
			for(int i = 0; i < size; i++)
			{
				tmp[i] = mBuffer[address + i];
			}
			return System.Text.Encoding.Unicode.GetString(tmp).Trim('\0');
		}

		public void WriteNumber(uint address, uint size, uint value)
		{
			if (mBuffer == null) return;
			if (address + size > mBuffer.Length) return;
			for (int i = 0; i < size; i++)
			{
				mBuffer[address + i] = (Byte)(value & 0xFF);
				value >>= 8;
			}
		}

		// 0 to 7.
		public void WriteBit(uint address, uint bit, bool value)
		{
			if (bit < 0) return;
			if (bit > 7) return;
			if (mBuffer == null) return;
			if (address > mBuffer.Length) return;
			Byte mask = (Byte)(1 << (int)bit);
			if (value) mBuffer[address] = (Byte)(mBuffer[address] | mask);
			else mBuffer[address] = (Byte)(mBuffer[address] & ~mask);
		}

		public void WriteUnicode(uint address, uint size, String value)
		{
			if (mBuffer == null) return;
			if (address + size > mBuffer.Length) return;
			Byte[] tmp = System.Text.Encoding.Unicode.GetBytes(value);
			Array.Resize(ref tmp, (int)size);
			for (int i = 0; i < size; i++)
			{
				mBuffer[address + i] = tmp[i];
			}
		}

		public void Copy(uint from, uint to, uint size)
		{
			if (mBuffer == null) return;
			if (from + size > mBuffer.Length) return;
			if (to + size > mBuffer.Length) return;
			for(int i = 0; i < size; i++)
			{
				mBuffer[to + i] = mBuffer[from + i];
			}
		}

		private void Backup()
		{
			DateTime now = DateTime.Now;
			String path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			path = System.IO.Path.Combine(path, "backup");
			if(!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
			path = System.IO.Path.Combine(path, 
				String.Format("{0:0000}-{1:00}-{2:00} {3:00}-{4:00}", now.Year, now.Month, now.Day, now.Hour, now.Minute));
			System.IO.File.WriteAllBytes(path, mBuffer);
		}
	}
}
