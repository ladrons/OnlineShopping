using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Tools.CartTools
{
    public class Cart
    {
        Dictionary<int, CartItem> _cart;
        public Cart()
        {
            _cart = new Dictionary<int, CartItem>();
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public decimal TotalPrice
        {
            get
            {
                return _cart.Sum(x => x.Value.SubTotal);
            }
        }
        public List<CartItem> ShoppingCart
        {
            get
            {
                return _cart.Values.ToList();
            }
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\

        public void AddToCart(CartItem item)
        {
            if (_cart.ContainsKey(item.ID))
            {
                _cart[item.ID].Amount++;
                return;
            }
            _cart.Add(item.ID, item);
        }
        public void RemoveToCart(int id)
        {
            if (_cart[id].Amount > 1) 
            { 
                _cart[id].Amount--;
                return;
            }
            _cart.Remove(id);
        }

        //--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\//--\\
    }
}