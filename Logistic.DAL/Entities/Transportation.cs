using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Entities
{
    public class Transportation
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Водитель")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int DriverId { get; set; }

        [DisplayName("Маршрут")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int RouteId { get; set; }

        [DisplayName("Машина")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int CarId { get; set; }

        [DisplayName("Тариф")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int RateId { get; set; }
        
        [DisplayName("Груз")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int CargoId { get; set; }

        [DisplayName("Количество груза")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 1000, ErrorMessage = "от 1 до 1000")]
        public int CargoCount { get; set; }

        [DisplayName("Дата")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime Date { get; set; } = DateTime.Now;


        public Driver Driver { get; set; }

        public Route Route { get; set; }

        public Car Car { get; set; }

        public Cargo Cargo { get; set; }

        public Rate Rate { get; set; }
    }
}
