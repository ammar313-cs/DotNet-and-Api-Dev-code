using System;
using System.Windows;
using System.Windows.Controls;


namespace JsonCRUD
{
    public partial class MainWindow : Window
    {
        CrmCrud crud;
        private DataEntryControl DataEntryCtrl;

        public MainWindow()
        {

            InitializeComponent();

            crud = new CrmCrud();

            LoadDataEntryControl();

            DataEntryZone.Visibility = Visibility.Collapsed;
            RefreshGrid();
        }

        private void LoadDataEntryControl()
        {
            MyStack.Children.Clear();

            DataEntryCtrl = new DataEntryControl();
            DataEntryCtrl.onActionHandler += DataEntryCtrl_onActionHandler;
            DataEntryCtrl.onCancelHandler += DataEntryCtrl_onCancelHandler;
            MyStack.Children.Add(DataEntryCtrl);
        }

        private void DataEntryCtrl_onCancelHandler()
        {
            DataEntryCtrl.ClearDataEntryForm();
            DataEntryZone.Visibility = Visibility.Collapsed;
        }

        private void DataEntryCtrl_onActionHandler(UIType type, bool isSuccess, string[]? messages)
        {
            DataEntryCtrl.ClearDataEntryForm();
            DataEntryZone.Visibility = Visibility.Collapsed;
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CrmCrud crm = new CrmCrud();
            var allData = crm.GetAllDisplayData();

            MyFriendsListBox.ItemsSource = null;
            MyFriendsListBox.ItemsSource = allData;
        }

        private void BtnNewRecord_Click(object sender, RoutedEventArgs e)
        {
            DataEntryZone.Visibility = Visibility.Visible;
            DataEntryCtrl.ChangeUIType(UIType.UICreate);
            DataEntryCtrl.ClearDataEntryForm();
            DataEntryCtrl.SetDataIsEnabled(true);
            DataEntryCtrl.NewRecordGuid();
            
        }

        #region ListBox Edit/Delete buttons

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (b.CommandParameter != null)
            {
                
                Guid id = Guid.Parse(b.CommandParameter.ToString());

                int selectedIndex = int.Parse(b.Tag.ToString());

                MyFriendsListBox.SelectedIndex = selectedIndex;

                DataEntryCtrl.ChangeUIType(UIType.UIEdit);
                DataEntryCtrl.LoadForm(id);
                DataEntryCtrl.SetDataIsEnabled(true);
                DataEntryZone.Visibility= Visibility.Visible;
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (b.CommandParameter != null)
            {

                Guid id = Guid.Parse(b.CommandParameter.ToString());

                int selectedIndex = int.Parse(b.Tag.ToString());

                MyFriendsListBox.SelectedIndex = selectedIndex;
                
                DataEntryCtrl.ChangeUIType(UIType.UIDelete);
                DataEntryCtrl.LoadForm(id);
                DataEntryCtrl.SetDataIsEnabled(false);
                DataEntryZone.Visibility = Visibility.Visible;
            }
        }

        #endregion


    }
}
