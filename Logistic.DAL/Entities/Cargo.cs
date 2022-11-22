using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Entities
{
    public class Cargo
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }

        [DisplayName("Масса (кг)")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 500, ErrorMessage = "От 1 до 500")]
        public int Weight { get; set; }

        [DisplayName("Объем кузова (м3)")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 50, ErrorMessage = "От 1 до 50")]
        public int Volume { get; set; }


        public List<Transportation> Transportations { get; set; }
    }
}
