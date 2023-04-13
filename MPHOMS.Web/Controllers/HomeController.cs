using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPHOMS.Web.Models;


using MPHOMS.Web.Repository;
using MPHOMS.Web.Models.Commerce;
using MPHOMS.Web.Models.Delivery;
using Microsoft.EntityFrameworkCore;
using MPHOMS.Web.ViewModels;
using Microsoft.Data.SqlClient;
//using MPHOMS.Web.ViewModels;

namespace MPHOMS.Web.Controllers
{
    public class HomeController : Controller
    {
        #region variables
        private readonly IOrderRepository _repository;
        private readonly ILogger<HomeController> _logger;
        private readonly CommerceDbContext _context;
        private readonly DeliveryDbContext _contextdelivery;
        #endregion

        public HomeController(ILogger<HomeController> logger, DeliveryDbContext contextdelivery, CommerceDbContext context, IOrderRepository repository)
        {
            _context = context;
            _contextdelivery = contextdelivery;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET Order List
        [HttpGet]
        public IActionResult GetOrders()
        {
            //DateTime timeUtc = DateTime.UtcNow;
            //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            //string date = cstTime.ToLongDateString();
            return Ok(_repository.OrderList);
        }

        // Show order detail partial view 
        public async Task<IActionResult> ShowOrder(int? Id)
        {
                var ShippingDetails = await GetOrderShippingDetailByOrderAsync(Id);
                var OrderTotal = await GetOrderTotalByOrderAsync(Id);
                var viewModel = new OrderViewModel
                {
                    Orders = _context.Order.Where(m => m.Id == Id),
                    GetOrderDetail = await GetOrderDetailByOrderAsync(Id),
                    GetOrderShippingDetail = ShippingDetails.FirstOrDefault(),
                    GetOrderItemAttributes = await GetOrderItemAttributeByOrderAsync(Id),
                    GetOrderTotal = OrderTotal.FirstOrDefault()
                };

            return PartialView("_OrderDetail", viewModel);
        }

        // Comand with order detail
        [HttpGet]
        public async Task<JsonResult> LoadCommands(int? Id)
        {
            // Initialization.  
            var OrderList = await GetOrderHeaderReceiptAsync(Id);
            var OrderDetail = await GetOrderDetailByOrderAsync(Id);
            var OrderTotal = await GetOrderTotalByOrderAsync(Id);
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            //cmds = string.Empty;
            //Create ESC/POS commands for sample receipt
            var esc = "\x1B"; //ESC byte in hex notation
            var gs = "\x1D"; //ESC byte in hex notation
            var newLine = "\x0A"; //LF byte in hex notation

            //cmds = ESC + "@"; //Initializes the printer (ESC @)
            var cmds = esc + "@"; //Initializes the printer (ESC @)
            cmds += newLine;
            cmds += esc + "!" + "\x18";
            cmds += esc + "a" + "\x31"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 12 + 32)) 56 dec => 38 hex
                                        //HEADER
            foreach (var item in OrderList)
            {
                cmds += item.Email; //text to print //Client's name
                cmds += newLine + newLine;
                cmds += esc + "!" + "\x18";
                //cmds += "11 items"; //text to print
                cmds += newLine;
                cmds += "Order Time"; //text to print
                cmds += newLine;
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(item.CreatedOnUtc, cstZone);
                string date = cstTime.ToLongDateString();
                string hour = cstTime.Hour.ToString();
                cmds += date; //text to print
                cmds += newLine;
                cmds += hour + ":" + cstTime.Minute.ToString() + ":" + cstTime.Second.ToString();
            }
            cmds += newLine + newLine;
            cmds += esc + "!" + "\x14";
            cmds += esc + "a" + "\x30";
            //ORDER ITEMS
            foreach (var item in OrderDetail)
            {
                cmds += newLine + newLine;
                cmds += "-----------------------------------------";//text to print
                cmds += newLine;
                cmds += item.Quantity + "x" + " " + item.Name + "...      " + "$" + item.PriceExclTax.ToString("0.00");//text to print
                cmds += newLine;
                //var OrderAttribute = _context.VNewOrderItemAttribute.Where(m => (m.OrderId == Id) && (m.OrderItemId == item.OrderItemId));
                var OrderAttribute = await GetOrderItemAttributeByOrderAsync(Id);
                var attributes = OrderAttribute.Where(m => m.OrderItemId == item.OrderItemId);
                if (attributes != null)
                {
                    foreach (var attribute in attributes)
                    {
                        string m = attribute.Attributedescription.Replace("\\x0A", "\x0A");
                        cmds += m;
                    }
                }
            }
            cmds += newLine + newLine;
            cmds += "-----------------------------------------";//text to print
            cmds += newLine;
            foreach (var item in OrderTotal)
            {
                cmds += newLine;
                cmds += "Sub-Total: " + item.OrderSubtotalExclTax.ToString("0.00");//text to print
                cmds += newLine;
                cmds += "Shipping: " + item.OrderShippingExclTax.ToString("0.00");//text to print
                cmds += newLine;
                cmds += "Tax: " + item.OrderTax.ToString("0.00");//text to print
                cmds += newLine;
                cmds += "Discount: " + item.OrderDiscount.ToString("0.00");//text to print
                cmds += newLine;
                cmds += "Order Total: " + item.OrderTotal.ToString("0.00");//text to print
                cmds += newLine;
            }
            cmds += newLine + newLine;
            cmds += "Thank for you purchase ";//text to print
            cmds += newLine;
            cmds += "www.mipequenahacienda.com";//text to print
            cmds += newLine + newLine;
            cmds += " ";//text to print
            cmds += newLine;
            cmds += newLine;
            cmds += newLine + newLine;
            cmds += gs + "V" + "\x31"; //cutter control, cuts paper Partial cut (One point Uncut)
            cmds += newLine + newLine;
            cmds += "Final";//text to print

