namespace IncrementingIntegers.Logic.Queries
{
    public class CurrentIdQuery
    {
        public string UserId { get; set; }

        public CurrentIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
