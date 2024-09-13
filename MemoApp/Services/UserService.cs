using MemoApp.Models.Object;

namespace MemoApp.Services
{
    public class UserService
    {
        private Employee? ActiveEmployee { get; set; }

        public UserService() { ActiveEmployee = null; }
        public UserService(Employee activeEmployee) { }
        public Employee GetActiveEmployee() { return ActiveEmployee; }
        public int GetActiveEmployeeId() { return ActiveEmployee.EmployeeId; }
        public void SetActiveEmployee(Employee activeEmployee) { ActiveEmployee = activeEmployee; }
        public void UnSetActiveEmployee() { ActiveEmployee = null; }
    }
}
