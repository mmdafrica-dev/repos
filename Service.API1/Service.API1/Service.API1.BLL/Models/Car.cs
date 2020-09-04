using System;

namespace Service.API1.BLL.Models
    {
    public class Car
        {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public CarType CarType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        }
    }
