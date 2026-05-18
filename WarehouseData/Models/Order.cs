using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseData.Models
{
    public enum OrderType
    {
        [Description("Входящая")]
        Incoming = 1,

        [Description("Исходящая")]
        Outgoing = 2
    }

    public enum OrderStatus
    {
        [Description("Не утверждена")]
        NotApproved = 1,

        [Description("Утверждена")]
        Approved = 2
    }

    [Table("orders")]
    public class Order
    {
        public static BigInteger OrderCounter = BigInteger.Zero;

        [Key]
        [Column("id")]
        public BigInteger OrderId { get; set; }

        [Column("number")]
        public string Number { get; set; } = string.Empty;

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("type")]
        public OrderType Type { get; set; }

        [Column("status")]
        public OrderStatus Status { get; set; }

        [Column("warehouse_id")]
        public BigInteger WarehouseId { get; set; }

        public ObservableCollection<Product> Products { get; set; }
            = new ObservableCollection<Product>();

        public Order()
        {
            OrderId = ++OrderCounter;
            Date = DateTime.Now;
            Status = OrderStatus.NotApproved;
        }
    }
}
