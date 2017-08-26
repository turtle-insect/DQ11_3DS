using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class ButtonCheckObserver
	{
		List<CheckBox> mChecks = new List<CheckBox>();
		public ButtonCheckObserver(Button check, Button uncheck)
		{
			check.Click += Check_Click;
			uncheck.Click += UnCheck_Click;
		}

		public void Append(CheckBox check)
		{
			mChecks.Add(check);
		}

		private void Check_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			foreach (var check in mChecks)
			{
				check.IsChecked = true;
			}
		}

		private void UnCheck_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			foreach (var check in mChecks)
			{
				check.IsChecked = false;
			}
		}
	}
}