            return Json(cmds);
        }

        // Edit pickup status method 20 to 30(Completed)
        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPickupStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderToUpdate = await _context.Order.FirstOrDefaultAsync(s => s.Id == id);
            var orderNotificationToUpdate = await _contextdelivery.OrderNotificationStore1.FirstOrDefaultAsync(s => s.OrderId == id);

            try
            {
                orderToUpdate.OrderStatusId = 30;
                orderNotificationToUpdate.OrderStatusId = 30;
                await _context.SaveChangesAsync();
                await _contextdelivery.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return View(orderToUpdate);
        }

        // Edit shipment status method 20 to 30(Completed)
        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShipmentStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var orderToUpdate = await _context.Order.FirstOrDefaultAsync(s => s.Id == id);
                var orderitems = _context.OrderItem.Where(i => i.OrderId == id);

                try
                {
                    orderToUpdate.OrderStatusId = 30;
                    var shipmentToAdd = new Shipment { OrderId = orderToUpdate.Id, TrackingNumber = null, TotalWeight = Convert.ToDecimal(0.0000), ShippedDateUtc = null, DeliveryDateUtc = null, AdminComment = null, CreatedOnUtc = DateTime.Now };
                    _context.Shipment.Add(shipmentToAdd);
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    int shipID = shipmentToAdd.Id;

                    foreach (OrderItem item in orderitems)
                    {
                        var orderitemToAdd = new ShipmentItem { ShipmentId = shipID, OrderItemId = item.Id, Quantity = item.Quantity, WarehouseId = 0 };
                        _context.ShipmentItem.Add(orderitemToAdd);
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                    }

                    dbContextTransaction.Commit();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }

