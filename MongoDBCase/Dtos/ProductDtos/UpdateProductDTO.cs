namespace MongoDBCase.Dtos.ProductDtos
{
    public class UpdateProductDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        public string CategoryId { get; set; }

        public List<string> ProductImages { get; set; }
    }
}
