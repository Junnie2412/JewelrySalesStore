﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JewelrySalesStoreData.Models;

public partial class Promotion
{
    public Promotion()
    {
        PromotionId = Guid.NewGuid();
    }
    public Guid PromotionId { get; set; }
    [Required]
    public string PromotionName { get; set; }
    [Required]
    public string PromotionCode { get; set; }
    [Required]
    [Display(Name = "Discount Percentage")]
    [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100")]
    public double? DiscountPercentage { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
    public DateTime? StartDate { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false),]
    [DateRange("StartDate", ErrorMessage = "EndDate must be greater than StartDate.")]
    public DateTime? EndDate { get; set; }
    [Required]
    [Display(Name = "Condition")]
    public string Condition { get; set; }
    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; }


    [Display(Name = "Is Active")]
    public bool? IsActive { get; set; }

    [Display(Name = "Notes")]
    public string Notes { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}

