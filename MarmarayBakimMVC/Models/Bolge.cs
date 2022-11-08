﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MarmarayBakimMVC.Models
{
    public partial class Bolge
    {
        public Bolge()
        {
            Istasyon = new HashSet<Istasyon>();
            Sistem = new HashSet<Sistem>();
        }

        [Key]
        public int BolgeId { get; set; }
        [Required]
        [StringLength(10)]
        public string BolgeAd { get; set; }

        [InverseProperty("Bolge")]
        public virtual ICollection<Istasyon> Istasyon { get; set; }
        [InverseProperty("Bolge")]
        public virtual ICollection<Sistem> Sistem { get; set; }
    }
}