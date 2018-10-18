using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInzynier.Models
{
    public class CartModel
    {
        //model koszyka 
        protected List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(ProductModel product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.ID == product.ID).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        //wirtualne metody do przesłonięcia w kontrolerze
        public virtual void RemoveLine(ProductModel product) =>
            lineCollection.RemoveAll(l => l.Product.ID == product.ID);

        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    //klasa kart line w celu dodania obiektów tej klasy do listy
    public class CartLine
    {
        public int CartLineID { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
