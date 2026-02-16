namespace WebShop13kVizsga.Persistence
{
    public static class DbSeeder
    {
        public static void Seed(DataDbContext db)
        {
            if (db.Categories.Any()) return;
        }

    }
}
