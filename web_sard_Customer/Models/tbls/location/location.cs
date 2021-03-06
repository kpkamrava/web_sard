﻿using System;
using System.ComponentModel.DataAnnotations;

namespace web_sard.Models.tbls.location
{
    public class location
    {

        public Guid Id { get; set; }
        [Display(Name = "عنوان")]
        [Required]
        public string Title { get; set; }
        public Guid? FkP { get; set; }
        public string ptitle { get; set; }
        public int pcode { get; set; }
        public int Kind { get; set; }
        [Display(Name = "کد")]
        [Required]
        public int Code { get; set; }
        [Display(Name = "ظرفیت (کیلو)")]

        public decimal? wight { get; set; }
        [Display(Name = "کد انبار سایر سیستم ها")]
        public int? OtcodeAnbar { get; set; }
        [Display(Name = "غیر فعال")]
        public bool isdell { get; set; }


        public string CodeFull { get; set; }
    }
}
