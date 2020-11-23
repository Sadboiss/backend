namespace WebApi.Entities
{
    public class Size : Entity
    {
        public enum SizeType
        {
            Small, 
            Medium,
            Large,
        }

        public SizeType name { get; set; }
    }
}