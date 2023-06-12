using Spg.AutoTeileShop.Domain.DTO;

namespace Spg.AutoTeileShop.Domain.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Guid guid { get; set; }
        public int Pieces { get; set; }
        public int? ProductId { get; set; }
        public virtual Product? ProductNav { get; set; }
        public int? ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCartNav { get; set; }

        public ShoppingCartItem()
        {
        }
        //public override string ToString()
        //{
        //    return $"Id: {Id}, Guid: {guid}, Pieces: {Pieces}, ProductId: {ProductId}, ShoppingCartId: {ShoppingCartId}";
        //}

        public override string ToString()
        {
            string json = $"{{\"Id\": {Id}, \"guid\": \"{guid}\", \"Pieces\": {Pieces}, \"ProductId\": {ProductId}, \"ShoppingCartId\": {ShoppingCartId}, }}";
            return json;
        }

        public ShoppingCartItem(int id, Guid guid, int pieces, int? productId, Product productNav, int? shoppingCartId, ShoppingCart? shoppingCartNav)
        {
            Id = id;
            this.guid = guid;
            Pieces = pieces;
            ProductId = productId;
            ProductNav = productNav;
            ShoppingCartId = shoppingCartId;
            ShoppingCartNav = shoppingCartNav;
        }

        public ShoppingCartItem(Guid guid, int pieces, int? productId, Product productNav, int? shoppingCartId, ShoppingCart? shoppingCartNav)
        {
            this.guid = guid;
            Pieces = pieces;
            ProductId = productId;
            ProductNav = productNav;
            ShoppingCartId = shoppingCartId;
            ShoppingCartNav = shoppingCartNav;
        }
        public ShoppingCartItem(ShoppingCartItemPostDTO dto)
        {
            guid = Guid.NewGuid();
            Pieces = dto.Pieces;
            ProductNav = dto.ProductNav;
            ShoppingCartNav = dto.ShoppingCartNav;
        }
    }
}
