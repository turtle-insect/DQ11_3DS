using System.Windows.Controls;

namespace DQ11
{
	class PlayTime : AllStatus
	{
		private readonly TextBox mHour;
		private readonly TextBox mMinute;
		private readonly TextBox mSecond;

		public PlayTime(TextBox hour, TextBox minute, TextBox second)
		{
			mHour = hour;
			mMinute = minute;
			mSecond = second;
		}

		public override void Open()
		{
			uint value = SaveData.Instance().ReadNumber(0x3E24, 4);
			uint hour = value / 3600;
			uint minute = value / 60 % 60;
			mHour.Text = hour.ToString();
			mMinute.Text = minute.ToString();
			mSecond.Text = (value - hour * 3600 - minute * 60).ToString();
		}

		public override void Save()
		{
			uint hour;
			if (!uint.TryParse(mHour.Text, out hour)) return;
			uint minute;
			if (!uint.TryParse(mMinute.Text, out minute)) return;
			uint second;
			if (!uint.TryParse(mSecond.Text, out second)) return;

			if (hour < 0) hour = 0;
			if (hour > 999) hour = 999;
			if (minute < 0) minute = 0;
			if (minute > 59) minute = 59;
			if (second < 0) second = 0;
			if (second > 59) second = 59;

			SaveData.Instance().WriteNumber(0x3E24, 4, hour * 3600 + minute * 60 + second);
		}
	}
}
