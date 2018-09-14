using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBL
{
    public class TaskViewModel
    {
        public TaskViewModel()
        {

        }
        public TaskViewModel(int tid, string tName, string ptName, string projName, string stdt, string endt, int p,string status)
        {
            TaskID = tid;
            TaskName = tName;
            ParentTaskName = ptName;
            ProjectName = projName;
            StartDate = stdt;
            Status = status;
            Priority = p;

        }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string ParentTaskName { get; set; }
        public string ProjectName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
    }
}
