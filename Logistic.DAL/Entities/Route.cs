using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.DAL.Attributes.Validation;

namespace Logistic.DAL.Entities
{
    public class Route
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }

        [DisplayName("Расстояние (км)")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 10000, ErrorMessage = "От 1 до 10000")]
        public int RouteLength { get; set; }

        [DisplayName("Откуда")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int? StartPointId { get; set; }

        [DisplayName("Куда")]
        [Required(ErrorMessage = "Обязательное поле")]
        [NotEqual(nameof(StartPointId), "Куда", "Откуда")]
        public int? EndPointId { get; set; }


        public List<Transportation> Transportations { get; set; }

        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }
    }
}
