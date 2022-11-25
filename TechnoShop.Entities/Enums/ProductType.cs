using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TechnoShop.Entities.Enums
{
    public enum ProductType
    {
        [Display(Name = "Телевизор")]
        Tv = 1,
        [Display(Name = "Компьютер")]
        Computer,
        [Display(Name = "Смартфон")]
        Smartphone,
        [Display(Name = "Ноутбук")]
        Notebook,
        [Display(Name = "Телевизионная периферия")]
        Tv_Perifery,
        [Display(Name = "Компьютерная периферия")]
        Computer_Perifery,
        [Display(Name = "Аксессуары для смартфонов")]
        Smartphone_Perifery,
        [Display(Name = "Аксессуары для ноутбуков")]
        Notebook_Perifery,
        [Display(Name = "Холодильник")]
        Refrigerator,
    }
}
