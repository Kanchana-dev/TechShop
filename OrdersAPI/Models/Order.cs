﻿

namespace OrdersAPI.Models
{
	public class Order
	{
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderTime { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
    }

	public class ListOrders
	{		
		public List<Order> Orders { get; set; }		
	}
}
