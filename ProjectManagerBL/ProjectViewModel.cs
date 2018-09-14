using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBL
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {

        }
        public ProjectViewModel(int pid, string pName, string stdt, string endt, int p, string status)
        {
            ProjectID = pid;
            ProjectName = pName;
            StartDate = stdt;
            EndDate = endt;
            Priority = p;
            Status = status;
        }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
    }
}

