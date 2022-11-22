using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Entities
{
    public class Rate
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }

        [DisplayName("Цена за м3")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 1000)]
        public int VolumeRate { get; set; }

        [DisplayName("Цена за т")]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 1000)]
        public int CarryingRate { get; set; }


        public List<Transportation> Transportations { get; set; }
    }
}
