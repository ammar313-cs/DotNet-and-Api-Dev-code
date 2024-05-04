using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonCRUD
{
    /// <summary>
    /// Interaction logic for DataEntryControl.xaml
    /// </summary>
    public partial class DataEntryControl : UserControl
    {
        public delegate void JsonActionHandler(UIType type, bool isSuccess, string[]? messages);
        public event JsonActionHandler onActionHandler;

        public delegate void CancelHandler();
        public event CancelHandler onCancelHandler;

        public DataEntryControl()
        {
            InitializeComponent();
        }


        private void BtnJsonCommand_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text.Length == 0)
            {
                MessageBox.Show("FirstName is required", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (tbLastName.Text.Length == 0)
            {
                MessageBox.Show("LastName is required", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (BtnJsonCommand.Content == null) return;
            var obj = GetFrmFromDataEntry();

            string commandType = BtnJsonCommand.Content.ToString();

            switch (commandType.ToLower().Trim())
            {
                case "update record":
                    {
                        JsonUpdate(obj);
                    }
                    break;
                case "insert record":
                    {
                        JsonInsert(obj);
                    }
                    break;
                case "delete record":
                    {
                        JsonDelete(obj);
                    }
                    break;
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (onCancelHandler != null)
            {
                onCancelHandler();
            }
        }

        public void LoadForm(Guid? id)
        {
            Crm rec = new Crm();
            if (id.HasValue)
                rec.Id = id.Value;
            else
            {
                ClearDataEntryForm();
                return;
            }

            CrmCrud crud = new CrmCrud();
            List<Crm> allRecords = crud.JsonFileFindRecord(rec);

            

            Crm record = allRecords.FirstOrDefault(p => p.Id == id);
            if (record != null)
            {
                this.tbID.Text = id.ToString();
                this.tbFirstName.Text = record.FirstName.ToString();
                this.tbLastName.Text = record.LastName.ToString();
                if (record.CellPhoneNum != null)
                {
                    this.tbCellPhone.Text = record.CellPhoneNum.ToString();
                }
            }
        }

        public void ChangeUIType(UIType uIEdit)
        {
            switch (uIEdit)
            {
                case UIType.UIEdit:
                    {
                        TBServiceDescription.Text = "Edit Record";
                        BtnJsonCommand.Content = "Update Record";
                    }
                    break;
                case UIType.UICreate:
                    {
                        TBServiceDescription.Text = "Insert New Record";
                        BtnJsonCommand.Content = "Insert Record";
                    }
                    break;
                case UIType.UIDelete:
                    {
                        TBServiceDescription.Text = "Delete Record";
                        BtnJsonCommand.Content = "Delete Record";
                    }
                    break;
            }
        }

        private Crm GetFrmFromDataEntry()
        {
            var obj = new Crm();
            obj.Id = Guid.Parse(this.tbID.Text.ToString());
            obj.FirstName = tbFirstName.Text.Trim();
            obj.LastName = tbLastName.Text.Trim();

            if (tbCellPhone.Text.Trim().Length > 0)
                obj.CellPhoneNum = tbCellPhone.Text.Trim();
            else
                obj.CellPhoneNum = null;


            obj.CreateDate = DateTime.Now;

            return (obj);
        }

        private void ResetDataEntry()
        {
            ClearDataEntryForm();
            GridNewRecord.Visibility = Visibility.Collapsed;
        }

        public void ClearDataEntryForm()
        {
            this.tbID.Text = String.Empty;
            this.tbFirstName.Text = String.Empty;
            this.tbLastName.Text = String.Empty;
            this.tbCellPhone.Text = String.Empty;
        }


        public void NewRecordGuid()
        {
            this.tbID.Text = Guid.NewGuid().ToString();
            this.tbID.IsEnabled = false;
        }

        public void SetDataIsEnabled(bool state)
        {
            this.tbFirstName.IsEnabled = state;
            this.tbLastName.IsEnabled = state;
            this.tbCellPhone.IsEnabled = state;
        }

        private void JsonInsert(Crm obj)
        {
            CrmCrud crud = new CrmCrud();

            var tupValue = crud.JsonFileInsert(obj);

            if (onActionHandler != null)
            {
                onActionHandler(UIType.UICreate, tupValue.Success, tupValue.errors);
            }

        }

        private void JsonUpdate(Crm obj)
        {
            CrmCrud crud = new CrmCrud();

            var tupValue = crud.JsonFileUpdate(obj);

            if (onActionHandler != null)
            {
                onActionHandler(UIType.UIEdit, tupValue.Success, tupValue.errors);
            }
        }

        private void JsonDelete(Crm obj)
        {
            CrmCrud crud = new CrmCrud();
            var tupValue = crud.JsonFileDelete(obj);

            if (onActionHandler != null)
            {
                onActionHandler(UIType.UIDelete, tupValue.Success, tupValue.errors);
            }
        }

        private void tbID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
