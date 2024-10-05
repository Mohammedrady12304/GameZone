namespace GameZone.Models
{
    public class Category :InitialEntity
    {
        public ICollection<Game> Games { get; set; }=new List<Game>();
    }
}
