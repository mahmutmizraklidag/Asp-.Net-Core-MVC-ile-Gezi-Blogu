﻿using System.ComponentModel.DataAnnotations;

namespace GeziBlogu.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), StringLength(50), Required(ErrorMessage = "Bu Alan Gereklidir!")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public virtual List<Post> Posts { get; set; }
        public Category()
        {
            Posts = new List<Post>();
        }
    }
}
