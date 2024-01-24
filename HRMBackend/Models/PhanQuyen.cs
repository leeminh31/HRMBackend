#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMBackend.Models
{
    [Table("tbl_PhanQuyen")]
    public partial class PhanQuyen
    {
        [Key]
        [Column("maPhanQuyen")]
        public int MaPhanQuyen { get; set; }
        [Column("loaiPhanQuyen")]
        public string LoaiPhanQuyen { get; set; }
    }
}
