using ProjectManagerBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManagerServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectController : ApiController
    {
        ProjectDAO projectDao;
        public ProjectController()
        {
            projectDao = new ProjectDAO();
        }
        public ProjectController(ProjectDAO p)
        {
            projectDao = p;
        }

        public IHttpActionResult Get()
        {
            List<ProjectViewModel> list = projectDao.GetAll();
            if (list.Count == 0)
                return NotFound();
            return Ok(list);
        }

        // GET: api/Task/5
        // public TaskViewModel Get(int id)
        public ProjectViewModel Get(int id)
        {
            return projectDao.GetProject(id);
        }

        // POST: api/Task

        public IHttpActionResult Post(ProjectViewModel project)
        {

            projectDao.AddProject(project);
            return Ok();

        }

        // PUT: api/Task/5
        public IHttpActionResult Put(ProjectViewModel project)
        {
            projectDao.EditProject(project);
            return Ok();
        }

        // DELETE: api/Task/5
        public IHttpActionResult Delete(int p)
        {
            projectDao.DeleteProject(p);
            return Ok();
        }
    }
}
