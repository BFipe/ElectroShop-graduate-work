using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TechnoShop.Enums
{
    public enum OrderStatusEnum
    {
        [Display(Name = "В процессе обработки")]
        Processing_State,

        [Display(Name = "Заказ подтвержден")]
        Confirmed,

        [Display(Name = "Заказ готов к выдаче")]
        Ready_to_go,

        [Display(Name = "Успешно завершен")]
        Finished_Sucessfully,

        [Display(Name = "Отменен пользователем")]
        Canceled_By_User,

        [Display(Name = "Отменен администратором")]
        Canceled_By_Manager,
    }
}
