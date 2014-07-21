﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_HW1.Models
{
    public class BankBatchUpdateVM
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 銀行名稱 { get; set; }

        [Required]
        public int 銀行代碼 { get; set; }

        public Nullable<int> 分行代碼 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 帳戶名稱 { get; set; }

        [StringLength(20, ErrorMessage = "欄位長度不得大於 20 個字元")]
        [Required]
        public string 帳戶號碼 { get; set; }
    }
}