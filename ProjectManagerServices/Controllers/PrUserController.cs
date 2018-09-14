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
    public class PrUserController : ApiController
    {
        UserDAO userDao;
        public PrUserController()
        {
            userDao = new UserDAO();
        }
        public PrUserController(UserDAO t)
        {
            userDao = t;
        }

        public IHttpActionResult Get()
        {
            List<UserViewModel> list = userDao.GetAll();
            if (list.Count == 0)
                return NotFound();
            return Ok(list);
        }

        // GET: api/User/5

        public UserViewModel Get(int id)
        {
            return userDao.GetUser(id);
        }

        // POST: api/User

        public IHttpActionResult Post(UserViewModel user)
        {

            userDao.AddUser(user);
            return Ok();

        }

        // PUT: api/User/5
        public IHttpActionResult Put(UserViewModel user)
        {
            userDao.EditUser(user);
            return Ok();
        }

        // DELETE: api/User/5
        public IHttpActionResult Delete(int id)
        {
            userDao.DeleteUser(id);
            return Ok();
        }
    }
}
