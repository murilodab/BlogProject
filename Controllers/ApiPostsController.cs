﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data;
using BlogProject.Models;
using Microsoft.AspNetCore.Cors;
using BlogProject.Enums;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            return await _context.Posts.ToListAsync();
        }

        // GET: api/ApiPosts
        [EnableCors("PortfolioPolicy")]
        [HttpGet("/recentposts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetRecentPosts()
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            return await _context.Posts.OrderBy(p => p.Created).Take(3).Where(p => p.ReadyStatus.Value.Equals(nameof(ReadyStatus.ProductionReady))).ToListAsync();
        }


        // GET: api/ApiPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        //// PUT: api/ApiPosts/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPost(int id, Post post)
        //{
        //    if (id != post.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(post).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PostExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/ApiPosts
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Post>> PostPost(Post post)
        //{
        //  if (_context.Posts == null)
        //  {
        //      return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        //  }
        //    _context.Posts.Add(post);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPost", new { id = post.Id }, post);
        //}

        //// DELETE: api/ApiPosts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePost(int id)
        //{
        //    if (_context.Posts == null)
        //    {
        //        return NotFound();
        //    }
        //    var post = await _context.Posts.FindAsync(id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Posts.Remove(post);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
