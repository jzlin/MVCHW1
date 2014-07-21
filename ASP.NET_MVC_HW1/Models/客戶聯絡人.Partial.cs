namespace ASP.NET_MVC_HW1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public interface I客戶聯絡人更新
    {
        [Required]
        int Id { get; set; }
        [Required]
        int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        string 電話 { get; set; }
    }
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : I客戶聯絡人更新//, IValidatableObject
    {
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();
        //    var contacts = clientRepo.get客戶聯絡人ById(this.客戶Id);
        //    bool isEmailExisted = false;
        //    foreach (客戶聯絡人 c in contacts)
        //    {
        //        if (c.Email == this.Email && c.Id != this.Id)
        //        {
        //            isEmailExisted = true;
        //            break;
        //        }
        //    }
        //    if (isEmailExisted) 
        //    {
        //        yield return new ValidationResult("同一個客戶下的聯絡人，其 Email 不能重複。", new string[] { "Email" });
        //    }
        //    yield return ValidationResult.Success;
        //}
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
