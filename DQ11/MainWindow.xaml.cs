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
		private List<ListStatus> mCharStatusList;
		private List<ListStatus> mYochiStatusList;
		private List<ListStatus> mPartyStatusList;
		private List<AllStatus> mAllStatusList;

		BagToolMgr mBagTool;
		BagEquipmentMgr mBagEquipment;
		ListActionObserver mParty;
		ListActionObserver mYochi;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Item.Instance();
			SaveData.Instance();
			// キャラクタ.
			mCharStatusList = new List<ListStatus>();
			mCharStatusList.Add(new CharName(TextBoxCharName, 0x2));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharLv, 0x10, 1, 1, 99));
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharExp, 0x14, 4, 0, 9999999));
			mCharStatusList.Add(new CharChoiceStatus(ComboBoxCharStrategy, 0x1C, 1));
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


			// ヨッチ族.
			mYochiStatusList = new List<ListStatus>();
			mYochi = new ListActionObserver(ListBoxYochi,
							ButtonYochiUp, ButtonYochiDown, ButtonYochiAppend, ButtonYochiRemove, new ListControlYochi());
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

			// ふくろ.
			mBagTool = new BagToolMgr();
			mBagTool.Init(mAllStatusList, StackPanelBagTool, ComboBoxBagToolPage, 0x3E34, 168);
			mBagEquipment = new BagEquipmentMgr();
			mBagEquipment.Init(mAllStatusList, StackPanelBagEquipment, ComboBoxBagEquipmentPage, 0x40EC, 2340);

			// だいじなもの.
			mAllStatusList.Add(new CheckBoxListItem(ListBoxImportant, Item.Instance().Importants, 0x65C4, 90));

			// レシピ.
			mAllStatusList.Add(new CheckBoxListItem(ListBoxRecipe, Item.Instance().Recipes, 0x6678, 105));

			// 帽子.
			CreateHat(mAllStatusList, StackPanelHat);

			// すれちがい.
			mAllStatusList.Add(new AllStringStatus(TextBoxPassName, 0xC46C, 6));
			mAllStatusList.Add(new AllStringStatus(TextBoxPassMessage, 0xC47A, 16));

			// 称号.
			CreateTitle(mAllStatusList, StackPanelTitle);

			// クエスト.
			mAllStatusList.Add(new Quest(ListBoxQuest));

			// 基本.
			mAllStatusList.Add(new PlayTime(TextBoxPlayHour, TextBoxPlayMinute, TextBoxPlaySecond));
			mAllStatusList.Add(new AllNumberStatus(TextBoxGoldHand, 0x3E28, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxTotalGold, 0x3E2C, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxGoldBank, 0x6584, 4, 0, 9999999));
			//mAllStatusList.Add(new AllNumberStatus(TextBoxSmallMedal, 0x6588, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxCamp, 0x67A8, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxINI, 0x67AC, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxBreak, 0x67B4, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxKnockDownMonster, 0x3E30, 4, 0, 9999999));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxHorse, 0x955C));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxZoom, 0x6A7B));
			mAllStatusList.Add(new AllCheckBoxStatus(CheckBoxShip, 0x6A13));

			// パーティー.
			mPartyStatusList = new List<ListStatus>();
			mParty = new ListActionObserver(ListBoxParty,
							ButtonPartyUp, ButtonPartyDown, ButtonPartyAppend, ButtonPartyRemove, new ListControlParty());
			mPartyStatusList.Add(new PartyOrder(ComboBoxPartyOrder));
			mPartyStatusList.ForEach(x => x.Init());

			// ルーラ
			mAllStatusList.Add(new Zoom(ListBoxZoom));

			// システム.
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
			Load(false);
		}

		private void MenuItemFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load(false);
		}

		private void MenuItemFileOpenForce_Click(object sender, RoutedEventArgs e)
		{
			Load(true);
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
			if (ListBoxChar.SelectedIndex < 0) return;
			mCharStatusList.ForEach(x => x.Write());
		}

		private void ButtonYochiStatusDecision_Click(object sender, RoutedEventArgs e)
		{
			if (ListBoxYochi.SelectedIndex < 0) return;
			mYochiStatusList.ForEach(x => x.Write());
			mYochi.Load();
		}

		private void ButtonPartyDecision_Click(object sender, RoutedEventArgs e)
		{
			if (ListBoxParty.SelectedIndex < 0) return;
			mPartyStatusList.ForEach(x => x.Write());
			mParty.Load();
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

		private void ListBoxParty_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ListBoxParty.SelectedIndex;
			if (index < 0) return;
			mPartyStatusList.ForEach(x => x.Load((uint)index));
			mPartyStatusList.ForEach(x => x.Read());
		}

		private void ComboBoxCharItemPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ListBoxChar.SelectedIndex;
			if (index < 0) return;
			mCharStatusList.ForEach(x => x.Load((uint)index * Util.CharDateSize));
			mCharStatusList.ForEach(x => x.Read());
		}

		private void Load(bool force)
		{
			SaveData saveData = SaveData.Instance();
			if (saveData.Open(force) == false)
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

			mParty.Load();
			mYochi.Load();

			mAllStatusList.ForEach(x => x.Open());
			MessageBox.Show("読込成功");
		}

		private void Save()
		{
			mAllStatusList.ForEach(x => x.Save());
			if (SaveData.Instance().Save() == true) MessageBox.Show("書込成功");
			else MessageBox.Show("書込失敗");
		}

		private void CreateHat(List<AllStatus> status, Panel panel)
		{
			Item item = Item.Instance();
			foreach (ItemInfo info in item.Hats)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(45) });
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });

				Label name = new Label();
				name.Content = info.Name;
				grid.Children.Add(name);

				TextBox ticket = new TextBox();
				status.Add(new AllNumberStatus(ticket, Util.HatStartAddress + info.ID, 1, 0, 999));
				ticket.SetValue(Grid.ColumnProperty, 1);
				grid.Children.Add(ticket);

				CheckBox obtain = new CheckBox();
				status.Add(new HatObtain(obtain, info.ID));
				obtain.SetValue(Grid.ColumnProperty, 2);
				obtain.HorizontalAlignment = HorizontalAlignment.Center;
				obtain.VerticalAlignment = VerticalAlignment.Center;
				grid.Children.Add(obtain);

				panel.Children.Add(grid);
			}
		}

		private void CreateTitle(List<AllStatus> status, Panel panel)
		{
			Item item = Item.Instance();
			uint count = 0;
			foreach (ItemInfo info in item.Titles)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });

				Label name = new Label();
				name.Content = info.Name;
				grid.Children.Add(name);

				CheckBox obtain = new CheckBox();
				status.Add(new TitleObtain(obtain, info.ID));
				obtain.SetValue(Grid.ColumnProperty, 1);
				obtain.HorizontalAlignment = HorizontalAlignment.Center;
				obtain.VerticalAlignment = VerticalAlignment.Center;
				grid.Children.Add(obtain);

				panel.Children.Add(grid);

				count++;
				if (count % 8 == 0)
				{
					var line = new System.Windows.Shapes.Line();
					line.Stroke = System.Windows.Media.Brushes.Blue;
					line.StrokeThickness = 2.0;
					line.X1 = line.Y1 = line.Y2 = 0;
					line.X2 = 500;
					panel.Children.Add(line);
				}
			}
		}
	}
}
