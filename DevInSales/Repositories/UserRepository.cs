using DevInSales.Enums;
using DevInSales.Models;

namespace DevInSales.Repositories
{
    public static class UserRepository
    {
        public static User? CheckNameAndPassword(string username, string password)
        {
            var users = new List<User>();

            users.Add(new User() { Id = 1, Name = "João", Password = "123", Role = "funcionario"});
            users.Add(new User() { Id = 2, Name = "Maria", Password = "123", Role = "gerente" });
            users.Add(new User() { Id = 2, Name = "Tiao", Password = "123", Role = "administrador"});

            return users.Where(x => x.Name.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }

        public static List<User> Obter()
        {
            var users = new List<User>();

            users.Add(new User() { Id = 1, Name = "Cintia", Password = "987", Role = "funcionario", Permissao = Permissoes.Administrador });


            return users;
        }
    }
}
