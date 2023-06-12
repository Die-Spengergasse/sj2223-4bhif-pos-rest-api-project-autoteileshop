using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.Domain.DTO
{
    public class ShoppingCartItemDTOGet
    {
        public int Id { get; set; }
        public Guid guid { get; set; }
        public int Pieces { get; set; }
        public int? ProductId { get; set; }
        public int? ShoppingCartId { get; set; }

        public ShoppingCartItemDTOGet(ShoppingCartItem cart)
        {
            Id = cart.Id;
            this.guid = cart.guid;
            Pieces = cart.Pieces;
            ProductId = cart.ProductId;
            ShoppingCartId = cart.ShoppingCartId;
        }

        public override string ToString()
        {
            string json = $"{{\"Id\": {Id}, \"guid\": \"{guid}\", \"Pieces\": {Pieces}, \"ProductId\": {ProductId}, \"ShoppingCartId\": {ShoppingCartId}, }}";
            return json;
        }
    }


}
