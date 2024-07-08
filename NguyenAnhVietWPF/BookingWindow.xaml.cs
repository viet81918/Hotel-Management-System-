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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NguyenAnhVietWPF
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private Customer customer;
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
            iRoomTypeService = new RoomTypeService();
            LoadRoomInformationList();
            LoadBookingInformationList(customer.CustomerID);
        }
        private void LoadRoomInformationList()
        {
            try
            {
                var roomInformationList = RoomInformationDAO.GetRoomInformations();
                dgRooms.ItemsSource = roomInformationList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading room information");
            }
            finally
            {
             
            }
        }
        private void LoadBookingInformationList(int customerId)
        {
            try
            {
                var bookingReservationList = BookingReservationDAO.GetBookingReservation(customerId);

                if (bookingReservationList == null || !bookingReservationList.Any())
                {
                    MessageBox.Show("No booking reservations found for the customer.");
                    dgBookings.ItemsSource = null;
                    return;
                }

                // Assuming you want to load booking details for the first reservation found
                var firstBookingReservation = bookingReservationList.FirstOrDefault();
                if (firstBookingReservation != null)
                {
                    var bookingInformationList = BookingDetailDAO.GetBookingDetail(firstBookingReservation.BookingReservationID);
                    dgBookings.ItemsSource = bookingInformationList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading booking information");
            }


        }



    }
}
