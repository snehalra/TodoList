using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Models
{
    public class Todo
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Priority { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    //public class TodosDBContext : DbContext
    //{
    //    public DbSet<Todo> Todo { get; set; }
    //}
    
}