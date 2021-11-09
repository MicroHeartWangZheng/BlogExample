using Nest;

namespace Es.Models
{
    public class Product
    {
        [Number(NumberType.Integer)]
        public int Id { get; set; }

        [Keyword]
        public string PartNo { get; set; }

        [Keyword]
        public string BrandName { get; set; }

        [Text(Analyzer = "whitespace")]
        public string BrandAlias { get; set; }
    }
}
