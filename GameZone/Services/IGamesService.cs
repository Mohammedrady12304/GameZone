namespace GameZone.Services
{
    public interface IGamesService
    {
        public IEnumerable<Game>GetAll();
        Task Create(CreateGameFormViewModel model);
        Task<Game> GetById(int id);

        bool Delete (int id);
    }
}
