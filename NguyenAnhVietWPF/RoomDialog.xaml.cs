using BusinessObjects.Models;
using Microsoft.IdentityModel.Tokens;
using Services;
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
using System.Windows.Shapes;

namespace NguyenAnhVietWPF
{
    /// <summary>
    /// Interaction logic for RoomDialog.xaml
    /// </summary>
    public partial class RoomDialog : Window
    {
        private readonly IRoomInformationService iRoomInformationService;
        private readonly IRoomTypeService iRoomTypeService;
        public RoomDialog()
        {
            InitializeComponent();
            iRoomInformationService = new RoomInformationService();
            iRoomTypeService = new RoomTypeService();
      
            InitializeAsync();
        }
        private async Task InitializeAsync()
        {


            await LoadRoomTypeList();
            await LoadRoomInformationList();
        }
        public async Task LoadRoomInformationList()
        {
            try
            {
                var roomInformationList = await iRoomInformationService.GetRoomInformations();
                dgData.ItemsSource = roomInformationList;
                if (roomInformationList.IsNullOrEmpty())
                {
                    MessageBox.Show("No information of Room information");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading room information");
            }
            finally
            {
                ResetInput();
            }
        }

        public async Task LoadRoomTypeList()
        {
            try
            {
                var roomTypes = await iRoomTypeService.GetAllRoomTypes();
                cboCategory.ItemsSource = roomTypes;
                if (roomTypes.IsNullOrEmpty())
                {
                    MessageBox.Show("No information of Room Types");
                }
                cboCategory.DisplayMemberPath = "RoomTypeName";
                cboCategory.SelectedValuePath = "RoomTypeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading room types");
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is RoomInformation selectedRoom))
                    return;

                txtRoomId.Text = selectedRoom.RoomId.ToString();
                txtRoomNumber.Text = selectedRoom.RoomNumber;
                txtRoomDescription.Text = selectedRoom.RoomDescription;
                txtRoomMaxCapacity.Text = selectedRoom.RoomMaxCapacity.ToString();
                txtRoomStatus.Text = selectedRoom.RoomStatus.ToString();
                txtRoomPrice.Text = selectedRoom.RoomPricePerDate.ToString();
                cboCategory.SelectedValue = selectedRoom.RoomTypeId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void ResetInput()
        {
            txtRoomId.Text = "";
            txtRoomNumber.Text = "";
            txtRoomDescription.Text = "";
            txtRoomMaxCapacity.Text = "";
            txtRoomStatus.Text = "";
            txtRoomPrice.Text = "";
            cboCategory.SelectedValue = null;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate input controls
            if (string.IsNullOrWhiteSpace(txtRoomId.Text) || string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
                string.IsNullOrWhiteSpace(txtRoomDescription.Text) || string.IsNullOrWhiteSpace(txtRoomMaxCapacity.Text) ||
                string.IsNullOrWhiteSpace(txtRoomStatus.Text) || string.IsNullOrWhiteSpace(txtRoomPrice.Text)
                
                )
            {
                MessageBox.Show("One or more input fields are empty.");
                return;
            }

            // Close the dialog and set DialogResult to true
            this.DialogResult = true;
            this.Close();
        }

    }
  
}
