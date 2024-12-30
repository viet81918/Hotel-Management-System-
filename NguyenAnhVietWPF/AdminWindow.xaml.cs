using BusinessObjects.Models; // Assuming RoomInformation and RoomType are in BusinessObjects namespace
using DataAccessObjects;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IBookingDetailService iBookingDetailService;

        public AdminWindow()
        {
            InitializeComponent();

            // Initialize services
            iRoomInformationService = new RoomInformationService();
            iRoomTypeService = new RoomTypeService();
            iBookingDetailService = new BookingDetailService();
            InitializeAsync();
            // Load initial data

        }

              private async Task InitializeAsync()
        {

 
            await LoadRoomTypeList();
            await LoadRoomInformationList();
        }

        public async Task LoadRoomTypeList()
        {
            try
            {
                var roomTypes = await iRoomTypeService.GetAllRoomTypes();
                cboCategory.ItemsSource =  roomTypes;
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
                    RoomId = Int32.Parse(txtRoomId.Text),
                    RoomNumber = txtRoomNumber.Text,
                    RoomDescription = txtRoomDescription.Text,
                    RoomMaxCapacity = Int32.Parse(txtRoomMaxCapicity.Text),
                    RoomStatus = Int32.Parse(txtRoomStatus.Text),
                    RoomPricePerDate = Decimal.Parse(txtRoomPrice.Text),
                    RoomTypeId = (int)cboCategory.SelectedValue
                };

                // Save room information
                iRoomInformationService.AddRoomInformation(room);

                // Refresh room information list
                LoadRoomTypeList();
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

                txtRoomId.Text = selectedRoom.RoomId.ToString();
                txtRoomNumber.Text = selectedRoom.RoomNumber;
                txtRoomDescription.Text = selectedRoom.RoomDescription;
                txtRoomMaxCapicity.Text = selectedRoom.RoomMaxCapacity.ToString();
                txtRoomStatus.Text = selectedRoom.RoomStatus.ToString();
                txtRoomPrice.Text = selectedRoom.RoomPricePerDate.ToString();
                cboCategory.SelectedValue = selectedRoom.RoomTypeId;
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
                selectedRoom.RoomPricePerDate = Decimal.Parse(txtRoomPrice.Text);
                selectedRoom.RoomStatus = Int32.Parse(txtRoomStatus.Text);
                selectedRoom.RoomTypeId = (int)cboCategory.SelectedValue;
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

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is RoomInformation selectedRoom))
                {
                    MessageBox.Show("Please select a room to delete.");
                    return;
                }
                var result = MessageBox.Show($"Are you sure you want to delete Room Information {selectedRoom.RoomId}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    return; // User chose not to delete
                }
                // Check if the room is associated with any booking details
                var bookingDetails = await iBookingDetailService.GetBookingDetail(selectedRoom.RoomId);

                if (bookingDetails == null || !bookingDetails.Any())
                {
                    // Delete the room if it is not associated with any booking details
                    await iRoomInformationService.DeleteRoomInformation(selectedRoom.RoomId);
                    MessageBox.Show("Room deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Change the room status if it is associated with any booking details
                    selectedRoom.RoomStatus = 2; // Or any other status indicating it's not available for booking
                    await iRoomInformationService.UpdateRoomInformation(selectedRoom);
                    MessageBox.Show("Room status updated to 'Inactive'.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Refresh room information list
                dgData.Items.Refresh();
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                // Display inner exception details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                MessageBox.Show($"An error occurred: {ex.Message}\nInner Exception: {innerExceptionMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the search keyword from the text box
                string searchWord = txtSearchWord.Text.Trim();

                // Fetch all room information
                var allRooms = await iRoomInformationService.GetRoomInformations();

                // Filter rooms by name using LINQ
                var filteredRooms = allRooms
                    .Where(r => r.RoomNumber.Contains(searchWord, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (filteredRooms == null || !filteredRooms.Any())
                {
                    MessageBox.Show("No rooms found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgData.ItemsSource = null;  // Clear the DataGrid if no results
                }
                else
                {
                    // Update the DataGrid with the search results
                    dgData.ItemsSource = filteredRooms;
                    ResetInput();
                }
            }
            catch (Exception ex)
            {
                // Display inner exception details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                MessageBox.Show($"An error occurred: {ex.Message}\nInner Exception: {innerExceptionMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void btnManageBookings_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ManageBooking manageBooking = new ManageBooking();
            manageBooking.ShowDialog();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
