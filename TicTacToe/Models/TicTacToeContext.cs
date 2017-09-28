using System.Linq;
using System.Data.Entity;

namespace TicTacToe.Models
{
    public class TicTacToeContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Fields> Fields { get; set; }
        public DbSet<Level> Levels { get; set; }

        public TicTacToeContext()
        {
            // Указывает EF, что если модель изменилась, нужно воссоздать базу данных с новой структурой
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TicTacToeContext>());

            Initialize();
        }

        public void Initialize()
        {
            if (!Levels.Any())
            {
                Levels.Add(new Level { Id = 1, LevelName = "Easy" });
                Levels.Add(new Level { Id = 2, LevelName = "Hard" });
                SaveChanges();
            }
        }
    }
}