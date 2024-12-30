using BusinessObjects;
using BusinessObjects.Models;
using DataAccessObjects;
using Microsoft.Identity.Client.NativeInterop;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NguyenAnhVietWPF
{
    /// <summary>
    /// Interaction logic for ManageBooking.xaml
    /// </summary>
    public partial class ManageBooking : Window
    {
        private readonly ICustomerService iCustomerService;
        private readonly IBookingDetailService iBookingDetailService;
        private readonly IBookingReservationService iBookingReservationService;
        private readonly IRoomInformationService iRoomInformationService;
        private readonly IRoomTypeService iRoomTypeService;

        public ManageBooking()
        {
            InitializeComponent();
            iCustomerService = new CustomerService();
            iBookingDetailService = new BookingDetailService();
            iBookingReservationService = new BookingReservationService();
            iRoomInformationService = new RoomInformationService();
            iRoomTypeService = new RoomTypeService();
            InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            await LoadBookingReservationList();
            await LoadRoomInformationList();
             await LoadBookingDetailsList();
            await LoadReservationList();
            await LoadCustomerList();
        }
        public async Task LoadRoomInformationList()
        {
            try
            {
                var roomInformation = await iRoomInformationService.GetRoomInformations();
                cboRooms.ItemsSource = roomInformation;
                if (roomInformation.IsNullOrEmpty())
                {
                    MessageBox.Show("No information of Room Types");
                }
                cboRooms.DisplayMemberPath = "RoomNumber";
                cboRooms.SelectedValuePath = "RoomId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading room Information");
            }
        }
        public async Task LoadReservationList()
        {
            try
            {
                var roomReservation = await iBookingReservationService.GetBookingReservations();

                if (roomReservation == null || !roomReservation.Any())
                {
                    MessageBox.Show("No booking reservations available.");
                    cboReservation.ItemsSource = null; // Ensure no stale data
                    return;
                }

                cboReservation.ItemsSource = roomReservation;
                cboReservation.DisplayMemberPath = "BookingReservationId";
                cboReservation.SelectedValuePath = "BookingReservationId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading Reservation");
            }
        }


        private async Task LoadBookingDetailsList()
        {
            try
            {
                // Retrieve all booking details asynchronously
                var bookingDetailList = await iBookingDetailService.GetBookingDetails();

                if (bookingDetailList == null || !bookingDetailList.Any())
                {
                    System.Windows.MessageBox.Show("No booking details found.");
                    dgBookingDetails.ItemsSource = null;
                    return;
                }

                // Set the DataGrid's ItemsSource to the full list of booking details
                dgBookingDetails.ItemsSource = bookingDetailList;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading booking details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadBookingReservationList()
        {
            try
            {
                var bookingReservations = await iBookingReservationService.GetBookingReservations();
                dgBookingReservations.ItemsSource = bookingReservations;

                foreach (var reservation in bookingReservations)
                {
                    Console.WriteLine($"ID: {reservation.BookingReservationId}, Date: {reservation.BookingDate}, Price: {reservation.TotalPrice}, Customer: {reservation.CustomerId}, Status: {reservation.BookingStatus}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booking reservations: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task LoadCustomerList()
        {
            try
            {
                var customers = await iCustomerService.GetCustomers();
                cboCustomers.ItemsSource = customers;
                if (customers.IsNullOrEmpty())
                {
                    MessageBox.Show("No information of Customers");
                }
                cboCustomers.DisplayMemberPath = "CustomerFullName";
                cboCustomers.SelectedValuePath = "CustomerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading Customer");
            }
        }
        private async void btnUpdateBookingDetail_Click(object sender, RoutedEventArgs e)
        {
            string dateFormat = "dd/MM/yyyy";
            int pastRoomId = 0;
            int pastBookingReservationId = 0;
            try
            {
                // Get input values
                if (!int.TryParse(cboReservation.SelectedValue?.ToString(), out int bookingReservationID) ||
                    !int.TryParse(cboRooms.SelectedValue?.ToString(), out int roomId) ||
                    !DateOnly.TryParseExact(txtStartDay.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate) ||
                    !DateOnly.TryParseExact(txtEndDay.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly endDate))
                {
                    MessageBox.Show("Please enter valid Booking Reservation ID, Room ID, and dates in the format dd/MM/yyyy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (dgBookingDetails.SelectedItem is BookingDetail OldBookingDetail)
                {
                    pastRoomId = OldBookingDetail.RoomId;
                    pastBookingReservationId = OldBookingDetail.BookingReservationId;
                }
                else
                {
                    MessageBox.Show("Please select a booking detail to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Calculate the actual price
                int days = (endDate.DayNumber - startDate.DayNumber);
                if (days <= 0)
                {
                    MessageBox.Show("End date must be after start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var room = await iRoomInformationService.GetRoomInformation(roomId); // Ensure this is an async method
                if (room == null)
                {
                    MessageBox.Show($"Room with ID {roomId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                decimal newActualPrice = room.RoomPricePerDate * days;

                // Get the current booking detail
                var currentBookingDetails = await iBookingDetailService.GetBookingDetail(pastBookingReservationId, pastRoomId);
                if (currentBookingDetails == null || !currentBookingDetails.Any())
                {
                    MessageBox.Show("Booking detail not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var currentBookingDetail = currentBookingDetails.FirstOrDefault();
                if (currentBookingDetail == null)
                {
                    MessageBox.Show("Booking detail not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Deduct the old actual price from the past reservation
                var pastBookingReservations = await iBookingReservationService.GetBookingReservation(null, pastBookingReservationId);
                var pastBookingReservation = pastBookingReservations.FirstOrDefault();
                if (pastBookingReservation.BookingReservationId != bookingReservationID )
                {
                    pastBookingReservation.TotalPrice -= OldBookingDetail.ActualPrice;

                    await iBookingReservationService.UpdateBookingReservation(pastBookingReservation);
                }

                await iBookingDetailService.DeleteBookingDetail(pastBookingReservationId, pastRoomId);

                // Update or create the booking detail for the new reservation
                currentBookingDetail.StartDate = startDate;
                currentBookingDetail.EndDate = endDate;
                currentBookingDetail.RoomId = roomId;
                currentBookingDetail.BookingReservationId = bookingReservationID;
                decimal priceDifference = newActualPrice - OldBookingDetail.ActualPrice;
                currentBookingDetail.ActualPrice = newActualPrice;
                // Get the new booking reservation
                var newBookingReservations = await iBookingReservationService.GetBookingReservation(null, bookingReservationID);
                var newBookingReservation = newBookingReservations.FirstOrDefault();
                
                if (newBookingReservation != null )
                {
                    if (pastBookingReservation.BookingReservationId != bookingReservationID)
                    {
                        newBookingReservation.TotalPrice += OldBookingDetail.ActualPrice;

                        await iBookingReservationService.UpdateBookingReservation(newBookingReservation);
                    }   
                    newBookingReservation.TotalPrice += priceDifference  ;
                    
                    await iBookingDetailService.UpdateBookingDetail(currentBookingDetail);
                    await iBookingReservationService.UpdateBookingReservation(newBookingReservation);

                    MessageBox.Show("Update successfully");

                    // Refresh the DataGrids
                    await InitializeAsync();
                }
                else
                {
                    MessageBox.Show("New booking reservation not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddBookingDetail_Click(object sender, RoutedEventArgs e)
        {
            string dateFormat = "dd/MM/yyyy";
            try
            {
                // Get input values
                if (!int.TryParse(cboReservation.SelectedValue?.ToString(), out int bookingReservationID) ||
                    !int.TryParse(cboRooms.SelectedValue?.ToString(), out int roomId) ||
                    !DateOnly.TryParseExact(txtStartDay.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate) ||
                    !DateOnly.TryParseExact(txtEndDay.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly endDate))
                {
                    MessageBox.Show("Please enter valid Booking Reservation ID, Room ID, and dates in the format dd/MM/yyyy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Calculate the actual price
                int days = (endDate.DayNumber - startDate.DayNumber);
                if (days <= 0)
                {
                    MessageBox.Show("End date must be after start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var room = await iRoomInformationService.GetRoomInformation(roomId); // Ensure this is an async method
                if (room == null)
                {
                    MessageBox.Show($"Room with ID {roomId} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                decimal actualPrice = room.RoomPricePerDate * days;

                // Create new booking detail
                BookingDetail newBookingDetail = new BookingDetail
                {
                    BookingReservationId = bookingReservationID,
                    RoomId = roomId,
                    StartDate = startDate,
                    EndDate = endDate,
                    ActualPrice = actualPrice
                };

                // Get the current booking reservation
                var bookingReservations = await iBookingReservationService.GetBookingReservation(null, bookingReservationID);
                var bookingReservation = bookingReservations.FirstOrDefault();
                if (bookingReservation == null)
                {
                    MessageBox.Show("Booking reservation not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Update the total price of the reservation
                bookingReservation.TotalPrice += actualPrice;

                // Add new booking detail and update the reservation
                await iBookingDetailService.AddBookingDetail(newBookingDetail);
                await iBookingReservationService.UpdateBookingReservation(bookingReservation);

                MessageBox.Show("Booking detail added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the DataGrids
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding booking detail: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void btnAddBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            string dateFormat = "dd/MM/yyyy";
            try
            {
                int bookingReservationID = int.Parse(txtBookingReservationIDReservation.Text);
                int customerId = int.Parse(cboCustomers.SelectedValue.ToString());
                DateOnly reservationDate = DateOnly.ParseExact(txtBookingDate.Text, dateFormat, CultureInfo.InvariantCulture);
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text);
                string status = txtStatus.Text;

                // Create new booking reservation
                BookingReservation newBookingReservation = new BookingReservation
                {
                    BookingReservationId = bookingReservationID,
                    CustomerId = customerId,
                    BookingDate = reservationDate,
                    TotalPrice = totalPrice,
                    BookingStatus = status
                };

                // Add new booking reservation
                await iBookingReservationService.AddBookingReservation(newBookingReservation);

                MessageBox.Show("Booking reservation added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the DataGrids
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding booking reservation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUpdateBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            string dateFormat = "dd/MM/yyyy";
            int oldCustomerId = 0;  
            try
            {
                // Directly get input values
                int bookingReservationID = int.Parse(txtBookingReservationIDReservation.Text);
                int customerId = int.Parse(cboCustomers.SelectedValue.ToString());

                DateOnly reservationDate = DateOnly.ParseExact(txtBookingDate.Text, dateFormat, CultureInfo.InvariantCulture);
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text);
                string status = txtStatus.Text;
                if (dgBookingReservations.SelectedItem is BookingReservation oldBookingReservation){
                  
                     oldCustomerId = oldBookingReservation.CustomerId;
                }
                // Fetch the existing booking reservation
                var bookingReservations = await iBookingReservationService.GetBookingReservation(oldCustomerId, bookingReservationID);
                var bookingReservation = bookingReservations.FirstOrDefault();
                if (bookingReservation == null)
                {
                    MessageBox.Show("Booking reservation not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

               
        bookingReservation.CustomerId = customerId;
                bookingReservation.BookingDate = reservationDate;
                bookingReservation.TotalPrice = totalPrice;
                bookingReservation.BookingStatus = status;

                // Update the booking reservation
                await iBookingReservationService.UpdateBookingReservation(bookingReservation);

                MessageBox.Show("Booking reservation updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the DataGrids
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating booking reservation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnManageRooms_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminWindow manageRoom = new AdminWindow();
            manageRoom.ShowDialog();
        }

        private async void btnDeleteBookingDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if a booking detail is selected
                if (dgBookingDetails.SelectedItem is not BookingDetail selectedBookingDetail)
                {
                    MessageBox.Show("Please select a booking detail to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var result = MessageBox.Show($"Are you sure you want to delete BookingDetail from Reservation {selectedBookingDetail.BookingReservationId}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    return; // User chose not to delete
                }
                // Retrieve the booking reservation
                var bookingReservations = await iBookingReservationService.GetBookingReservation(null,selectedBookingDetail.BookingReservationId);
                var bookingReservation = bookingReservations.FirstOrDefault();
                if (bookingReservation == null)
                {
                    MessageBox.Show("Booking reservation not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Calculate the price difference
                decimal priceDifference = selectedBookingDetail.ActualPrice;

                // Delete the booking detail
                await iBookingDetailService.DeleteBookingDetail(selectedBookingDetail.BookingReservationId, selectedBookingDetail.RoomId);

                // Update the booking reservation's total price
                bookingReservation.TotalPrice -= priceDifference;

                // Update the booking reservation in the database
                await iBookingReservationService.UpdateBookingReservation(bookingReservation);

                // Refresh the DataGrids
                await InitializeAsync();

                MessageBox.Show("Booking detail deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting booking detail: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void btnDeleteBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgBookingReservations.SelectedItem is BookingReservation selectedBookingReservation)
                {
                    var result = MessageBox.Show($"Are you sure you want to delete BookingReservation {selectedBookingReservation.BookingReservationId}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.No)
                    {
                        return; // User chose not to delete
                    }
                    int bookingReservationId = selectedBookingReservation.BookingReservationId;

                    // Retrieve and delete all booking details associated with the selected booking reservation
                    var bookingDetails = await iBookingDetailService.GetBookingDetail(bookingReservationId, null);
                    foreach (var bookingDetail in bookingDetails)
                    {
                        await iBookingDetailService.DeleteBookingDetail(bookingDetail.BookingReservationId, bookingDetail.RoomId);
                    }

                    // Delete the booking reservation itself
                    await iBookingReservationService.DeleteBookingReservation(bookingReservationId);

                    MessageBox.Show("Booking reservation and associated booking details deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Refresh the DataGrids
                    await InitializeAsync();
                }
                else
                {
                    MessageBox.Show("Please select a booking reservation to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting booking reservation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void dgBookingDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgBookingDetails.SelectedItem == null || !(dgBookingDetails.SelectedItem is BookingDetail selectedBookingDetail))
                    return;

                cboReservation.SelectedValue = selectedBookingDetail.BookingReservationId;
                cboRooms.SelectedValue = selectedBookingDetail.RoomId;
                txtStartDay.Text = selectedBookingDetail.StartDate.ToString("dd/MM/yyyy");
                txtEndDay.Text = selectedBookingDetail.EndDate.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void dgBookingReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Get the selected item from the DataGrid
                if (dgBookingReservations.SelectedItem == null || !(dgBookingReservations.SelectedItem is BookingReservation selectedBookingReservation))
                    return;

                // Update the TextBox and ComboBox with the selected booking reservation details
                txtBookingReservationIDReservation.Text = selectedBookingReservation.BookingReservationId.ToString();
                txtBookingDate.Text = selectedBookingReservation.BookingDate.ToString("dd/MM/yyyy");  // Ensure the date format matches
                txtTotalPrice.Text = selectedBookingReservation.TotalPrice.ToString(); 
                txtStatus.Text = selectedBookingReservation.BookingStatus;

                // Set the selected customer in the ComboBox
                cboCustomers.SelectedValue = selectedBookingReservation.CustomerId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private async void btnSearchBookings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the search Customer ID from the text box
                if (!int.TryParse(txtSearchWord.Text.Trim(), out int customerId))
                {
                    MessageBox.Show("Please enter a valid Customer ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Fetch all booking reservations
                var allBookings = await iBookingReservationService.GetBookingReservations();

                // Filter bookings by Customer ID using LINQ
                var filteredBookings = allBookings
                    .Where(b => b.CustomerId == customerId)
                    .ToList();

                if (filteredBookings == null || !filteredBookings.Any())
                {
                    MessageBox.Show("No bookings found for this Customer ID.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgBookingReservations.ItemsSource = null;  // Clear the DataGrid if no results
                }
                else
                {
                    // Update the DataGrid with the search results
                    dgBookingReservations.ItemsSource = filteredBookings;
                }
            }
            catch (Exception ex)
            {
                // Display inner exception details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                MessageBox.Show($"An error occurred: {ex.Message}\nInner Exception: {innerExceptionMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





    }
}
