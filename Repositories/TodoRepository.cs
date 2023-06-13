using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Repositories {
    public interface ITodoRepository {
        List<TodoModel> ListAll(Guid userId);
        TodoModel FindById(Guid id);
        void Create(TodoModel todo);
        void Update(Guid id, TodoModel todo);
        void DeleteById(Guid id);
    }

    public class TodoRepository : ITodoRepository {

        private readonly DataContext _context;

        public TodoRepository(DataContext _context) {
            this._context = _context;
        }

        public List<TodoModel> ListAll(Guid userId) {
            return this._context.Todo.Where(todo => todo.UserId == userId).ToList();
        }

        public TodoModel FindById(Guid id) {
            TodoModel? todo = this._context.Todo.Find(id);

            if (todo == null) {
                throw new Exception($"Todo with id={id} not found");
            }

            return todo;
        }

        public void Create(TodoModel todo) {
            todo.Id = Guid.NewGuid();
            this._context.Todo.Add(todo);
            this._context.SaveChanges();
        }

        public void Update(Guid id, TodoModel todo) {
            TodoModel? oldTodo = this._context.Todo.Find(id);
            if (todo == null) {
                throw new Exception($"User with id={id} not found");
            }
            
            oldTodo!.Title = todo.Title;
            oldTodo.Done = todo.Done;
            this._context.Entry(oldTodo).State = EntityState.Modified;
            this._context.SaveChanges();
        }
        public void DeleteById(Guid id) {
            TodoModel? todo = this._context.Todo.Find(id);
            this._context.Entry<TodoModel>(todo!).State = EntityState.Deleted;
            this._context.SaveChanges();
        }
    }
}