namespace Practice.Data.Model
{
    public class Response
    {
        public string Stasus { get; protected set; }
        public string Message { get; protected set; }

        public static Response Create(string status, string message)
        {
            return new Response
            {
                Stasus = status,
                Message = message
            };
        }
    }
}
