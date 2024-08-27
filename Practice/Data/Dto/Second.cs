namespace Practice.Data.Dto
{
    public class Second
    {
        public First First { get; set; }

        public Second(First first)
        {
            First = first;
        }
    }
}
