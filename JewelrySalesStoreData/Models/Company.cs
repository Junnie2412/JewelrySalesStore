﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JewelrySalesStoreData.Models;

public partial class Company
{
    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; }

    public string CompanyAddress { get; set; }

    public string CompanyDescription { get; set; }

    public string CompanyPhone { get; set; }

    public string Website { get; set; }

    public DateTime? FoundationDate { get; set; }

    public string Email { get; set; }

    public bool? IsActive { get; set; }

    public string Notes { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}