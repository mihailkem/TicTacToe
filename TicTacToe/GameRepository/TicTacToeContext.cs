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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Game>().HasKey(g => g.Id);
            modelBuilder.Entity<Game>().Property(g => g.PlayerName).IsRequired();
            modelBuilder.Entity<Game>().Property(g => g.PlayerName).HasMaxLength(30);
            modelBuilder.Entity<Game>().Property(g => g.PlayerTeamId).IsRequired();            
            modelBuilder.Entity<Game>().HasRequired(g => g.Level);

            modelBuilder.Entity<Fields>().ToTable("Fields");
            modelBuilder.Entity<Fields>().HasKey(f => f.Id);
            modelBuilder.Entity<Fields>().HasRequired(f => f.Game);

            modelBuilder.Entity<Level>().ToTable("Levels");
            modelBuilder.Entity<Level>().HasKey(l => l.Id);
            modelBuilder.Entity<Level>().Property(l => l.LevelName).IsRequired();  

            base.OnModelCreating(modelBuilder);
        }
    }
}