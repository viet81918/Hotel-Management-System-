using BusinessObjects; // Assuming RoomInformation and RoomType are in BusinessObjects namespace
using DataAccessObjects;
using Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NguyenAnhVietWPF
{
    public partial class AdminWindow : Window
    {
        private readonly IRoomInformationService iRoomInformationService;
        private readonly IRoomTypeService iRoomTypeService;

        public AdminWindow()
        {
            InitializeComponent();

            // Initialize services
            iRoomInformationService = new RoomInformationService();
            iRoomTypeService = new RoomTypeService();

            // Load initial data
            LoadRoomTypeList();
            LoadRoomInformationList();
        }

        private void LoadRoomTypeList()
        {
            try
            {
                var roomTypes = iRoomTypeService.GetAllRoomTypes();
                cboCategory.ItemsSource = roomTypes;
                cboCategory.DisplayMemberPath = "RoomTypeName";
                cboCategory.SelectedValuePath = "RoomTypeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading room types");
            }
        }

        private void LoadRoomInformationList()
        {
            try
            {
                var roomInformationList = RoomInformationDAO.GetRoomInformations();
                dgData.ItemsSource = roomInformationList;
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input controls
                if (string.IsNullOrWhiteSpace(txtRoomId.Text) || string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
                    string.IsNullOrWhiteSpace(txtRoomDescription.Text) || string.IsNullOrWhiteSpace(txtRoomMaxCapicity.Text) ||
                    string.IsNullOrWhiteSpace(txtRoomStatus.Text) || string.IsNullOrWhiteSpace(txtRoomPrice.Text) ||
                    cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("One or more input fields are empty.");
                    return;
                }

                // Create RoomInformation object
                RoomInformation room = new RoomInformation
                {
                    RoomID = Int32.Parse(txtRoomId.Text),
                    RoomNumber = txtRoomNumber.Text,
                    RoomDescription = txtRoomDescription.Text,
                    RoomMaxCapacity = Int32.Parse(txtRoomMaxCapicity.Text),
                    RoomStatus = Int32.Parse(txtRoomStatus.Text),
                    RoomPricePerDate = Double.Parse(txtRoomPrice.Text),
                    RoomTypeID = (int)cboCategory.SelectedValue
                };

                // Save room information
                iRoomInformationService.AddRoomInformation(room);

                // Refresh room information list
                LoadRoomInformationList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is RoomInformation selectedRoom))
                    return;

                txtRoomId.Text = selectedRoom.RoomID.ToString();
                txtRoomNumber.Text = selectedRoom.RoomNumber;
                txtRoomDescription.Text = selectedRoom.RoomDescription;
                txtRoomMaxCapicity.Text = selectedRoom.RoomMaxCapacity.ToString();
                txtRoomStatus.Text = selectedRoom.RoomStatus.ToString();
                txtRoomPrice.Text = selectedRoom.RoomPricePerDate.ToString();
                cboCategory.SelectedValue = selectedRoom.RoomTypeID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is RoomInformation selectedRoom))
                {
                    MessageBox.Show("Please select a room to update.");
                    return;
                }

                // Update selected room information
                selectedRoom.RoomNumber = txtRoomNumber.Text;
                selectedRoom.RoomDescription = txtRoomDescription.Text;
                selectedRoom.RoomMaxCapacity = Int32.Parse(txtRoomMaxCapicity.Text);
                selectedRoom.RoomPricePerDate = Double.Parse(txtRoomPrice.Text);
                selectedRoom.RoomStatus = Int32.Parse(txtRoomStatus.Text);
                selectedRoom.RoomTypeID = (int)cboCategory.SelectedValue;
                iRoomInformationService.UpdateRoomInformation(selectedRoom);
                // Notify DataGrid of the update
                dgData.Items.Refresh();
                ResetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is RoomInformation selectedRoom))
                {
                    MessageBox.Show("Please select a room to delete.");
                    return;
                }

                // Delete selected room
                RoomInformationDAO.DeleteRoomInformation(selectedRoom.RoomID);

                // Refresh room information list
                LoadRoomInformationList();
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
            txtRoomMaxCapicity.Text = "";
            txtRoomStatus.Text = "";
            txtRoomPrice.Text = "";
            cboCategory.SelectedValue = null;
        }
        private void btnManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageCustomer manageCustomer = new ManageCustomer();
            manageCustomer.ShowDialog(); 
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
