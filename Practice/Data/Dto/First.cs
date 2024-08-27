namespace Practice.Data.Dto
{
    public class First
    {
        public Second Second { get; set; }

        public First(Second second)
        {
            Second = second;
        }
    }
}
