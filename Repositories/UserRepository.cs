using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Repositories {

    public interface IUserRepository {
        List<UserModel> ListAll();
        UserModel FindById(Guid id);
        void Create(UserModel user);
        void Update(Guid id, UserModel user);
        void Delete(Guid id);
    }

    public class UserRepository : IUserRepository {

        private readonly DataContext _context;

        public UserRepository(DataContext _context) {
            this._context = _context;
        }

        public List<UserModel> ListAll() {
            return this._context.User.ToList();
        }

        public UserModel FindById(Guid id) {
            UserModel? user = this._context.User.Find(id);
            if (user == null) {
                throw new Exception($"User with id={id} not found");
            }

            return user;
        }

        public void Create(UserModel user) {

            if (this._context.User.SingleOrDefault((_user) => user.Username == _user.Username) != null) {
                throw new Exception($"Username already exists");
            }

            user.Id = Guid.NewGuid();
            this._context.User.Add(user);
            this._context.Entry(user).State = EntityState.Added;
            this._context.SaveChanges();

        }

        public void Update(Guid id, UserModel user) {
            UserModel? oldUser = this._context.User.Find(id);
            if (oldUser == null) {
                throw new Exception($"User with id={id} not found");
            }

            oldUser.Username = user.Username;
            oldUser.Password = user.Password;
            this._context.Entry(user).State = EntityState.Modified;
            this._context.SaveChanges();
        }

        public void Delete(Guid id) {
            UserModel? user = this._context.User.Find(id);
            if (user == null) {
                throw new Exception($"User with id={id} not found");
            }

            this._context.User.Remove(user);
            this._context.Entry(user).State = EntityState.Deleted;
            this._context.SaveChanges();
        }

    }
}