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
		private List<CharStatus> mChildStatusList;
		private List<AllStatus> mAllStatusList;

		BagToolMgr mBagTool;
		BagEquipmentMgr mBagEquipment;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Item.Instance();
			SaveData.Instance();
			mChildStatusList = new List<CharStatus>();
			mChildStatusList.Add(new CharName(TextBoxCharName));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharLv, 0x10, 1, 1, 99));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharExp, 0x14, 4, 0, 99999999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharSkill, 0x1E, 2, 0, 99));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharHP, 0x20, 2, 1, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharMP, 0x22, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharMaxHP, 0x100, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharMaxMP, 0x102, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharAttackMagic, 0x10C, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharHealMagic, 0x10E, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharAttack, 0x104, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharDiffence, 0x10A, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharSpeed, 0x108, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharSkillful, 0x106, 2, 0, 999));
			mChildStatusList.Add(new CharNumberStatus(TextBoxCharCharm, 0x110, 2, 0, 999));

			ComboBoxCharItemPage.Items.Add("1 / 2");
			ComboBoxCharItemPage.Items.Add("2 / 2");
			ComboBoxCharItemPage.SelectedIndex = 0;
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem1, ComboBoxCharItemCount1, 0));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem2, ComboBoxCharItemCount2, 2));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem3, ComboBoxCharItemCount3, 4));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem4, ComboBoxCharItemCount4, 6));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem5, ComboBoxCharItemCount5, 8));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem6, ComboBoxCharItemCount6, 10));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem7, ComboBoxCharItemCount7, 12));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem8, ComboBoxCharItemCount8, 14));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem9, ComboBoxCharItemCount9, 16));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem10, ComboBoxCharItemCount10, 18));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem11, ComboBoxCharItemCount11, 20));
			mChildStatusList.Add(new CharItem(ComboBoxCharItemPage, ComboBoxCharItem12, ComboBoxCharItemCount12, 22));
			mChildStatusList.ForEach(x => x.Init());

			mAllStatusList = new List<AllStatus>();
			mAllStatusList.Add(new PlayTime(TextBoxPlayHour, TextBoxPlayMinute, TextBoxPlaySecond));
			mAllStatusList.Add(new AllNumberStatus(TextBoxGoldHand, 0x3E28, 4, 0, 9999999));
			
			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder1, 0));
			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder2, 1));
			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder3, 2));
			mAllStatusList.Add(new PartyOrder(ComboBoxPartyOrder4, 3));

			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxEscapeNG, 0x6A7F));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxShopNG, 0x6A80));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxArmorNG, 0x6A81));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxAshamed, 0x6A83));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxBattleSpeed, 0x6FBF));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxBGMVolume, 0x6FC0));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxSEVolume, 0x6FC1));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxCameraRotate, 0x6FC2));
			mAllStatusList.Add(new AllChoiceStatus(ComboBoxCStickRotate, 0x6FC3));

			mBagTool = new BagToolMgr();
			mBagTool.Init(mAllStatusList, StackPanelBagTool, ComboBoxBagToolPage, 0x3E34, 168);
			mBagEquipment = new BagEquipmentMgr();
			mBagEquipment.Init(mAllStatusList, StackPanelBagEquipment, ComboBoxBagEquipmentPage, 0x40EC, 2340);
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

		}

		private void ButtonCharStatusCopy_Click(object sender, RoutedEventArgs e)
		{
			mChildStatusList.ForEach(x => x.Copy());
		}

		private void ButtonCharStatusPaste_Click(object sender, RoutedEventArgs e)
		{
			mChildStatusList.ForEach(x => x.Paste());
		}

		private void ButtonCharStatusMin_Click(object sender, RoutedEventArgs e)
		{
			mChildStatusList.ForEach(x => x.Min());
		}

		private void ButtonCharStatusMax_Click(object sender, RoutedEventArgs e)
		{
			mChildStatusList.ForEach(x => x.Max());
		}

		private void ButtonCharStatusDecision_Click(object sender, RoutedEventArgs e)
		{
			mChildStatusList.ForEach(x => x.Write());
		}

		private void ListBoxChar_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBoxItem item = ListBoxChar.SelectedItem as ListBoxItem;
			if (item == null) return;
			mChildStatusList.ForEach(x => x.Load(item));
			mChildStatusList.ForEach(x => x.Read());
		}

		private void ComboBoxCharItemPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxCharItemPage.SelectedIndex < 0) return;
			ListBoxItem item = ListBoxChar.SelectedItem as ListBoxItem;
			if (item == null) return;
			mChildStatusList.ForEach(x => x.Load(item));
			mChildStatusList.ForEach(x => x.Read());
		}

		private void Load()
		{
			SaveData data = SaveData.Instance();
			if (data.Open() == false)
			{
				MessageBox.Show("読込失敗");
				return;
			}

			ListBoxChar.Items.Clear();
			ComboBoxCharItemPage.SelectedIndex = 0;
			List<String> names = Util.GetPartyNames();
			for (uint i = 0; i < names.Count; i++)
			{
				ListBoxItem item = new ListBoxItem();
				item.Tag = i * 0x200;
				item.Content = names[(int)i];
				ListBoxChar.Items.Add(item);
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
