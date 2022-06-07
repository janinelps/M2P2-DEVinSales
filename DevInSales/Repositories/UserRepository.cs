using DevInSales.Models;

namespace DevInSales.Repositories
{
    public class UserRepository
    {
        public static User CheckNameAndPassword(string username, string password)
        {
            var users = new List<User>();

            users.Add(new User() { Id = 1, Name = "João", Password = "123", Role = "funcionario"});
            users.Add(new User() { Id = 2, Name = "Maria", Password = "123", Role = "gerente" });

            return users.Where(x => x.Name.ToLower() == username.ToLower() && x.Password == password)
            .FirstOrDefault();
        }
    }
}
