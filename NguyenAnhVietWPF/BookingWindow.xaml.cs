using BusinessObjects;
using DataAccessObjects;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Globalization;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
namespace NguyenAnhVietWPF
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private readonly Customer customer;
        private readonly ICustomerService iCustomerService;
        private readonly IBookingDetailService iBookingDetailService;
        private readonly IBookingReservationService iBookingReservationService;
        private readonly IRoomInformationService iRoomInformationService;
        private readonly IRoomTypeService iRoomTypeService;

        public BookingWindow(Customer customer)
        {
            InitializeComponent();
            this.customer = customer; // Store the customer data in the private field
            DataContext = this.customer; // Set DataContext to bind controls to customer properties
            iCustomerService = new CustomerService();
            iBookingDetailService = new BookingDetailService();
            iBookingReservationService = new BookingReservationService();
            iRoomInformationService = new RoomInformationService();
            iRoomTypeService = new RoomTypeService();
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadRoomInformationList();
            await LoadBookingReservationList(customer.CustomerId);
        }

        public async Task LoadRoomInformationList()
        {
            try
            {
                var roomInformationList = await iRoomInformationService.GetRoomInformations();

                if (roomInformationList == null || !roomInformationList.Any())
                {
                    Console.WriteLine("No room information found.");
                    dgRooms.ItemsSource = null;
                }
                else
                {
                    dgRooms.ItemsSource = roomInformationList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading room information: {ex.Message}");
            }
        }

        public async Task LoadBookingReservationList(int customerId)
        {
            try
            {
                var bookingReservationList = await iBookingReservationService.GetBookingReservation(customerId);

                if (bookingReservationList == null || !bookingReservationList.Any())
                {
                    Console.WriteLine("No Reservation found.");
                    dgBookings.ItemsSource = null;
                }
                else
                {
                    dgBookings.ItemsSource = bookingReservationList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading room information: {ex.Message}");
            }
        }

        private async void btnManageProfile_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CustomerWindow manageRoom = new CustomerWindow(this.customer);
            manageRoom.ShowDialog();
        }

        private async void btnAddBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            string dateFormat = "dd/MM/yyyy";
            try
            {
                // Validate and parse input values
                bool isBookingReservationIdValid = int.TryParse(txtBookingReservationID.Text, out int bookingReservationID);
                bool isRoomIdValid = int.TryParse(dgRooms.SelectedValue?.ToString(), out int roomId);
                bool isStartDateValid = DateOnly.TryParseExact(txtStartDate.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate);
                bool isEndDateValid = DateOnly.TryParseExact(txtEndDate.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly endDate);

                // Log parsed values
                System.Windows.MessageBox.Show($"Parsed Values:\nReservation ID: {bookingReservationID}\nRoom ID: {roomId}\nStart Date: {startDate}\nEnd Date: {endDate}", "Debug Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // Check if input values are valid
                if (!isBookingReservationIdValid || !isRoomIdValid || !isStartDateValid || !isEndDateValid)
                {
                    System.Windows.MessageBox.Show("Please enter valid input values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // Ensure end date is after start date
                if (endDate <= startDate)
                {
                    System.Windows.MessageBox.Show("End date must be after start date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Get selected room
                if (dgRooms.SelectedItem is RoomInformation selectedRoom)
                {
                    DateOnly bookingDate = DateOnly.FromDateTime(DateTime.Now);
                    var existingBookingReservations = await iBookingReservationService.GetBookingReservation(null, bookingReservationID);
                    var existingBookingReservation = existingBookingReservations.FirstOrDefault();

                    // Calculate actual price
                    int days = (endDate.DayNumber - startDate.DayNumber);
                    decimal actualPrice = selectedRoom.RoomPricePerDate * days;

                    if (existingBookingReservation != null)
                    {
                        // Update existing booking reservation's total price
                        existingBookingReservation.TotalPrice += actualPrice;
                        await iBookingReservationService.UpdateBookingReservation(existingBookingReservation);

                        // Add new booking detail
                        var newBookingDetail = new BookingDetail
                        {
                            BookingReservationId = bookingReservationID,
                            RoomId = roomId,
                            StartDate = startDate,
                            EndDate = endDate,
                            ActualPrice = actualPrice
                        };
                        await iBookingDetailService.AddBookingDetail(newBookingDetail);
                    }
                    else
                    {
                        // Create new booking reservation
                        var newBookingReservation = new BookingReservation
                        {
                            BookingReservationId = bookingReservationID,
                            BookingDate = bookingDate,
                            TotalPrice = actualPrice,
                            CustomerId = customer.CustomerId,
                            BookingStatus = "1"
                        };

                        var newBookingDetail = new BookingDetail
                        {
                            BookingReservationId = bookingReservationID,
                            RoomId = roomId,
                            StartDate = startDate,
                            EndDate = endDate,
                            ActualPrice = actualPrice
                        };

                        // Add booking reservation and booking detail
                        await iBookingReservationService.AddBookingReservation(newBookingReservation);
                        await iBookingDetailService.AddBookingDetail(newBookingDetail);
                    }

                    // Refresh the DataGrid
                    await LoadBookingReservationList(customer.CustomerId);

                    System.Windows.MessageBox.Show("Booking added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Please select a room.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions
                System.Windows.MessageBox.Show($"Database error adding booking: {dbEx.InnerException?.Message ?? dbEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException formatEx)
            {
                // Handle format exceptions
                System.Windows.MessageBox.Show($"Invalid data format: {formatEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                System.Windows.MessageBox.Show($"Error adding booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
