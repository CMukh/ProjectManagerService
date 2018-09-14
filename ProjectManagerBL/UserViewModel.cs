using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBL
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(int uid, string fName, string lName, string projName, int eid, string tName)
        {
            UserID = uid;
            FirstName = fName;
            LastName = lName;
            ProjectName = projName;
            EmployeeID = eid;
            TaskName = tName;

        }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProjectName { get; set; }
        public int EmployeeID { get; set; }
        public string TaskName { get; set; }
    }
}
