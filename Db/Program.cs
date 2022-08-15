namespace Db
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ChessContext db = new ChessContext())
            {
                var user = db.Users.Where(u => u.Name.Equals("Rtytrtrtr2") && u.Password.Equals("12345678")).FirstOrDefault();
                Console.WriteLine($"{user.Id}.{user.Name} - {user.Email} - {user.Password}");
                var users = db.Users.ToList();
                Console.WriteLine("Список обєктів:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Email} - {u.Password}");
                }

                Console.ReadLine();
            }
        }
    }
}