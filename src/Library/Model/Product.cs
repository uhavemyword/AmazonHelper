namespace Library.Model
{
    public class Product
    {
        public Product()
        {
            Dimensions = new DimensionCollection();
        }

        public string item_sku { get; set; }
        public string item_name { get; set; }
        public string external_product_id { get; set; }
        public ExternalProductIdType external_product_id_type { get; set; }
        public string brand_name { get; set; }
        public string department_name { get; set; }
        public string price { get; set; }
        public string product_type_name { get; set; }
        public string ThumbImageUrl { get; set; }
        public string OfferListingUrl { get; set; }
        public string Url { get; set; }
        public DimensionCollection Dimensions { get; set; }
        public float Size { get; set; }

        public override string ToString()
        {
            return external_product_id;
        }
    }
}