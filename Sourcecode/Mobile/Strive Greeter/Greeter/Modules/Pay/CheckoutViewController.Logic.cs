using System.Collections.Generic;
using Greeter.DTOs;

namespace Greeter.Modules.Pay
{
    public partial class CheckoutViewController
    {
        List<Checkout> Checkouts;

        public CheckoutViewController()
        {
            //TODO Replace this into orginal data
            Checkouts = new List<Checkout>
            {
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout(),
                new Checkout()
            };
        }
    }
}
