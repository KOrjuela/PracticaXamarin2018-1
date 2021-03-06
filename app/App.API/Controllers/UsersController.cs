﻿namespace App.API.Controllers
{
    using App.API.Helpers;
    using Lands.Domain;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/usuario@gmail.com
        [HttpPost]
        [Route("GetUserByEmail")]
        public async Task<IHttpActionResult> GetUserByEmail(JObject obj)
        {

            var email = string.Empty;
            dynamic jsonObject = obj;

            try
            {
                email = jsonObject.Email.Value;
            }
            catch (Exception)
            {
                return BadRequest("Missing parameter.");
            }

            User user = await db.Users
                .Where(u => u.Email.ToLower() == email.ToLower())
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }




            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User objectUser)
        {
            if (objectUser.ImageArray != null && objectUser.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(objectUser.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0},jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    objectUser.ImagePath = fullPath;
                }
            }

            db.Users.Add(objectUser);
            await db.SaveChangesAsync();
            UsersHelper.CreateUserASP(objectUser.Email, "User", objectUser.Password);

            return CreatedAtRoute("DefaultApi", new { id = objectUser.UserId }, objectUser);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}