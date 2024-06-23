﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelrySalesStoreData.Models;

public partial class Product
{
    public Product()
    {
        ProductId = Guid.NewGuid();
    }
    public Guid ProductId { get; set; }

    public string Color { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than zero")]
    public double? Weight { get; set; }

    public byte[] Image { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public double? Price { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? PromotionId { get; set; }

    public bool? IsActive { get; set; }

    public string Notes { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Promotion Promotion { get; set; }
}