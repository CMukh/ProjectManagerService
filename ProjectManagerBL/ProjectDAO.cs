using ProjectManagerDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBL
{
   public class ProjectDAO
    {

        public List<ProjectViewModel> GetAll()
        {

            ProjectTasksDBEntities projectDBEntities = new ProjectTasksDBEntities();
            //taskDBEntities.TaskDetails.Include(x => x.TaskDetail1);
            List<Project> project = projectDBEntities.Projects.ToList();
            List<ProjectViewModel> projectViewModelList = new List<ProjectViewModel>();
            foreach (var p in project)
            {
                //var task = p.TaskDetails.SingleOrDefault(t => t.TaskName );
                projectViewModelList.Add(new ProjectViewModel
                    (p.ProjectID, p.ProjectName, p.StartDate.ToShortDateString(), p.EndDate.ToShortDateString(), p.Priority, p.Status));
            }
            return projectViewModelList;
        }
        public ProjectViewModel GetProject(int id)
        {
            ProjectTasksDBEntities taskDBEntities = new ProjectTasksDBEntities();
            var project = taskDBEntities.Projects.SingleOrDefault(p => p.ProjectID == id);
            return new ProjectViewModel
            (project.ProjectID, project.ProjectName, project.StartDate.ToString(), project.EndDate.ToString(), project.Priority, project.Status);

        }
        public void DeleteProject(int id)
        {
            ProjectTasksDBEntities projectDBEntities = new ProjectTasksDBEntities();
            Project project = projectDBEntities.Projects.SingleOrDefault(p => p.ProjectID == id);
            var entry = projectDBEntities.Entry(project);
            if (entry.State == System.Data.Entity.EntityState.Detached)
                projectDBEntities.Projects.Attach(project);
            projectDBEntities.Projects.Remove(project);
            projectDBEntities.SaveChanges();
        }
        public void AddProject(ProjectViewModel projectVM)
        {
            ProjectTasksDBEntities projectDBEntities = new ProjectTasksDBEntities();
            Project project = new Project();
            project.ProjectName = projectVM.ProjectName;
            project.StartDate = Convert.ToDateTime(projectVM.StartDate);
            project.EndDate = Convert.ToDateTime(projectVM.EndDate);
            project.Priority = projectVM.Priority;
            project.Status = (projectVM.Status == null) ? "" : projectVM.Status;

            projectDBEntities.Projects.Add(project);
            projectDBEntities.SaveChanges();

        }
        public void EditProject(ProjectViewModel projectVM)
        {
            ProjectTasksDBEntities projectDBEntities = new ProjectTasksDBEntities();
            Project project = projectDBEntities.Projects.SingleOrDefault(p => p.ProjectID == projectVM.ProjectID);

            project.ProjectName = projectVM.ProjectName;
            project.StartDate = Convert.ToDateTime(projectVM.StartDate);
            project.EndDate = Convert.ToDateTime(projectVM.EndDate);
            project.Priority = projectVM.Priority;
            project.Status = (projectVM.Status == null) ? "" : projectVM.Status;

            projectDBEntities.Projects.Attach(project);
            projectDBEntities.Entry(project).State = System.Data.Entity.EntityState.Modified;
            projectDBEntities.SaveChanges();

        }
    }
}
