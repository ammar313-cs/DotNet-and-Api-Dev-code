using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DrapperASPNETCORE.Repositories;
using DrapperASPNETCORE.Contracts;
using DrapperASPNETCORE.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrapperASPNETCORE.Controllers
{
    [Route("api/Jokes")]
    public class JokesController : ControllerBase
    {
        private readonly IJokeRepository _jokeRepo;

        public JokesController(IJokeRepository jokeRepo) => _jokeRepo = jokeRepo;

        

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetJokes()
        {
            try
            {
                var joke = await _jokeRepo.GetAllJokes();
                return Ok(joke);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "JokeById")]
        public async Task<IActionResult> GetJokeById(int id)
        {
            try
            {
                var joke = await _jokeRepo.GetJokeById(id);

                if (joke == null)
                    return NotFound();

                return Ok(joke);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public  async Task<IActionResult> CreateJoke([FromBody]NewJoke joke)
        {
            try
            {
                var createdJoke = await _jokeRepo.AddJoke(joke);
                return CreatedAtRoute("JokeByID", new { id = createdJoke.Id }, createdJoke); //error

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJoke(int id, JokeUpdate joke)
        {
            try
            {
                var dbJoke = await _jokeRepo.GetJokeById(id);

                if(dbJoke == null) {

                    return NotFound();
                }

                await _jokeRepo.UpdateJoke(id, joke);

                return NoContent();



            }

            catch(Exception ex) {

                return StatusCode(500, ex.Message);
            }
        }

       
    }
}

