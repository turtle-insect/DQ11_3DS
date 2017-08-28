using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

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

		ButtonCheckObserver mHatButtonCheck;
		ButtonCheckObserver mTitleButtonCheck;
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
			mCharStatusList.Add(new CharNumberStatus(TextBoxCharSkill, 0x112, 2, 0, 999));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName01, ButtonCharItemChange01, 0));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName02, ButtonCharItemChange02, 2));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName03, ButtonCharItemChange03, 4));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName04, ButtonCharItemChange04, 6));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName05, ButtonCharItemChange05, 8));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName06, ButtonCharItemChange06, 10));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName07, ButtonCharItemChange07, 12));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName08, ButtonCharItemChange08, 14));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName09, ButtonCharItemChange09, 16));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName10, ButtonCharItemChange10, 18));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName11, ButtonCharItemChange11, 20));
			mCharStatusList.Add(new CharItem(ComboBoxCharItemPage, LabelCharItemName12, ButtonCharItemChange12, 22));
			mCharStatusList.Add(new CharEquepment(ComboBoxCharRightHand, 0x54));
			mCharStatusList.Add(new CharEquepment(ComboBoxCharLeftHand, 0x55));
			mCharStatusList.Add(new CharEquepment(ComboBoxCharHead, 0x56));
			mCharStatusList.Add(new CharEquepment(ComboBoxCharBody, 0x57));
			mCharStatusList.Add(new CharEquepment(ComboBoxCharAccessory1, 0x58));
			mCharStatusList.Add(new CharEquepment(ComboBoxCharAccessory2, 0x59));
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
			mAllStatusList.Add(new CheckBoxListItem(ListBoxImportant, ButtonImportantCheck, ButtonImportantUnCheck, Item.Instance().Importants, 0x65C4, 90));

			// レシピ.
			mAllStatusList.Add(new CheckBoxListItem(ListBoxRecipe, ButtonRecipeCheck, ButtonRecipeUnCheck, Item.Instance().Recipes, 0x6678, 105));

			// 帽子.
			mHatButtonCheck = new ButtonCheckObserver(ButtonHatCheck, ButtonHatUnCheck);
			CreateHat(mAllStatusList, StackPanelHat);

			// すれちがい.
			mAllStatusList.Add(new AllStringStatus(TextBoxPassName, 0xC46C, 6));
			mAllStatusList.Add(new AllStringStatus(TextBoxPassMessage, 0xC47A, 16));

			// 称号.
			mTitleButtonCheck = new ButtonCheckObserver(ButtonTitleCheck, ButtonTitleUnCheck);
			CreateTitle(mAllStatusList, ListBoxTitle);

			// クエスト.
			mAllStatusList.Add(new Quest(ListBoxQuest, ComboBoxQuestState, ButtonQuestPatch));

			// 基本.
			mAllStatusList.Add(new PlayTime(TextBoxPlayHour, TextBoxPlayMinute, TextBoxPlaySecond));
			mAllStatusList.Add(new AllNumberStatus(TextBoxGoldHand, 0x3E28, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxTotalGold, 0x3E2C, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxGoldBank, 0x6584, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxCasinoCoin, 0x6FF4, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxSmallMedal, 0x6FE4, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxTotalSmallMedal, 0x6FE8, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxDepositSmallMedal, 0x6FF0, 4, 0, 9999999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxBlackSmith, 0x67E8, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxCamp, 0x67A8, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxINI, 0x67AC, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxBreak, 0x67B4, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxCooperation, 0x6798, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxSlot, 0x67DC, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxPoker, 0x67C4, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxRoulette, 0x67D0, 4, 0, 9999));
			mAllStatusList.Add(new AllNumberStatus(TextBoxKnockDownMonster, 0x3E30, 4, 0, 9999999));

			// パーティー.
			mPartyStatusList = new List<ListStatus>();
			mParty = new ListActionObserver(ListBoxParty,
							ButtonPartyUp, ButtonPartyDown, ButtonPartyAppend, ButtonPartyRemove, new ListControlParty());
			mPartyStatusList.Add(new PartyOrder(ComboBoxPartyOrder));
			mPartyStatusList.ForEach(x => x.Init());

			// ルーラ.
			mAllStatusList.Add(new Zoom(ListBoxZoom, ButtonZoomCheck, ButtonZoomUnCheck));

			// ストーリー.
			mAllStatusList.Add(new Story(ListBoxStory, ButtonStoryCheck, ButtonStoryUnCheck));

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

		private void Window_Drop(object sender, DragEventArgs e)
		{
			String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
			if (files == null) return;
			if (!System.IO.File.Exists(files[0])) return;

			SaveData saveData = SaveData.Instance();
			if (saveData.Open(files[0], false) == false)
			{
				MessageBox.Show("読込失敗");
				return;
			}

			Init();
			MessageBox.Show("読込成功");
		}

		private void Window_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
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

			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == false) return;

			if (SaveData.Instance().SaveAs(dlg.FileName) == true) MessageBox.Show("書込成功");
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
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			if (SaveData.Instance().Open(dlg.FileName, force) == false)
			{
				MessageBox.Show("読込失敗");
				return;
			}

			Init();
			MessageBox.Show("読込成功");
		}

		private void Save()
		{
			mAllStatusList.ForEach(x => x.Save());
			if (SaveData.Instance().Save() == true) MessageBox.Show("書込成功");
			else MessageBox.Show("書込失敗");
		}

		private void Init()
		{
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
		}

		private void CreateHat(List<AllStatus> status, StackPanel panel)
		{
			Item item = Item.Instance();
			foreach (ItemInfo info in item.Hats)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(45) });

				CheckBox obtain = new CheckBox();
				status.Add(new HatObtain(obtain, info.ID));
				mHatButtonCheck.Append(obtain);
				obtain.Content = info.Name;
				obtain.VerticalAlignment = VerticalAlignment.Center;
				grid.Children.Add(obtain);

				TextBox ticket = new TextBox();
				status.Add(new AllNumberStatus(ticket, Util.HatStartAddress + info.ID, 1, 0, 99));
				ticket.SetValue(Grid.ColumnProperty, 1);

				grid.Children.Add(ticket);
				panel.Children.Add(grid);
			}
		}

		private void CreateTitle(List<AllStatus> status, ListBox list)
		{
			Item item = Item.Instance();
			foreach (ItemInfo info in item.Titles)
			{
				CheckBox obtain = new CheckBox();
				status.Add(new TitleObtain(obtain, info.ID));
				mTitleButtonCheck.Append(obtain);
				obtain.Content = info.Name;
				list.Items.Add(obtain);
			}
		}
	}
}
