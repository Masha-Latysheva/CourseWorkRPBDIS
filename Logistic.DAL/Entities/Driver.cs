using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.DAL.Entities
{
    public class Driver
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string LastName { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string FirstName { get; set; }

        [DisplayName("Номер паспорта")]
        [Required(ErrorMessage = "Обязательное поле")]
        [RegularExpression(@"HB\d\d\d\d\d\d\d", ErrorMessage = "Формат: HB*******")]
        public string Passport { get; set; }


        public List<Transportation> Transportations { get; set; }
    }
}
