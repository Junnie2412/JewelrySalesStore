﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JewelrySalesStoreData.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public Category()
    {
        this.CategoryId = Guid.NewGuid();
    }


    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}