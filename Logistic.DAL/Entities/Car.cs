using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Entities
{
    public class Car
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Марка")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Mark { get; set; }

        [DisplayName("Организация")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int OrganizationId { get; set; }

        [DisplayName("Грузоподъемность (т)")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 50, ErrorMessage = "От 1 до 50")]
        public int CarryingWeight { get; set; }

        [DisplayName("Объем кузова (м3)")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 50, ErrorMessage = "От 1 до 50")]
        public int CarryingVolume { get; set; }


        public List<Transportation> Transportations { get; set; }

        public Organization Organization { get; set; }
    }
}
