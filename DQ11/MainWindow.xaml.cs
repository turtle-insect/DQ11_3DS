using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DQ11
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<CharStatus> mCharStatusList;
		private List<CharStatus> mYochiStatusList;
		private List<AllStatus> mAllStatusList;

		BagToolMgr mBagTool;
		BagEquipmentMgr mBagEquipment;
		HatMgr mYochiHat;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Item.Instance();
			SaveData.Instance();
			// キャラクタの設定.
			mCharStatusList = new List<CharStatus>();
			mCharStatusList.Add(new CharName(TextBoxCharName, 0x2));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharLv, 0x10, 1, 1, 99));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharExp, 0x14, 4, 0, 9999999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharSkill, 0x1E, 2, 0, 99));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharHP, 0x20, 2, 1, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharMP, 0x22, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharMaxHP, 0x100, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharMaxMP, 0x102, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharAttackMagic, 0x10C, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharHealMagic, 0x10E, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharAttack, 0x104, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharDiffence, 0x10A, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharSpeed, 0x108, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharSkillful, 0x106, 2, 0, 999));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharCharm, 0x110, 2, 0, 999));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem1, ComboBoxCharItemCount1, 0));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem2, ComboBoxCharItemCount2, 2));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem3, ComboBoxCharItemCount3, 4));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem4, ComboBoxCharItemCount4, 6));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem5, ComboBoxCharItemCount5, 8));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem6, ComboBoxCharItemCount6, 10));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem7, ComboBoxCharItemCount7, 12));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem8, ComboBoxCharItemCount8, 14));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem9, ComboBoxCharItemCount9, 16));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem10, ComboBoxCharItemCount10, 18));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem11, ComboBoxCharItemCount11, 20));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem12, ComboBoxCharItemCount12, 22));
			mCharStatusList.ForEach(x => x.Init());


			// ヨッチ族の設定.
			mYochiStatusList = new List<CharStatus>();
			mYochiStatusList.Add(new CharName(TextBoxYochiName, 0x0));
			mYochiStatusList.Add(new CharNumberStatus(TextBoxYochiMotivation, 0x78, 2, 1, 999));
			mYochiStatusList.Add(new CharChoiceStatus(ComboBoxYochiRank, 0x7A, 1, 1));
			mYochiStatusList.Add(new CharChoiceStatus(ComboBoxYochiColor, 0x7B, 1));
			mYochiStatusList.Add(new YochiHat(ComboBoxYochiHat));
			mYochiStatusList.Add(new CharChoiceStatus(ComboBoxYochiInfo, 0x84, 1));
			mYochiStatusList.Add(new CharChoiceStatus(ComboBoxYochiBoost, 0x85, 1));
			mYochiStatusList.Add(new YochiWeapon(ComboBoxYochiWeapon));
			mYochiStatusList.ForEach(x => x.Init());


			// 全体の設定.
			mAllStatusList = new List<AllStatus>();

			// ふくろの設定.
			mBagTool = new BagToolMgr();
			mBagTool.Init(mAllStatusList, StackPanelBagTool, ComboBoxBagToolPage, 0x3E34, 168);
			mBagEquipment = new BagEquipmentMgr();
			mBagEquipment.Init(mAllStatusList, StackPanelBagEquipment, ComboBoxBagEquipmentPage, 0x40EC, 2340);

			// 帽子の設定.
			mYochiHat = new HatMgr(mAllStatusList, StackPanelHat);

			// すれちがい
			mAllStatusList.Add(new AllStringStatus(TextBoxPassName, 0xC46C, 6));
			mAllStatusList.Add(new AllStringStatus(TextBoxPassMessage, 0xC47A, 16));

			// 雑多.
			mAllStatusList.Add(new PlayTime(TextBoxPlayHour, TextBoxPlayMinute, TextBoxPlaySecond));
			mAllStatusList.Add(new AllNumberStatus(TextBoxGoldHand, 0x3E28, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxGoldBank, 0x6584, 4, 0, 9999999));

			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder1, 0));
			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder2, 1));
			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder3, 2));
			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder4, 3));

			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxEscapeNG, 0x6A7F));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxShopNG, 0x6A80));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxArmorNG, 0x6A81));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxAshamed, 0x6A83));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxBattleSpeed, 0x6FBF, 1));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxBGMVolume, 0x6FC0, 1));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxSEVolume, 0x6FC1, 1));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxCameraRotate, 0x6FC2, 1));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxCStickRotate, 0x6FC3, 1));
			mAllStatusList.ForEach(x => x.Init());
		}

		private void ToolBarFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load();
		}

		private void MenuItemFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load();
		}

		private void ToolBarFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void MenuItemFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void MenuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
		{
			mAllStatusList.ForEach(x => x.Save());
			if (SaveData.Instance().SaveAs() == true) MessageBox.Show("書込成功");
			else MessageBox.Show("書込失敗");
		}

		private void MenuItemExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
		{
			new AboutWindow().ShowDialog();
		}

		private void ButtonCharStatusCopy_Click(object sender, RoutedEventArgs e)
		{
			mCharStatusList.ForEach(x => x.Copy());
		}

		private void ButtonCharStatusPaste_Click(object sender, RoutedEventArgs e)
		{
			mCharStatusList.ForEach(x => x.Paste());
		}

		private void ButtonCharStatusMin_Click(object sender, RoutedEventArgs e)
		{
			mCharStatusList.ForEach(x => x.Min());
		}

		private void ButtonCharStatusMax_Click(object sender, RoutedEventArgs e)
		{
			mCharStatusList.ForEach(x => x.Max());
		}

		private void ButtonCharStatusDecision_Click(object sender, RoutedEventArgs e)
		{
			mCharStatusList.ForEach(x => x.Write());
		}

		private void ButtonYochiRemove_Click(object sender, RoutedEventArgs e)
		{
			int index = ListBoxYochi.SelectedIndex;
			if (index < 0) return;

			SaveData saveDate = SaveData.Instance();
			for(uint i = (uint)index; i < Util.YochiCount - 1; i++)
			{
				saveDate.Copy((i + 1) * Util.YochiDateSize + Util.YochiStartAddress,
										i * Util.YochiDateSize + Util.YochiStartAddress,
										Util.YochiDateSize);
			}
			saveDate.WriteNumber((Util.YochiCount - 1) * Util.YochiDateSize + Util.YochiStartAddress, Util.YochiDateSize, 0);
			saveDate.WriteNumber((Util.YochiCount - 1) * Util.YochiDateSize + Util.YochiStartAddress - 4, 4, 0xFFFFFFFF);
			ListBoxYochi.Items.RemoveAt(index);
		}

		private void ButtonYochiAppend_Click(object sender, RoutedEventArgs e)
		{
			uint count = (uint)ListBoxYochi.Items.Count;
			if (count >= Util.YochiCount)
			{
				MessageBox.Show("ヨッチ族の登録数が上限に達しています");
				return;
			}

			Byte[] prof = { 0x4B, 0x30, 0x81, 0x30, 0x80, 0x30, 0x57, 0x30, 0xC3, 0x30, 0xC1, 0x30, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
										0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 0x00, 0x01, 0x05, 0x00, 0x00, 0xA4, 0x00,
										0x05, 0x00, 0x1B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, };

			if(prof.Length != Util.YochiDateSize)
			{
				// Debug Message.
				MessageBox.Show("ヨッチ族のプロフ確認");
				return;
			}
			SaveData saveDate = SaveData.Instance();
			for(uint i = 0; i < prof.Length; i++)
			{
				saveDate.WriteNumber(count * Util.YochiDateSize + Util.YochiStartAddress + i, 1, prof[i]);
			}
			// Random Better ?
			saveDate.WriteNumber(count * Util.YochiDateSize + Util.YochiStartAddress - 4, 4, count);

			String name = saveDate.ReadUnicode(count * Util.YochiDateSize + Util.YochiStartAddress, 12);
			if (String.IsNullOrEmpty(name)) return;
			ListBoxYochi.Items.Add(name);
		}

		private void ButtonYochiStatusDecision_Click(object sender, RoutedEventArgs e)
		{
			mYochiStatusList.ForEach(x => x.Write());
		}

		private void ListBoxChar_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ListBoxChar.SelectedIndex;
			if (index < 0) return;
			mCharStatusList.ForEach(x => x.Load((uint)index * Util.CharDateSize));
			mCharStatusList.ForEach(x => x.Read());
		}

		private void ListBoxYochi_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ListBoxYochi.SelectedIndex;
			if (index < 0) return;
			mYochiStatusList.ForEach(x => x.Load((uint)index * Util.YochiDateSize + Util.YochiStartAddress));
			mYochiStatusList.ForEach(x => x.Read());
		}

		private void ComboBoxCharItemPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ListBoxChar.SelectedIndex;
			if (index < 0) return;
			mCharStatusList.ForEach(x => x.Load((uint)index * Util.CharDateSize));
			mCharStatusList.ForEach(x => x.Read());
		}

		private void Load()
		{
			SaveData saveData = SaveData.Instance();
			if (saveData.Open() == false)
			{
				MessageBox.Show("読込失敗");
				return;
			}

			ListBoxChar.Items.Clear();
			ComboBoxCharItemPage.SelectedIndex = 0;
			List<String> names = Util.GetPartyNames();
			for (int i = 0; i < names.Count; i++)
			{
				ListBoxItem item = new ListBoxItem();
				item.Content = names[i];
				ListBoxChar.Items.Add(item);
			}

			ListBoxYochi.Items.Clear();
			names = Util.GetYochiNames();
			for (int i = 0; i < names.Count; i++)
			{
				ListBoxItem item = new ListBoxItem();
				item.Content = names[i];
				ListBoxYochi.Items.Add(item);
			}

			mAllStatusList.ForEach(x => x.Open());
			MessageBox.Show("読込成功");
		}

		private void Save()
		{
			mAllStatusList.ForEach(x => x.Save());
			if (SaveData.Instance().Save() == true) MessageBox.Show("書込成功");
			else MessageBox.Show("書込失敗");
		}
	}
}
