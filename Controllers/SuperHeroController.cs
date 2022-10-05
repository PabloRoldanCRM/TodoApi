using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public SuperHeroController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<SuperHero[]>> Get()
        {
            var heroes = await _dbContext.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero[]>> GetById(int id)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(id);
            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SuperHero newSuperHero)
        {
            var hero = await _dbContext.AddAsync(newSuperHero);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                Id = newSuperHero.Id
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put(SuperHero superHero)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(superHero.Id);
            if (hero == null)
                return NotFound();

            hero.Name = superHero.Name;
            hero.LastName = superHero.LastName;
            hero.FirstName = superHero.FirstName;
            hero.Place = superHero.Place;

            await _dbContext.SaveChangesAsync();
            return Ok(hero);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return NotFound();
            _dbContext.SuperHeroes.Remove(hero);
            await _dbContext.SaveChangesAsync();
            return Ok(hero);
        }
    }
}