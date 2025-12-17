using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Model
{
    public class OrderModel
    {
        private readonly DataDbContext _context;
        public OrderModel(DataDbContext context)
        {
            _context = context;
        }




        //async => item_list(confirmation)
        //-> user_data(confimation)
        //-> target_address(confirmation)
        //-> estimated_delviery(calculation)
        //-> target_phone(confirmation)
        //-> payment(confirmation)
        //-> 


        //statusz => item_quantity(onhold)
        //-> data_confirmed
        //-> payment_success(reply_mail/status_change_onwebpage)
        //-> order_confirmation(onhold)
        //-> order_confiremd(reply_mail/status_change_onwebpage)
        //-> delivery_information_changed(onprofile)

    }
}
