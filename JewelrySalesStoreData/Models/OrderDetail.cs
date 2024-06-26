﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace JewelrySalesStoreData.Models
{
    public partial class OrderDetail
    {
        public Guid OrderDetailId { get; set; }

        public OrderDetail()
        {
            OrderDetailId = Guid.NewGuid();
        }

        public Guid? OrderId { get; set; }

        public Guid? ProductId { get; set; }

        public int? Quantity { get; set; }

        public double? UnitPrice { get; set; }

        public double? TotalPrice { get; set; }

        public double? DiscountPrice { get; set; }

        public double? FinalPrice { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string Notes { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
