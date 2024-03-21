using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PlantsDetection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly PlantsDetectionContext _context;

        public CommentController(PlantsDetectionContext context)
        {
            _context = context;
        }

        // GET api/comment/post-comments/id
        [HttpGet("post-comments/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetPostComments(int id)
        {
            var comments = await _context.Comments.Where(c => c.PostId == id).ToListAsync();

            if (comments == null || !comments.Any())
            {
                return NotFound();
            }

            return comments;
        }

        // POST api/comment/like/id
        [HttpPost("like/{id}")]
        public async Task<IActionResult> LikeComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            // Increment the like count or perform relevant logic
            comment.NumOfLikes++;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST api/comment/dislike/id
        [HttpPost("dislike/{id}")]
        public async Task<IActionResult> DislikeComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            // Increment the dislike count or perform relevant logic
            comment.NumOfDisLikes++;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET api/comment/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // POST api/comment/on-post/id
        [HttpPost("on-post/{id}")]
        public async Task<ActionResult<Comment>> CreatePostComment(int id, Comment comment)
        {
            // Set the post ID for the comment
            comment.PostId = id;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.ComId }, comment);
        }

        // POST api/comment/on-comment/id
        [HttpPost("on-comment/{id}")]
        public async Task<ActionResult<Comment>> CreateCommentOnComment(int id, Comment comment)
        {
            // Set the parent comment ID for the comment
            comment.ComId = id;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.ComId }, comment);
        }

        // PUT api/comment/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment comment)
        {
            if (id != comment.ComId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/comment/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.ComId == id);
        }


    }
}
