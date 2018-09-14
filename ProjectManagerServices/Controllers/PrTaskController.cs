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
    public class PrTaskController : ApiController
    {
        TaskDAO taskDao;
        public PrTaskController()
        {
            taskDao = new TaskDAO();
        }
        public PrTaskController(TaskDAO t)
        {
            taskDao = t;
        }

        public IHttpActionResult Get()
        {
            List<TaskViewModel> list = taskDao.GetAll();
            if (list.Count == 0)
                return NotFound();
            return Ok(list);
        }

        // GET: api/Task/5
        public TaskViewModel Get(int id)
        {
            return taskDao.GetTask(id);
        }

        // POST: api/Task

        public IHttpActionResult Post(TaskViewModel task)
        {

            taskDao.AddTask(task);
            return Ok();

        }

        // PUT: api/Task/5
        public IHttpActionResult Put(TaskViewModel task)
        {
            taskDao.EditTask(task);
            return Ok();
        }

        // DELETE: api/Task/5
        public IHttpActionResult Delete(int id)
        {
            taskDao.DeleteTask(id);
            return Ok();
        }

    }
}
