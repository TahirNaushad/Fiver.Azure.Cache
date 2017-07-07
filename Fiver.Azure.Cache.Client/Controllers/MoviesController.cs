using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fiver.Azure.Cache.Client.Models.Movies;

namespace Fiver.Azure.Cache.Client.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IAzureCacheStorage cacheStorage;

        public MoviesController(IAzureCacheStorage cacheStorage)
        {
            this.cacheStorage = cacheStorage;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var keys = this.cacheStorage.ListKeys();

            return Ok(keys);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public async Task<IActionResult> Get(string id)
        {
            var model = await this.cacheStorage.GetObjectAsync<Movie>(id);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Movie model)
        {
            await this.cacheStorage.SetObjectAsync(model.Id.ToString(), model);

            return CreatedAtRoute("GetMovie", new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.cacheStorage.DeleteAsync(id);

            return NoContent();
        }
    }
}
