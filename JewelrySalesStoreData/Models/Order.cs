﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelrySalesStoreData.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? CompanyId { get; set; }

    [Required(ErrorMessage = "Date is required")]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "Total price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than zero")]
    public double? TotalPrice { get; set; }

    public string PaymentMethod { get; set; }

    public string ShippingMethod { get; set; }

    [Required]
    [Display(Name = "Customer Address")]
    public string CustomerAddress { get; set; }

    [Required]
    [Display(Name = "Customer Bank Account")]
    public string CustomerBankAccount { get; set; }

    public bool Status { get; set; }

    public string Notes { get; set; }

    public virtual Company Company { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}