                return View(orderToUpdate);
            }

        }

        #region Get Order Detail by ID store procedure method.  

        /// <summary>  
        /// Get order detail by ID store procedure method.  
        /// </summary>  
        /// <param name="orderId">Order ID value parameter</param>  
        /// <returns>Returns - List of Order Detail by ID</returns>  
        public async Task<List<usp_GetOrderDetailByOrder>> GetOrderDetailByOrderAsync(int? orderId)
        {
            // Initialization.  
            List<usp_GetOrderDetailByOrder> lst = new List<usp_GetOrderDetailByOrder>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@OrderId", orderId.ToString() ?? (object)DBNull.Value);

                // Processing.  
                string sqlQuery = "EXEC [dbo].[usp_GetOrderDetailByOrder] " + "@OrderId";

                lst = await _context.GetOrderDetailByOrder.FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }

        #endregion

        #region Get Order Header Receipt by ID store procedure method.  

        /// <summary>  
        /// Get order detail by ID store procedure method.  
        /// </summary>  
        /// <param name="orderId">Order ID value parameter</param>  
        /// <returns>Returns - List of Order Header Detail by ID</returns>  
        public async Task<List<usp_GetOrderHeaderReceipt>> GetOrderHeaderReceiptAsync(int? orderId)
        {
            // Initialization.  
            List<usp_GetOrderHeaderReceipt> lst = new List<usp_GetOrderHeaderReceipt>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@OrderId", orderId.ToString() ?? (object)DBNull.Value);

                // Processing.  
                string sqlQuery = "EXEC [dbo].[usp_GetOrderHeaderReceipt] " + "@OrderId";

                lst = await _context.GetOrderHeaderReceipt.FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }

        #endregion

        #region Get Order Shipping Detail by ID store procedure method.  

        /// <summary>  
        /// Get order shipping detail by ID store procedure method.  
        /// </summary>  
        /// <param name="orderId">Order ID value parameter</param>  
        /// <returns>Returns - List of Order Shipping Detail by ID</returns>  
        public async Task<List<usp_GetOrderShippingDetailByOrder>> GetOrderShippingDetailByOrderAsync(int? orderId)
        {
            // Initialization.  
            List<usp_GetOrderShippingDetailByOrder> lst = new List<usp_GetOrderShippingDetailByOrder>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@OrderId", orderId.ToString() ?? (object)DBNull.Value);

                // Processing.  
                string sqlQuery = "EXEC [dbo].[usp_GetOrderShippingDetailByOrder] " + "@OrderId";

                lst = await _context.GetOrderShippingDetailByOrder.FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                    throw ex;
            }

            // Info.  
            return lst;
        }

        #endregion

        #region Get Order Total Detail by ID store procedure method.  

        /// <summary>  
        /// Get order total detail by ID store procedure method.  
        /// </summary>  
        /// <param name="orderId">Order ID value parameter</param>  
        /// <returns>Returns - List of Order Total Detail by ID</returns>  
        public async Task<List<usp_GetOrderTotalByOrder>> GetOrderTotalByOrderAsync(int? orderId)
        {
            // Initialization.  
            List<usp_GetOrderTotalByOrder> lst = new List<usp_GetOrderTotalByOrder>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@OrderId", orderId.ToString() ?? (object)DBNull.Value);

                // Processing.  
                string sqlQuery = "EXEC [dbo].[usp_GetOrderTotalByOrder] " + "@OrderId";

                lst = await _context.GetOrderTotalByOrder.FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }

        #endregion

        #region Get Order Total Detail by ID store procedure method.  

        /// <summary>  
        /// Get order total detail by ID store procedure method.  
        /// </summary>  
        /// <param name="orderId">Order ID value parameter</param>  
        /// <returns>Returns - List of Order Total Detail by ID</returns>  
        public async Task<List<usp_GetOrderItemAttributeByOrder>> GetOrderItemAttributeByOrderAsync(int? orderId)
        {
            // Initialization.  
            List<usp_GetOrderItemAttributeByOrder> lst = new List<usp_GetOrderItemAttributeByOrder>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@OrderId", orderId.ToString() ?? (object)DBNull.Value);

                // Processing.  
                string sqlQuery = "EXEC [dbo].[usp_GetOrderItemAttributeByOrder] " + "@OrderId";

                lst = await _context.GetOrderItemAttributeByOrder.FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }

        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
