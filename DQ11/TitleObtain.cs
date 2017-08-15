using System.Windows.Controls;

namespace DQ11
{
	class TitleObtain : AllStatus
	{
		private readonly CheckBox mObtain;
		private readonly uint mID;

		public TitleObtain(CheckBox obtain, uint id)
		{
			mObtain = obtain;
			mID = id;
		}

		public override void Open()
		{
			SaveData saveData = SaveData.Instance();
			uint address = mID / 8 + Util.TitleObtainStartAddress;
			uint bit = mID % 8;
			mObtain.IsChecked = saveData.ReadBit(address, bit);
		}

		public override void Save()
		{
			SaveData saveData = SaveData.Instance();
			uint address = mID / 8 + Util.TitleObtainStartAddress;
			uint bit = mID % 8;
			saveData.WriteBit(address, bit, mObtain.IsChecked == true);
		}
	}
}
